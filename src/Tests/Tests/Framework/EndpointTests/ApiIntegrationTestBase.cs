using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using FluentAssertions.Execution;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiIntegrationTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override IElasticClient Client => Cluster.Client;
		protected abstract bool ExpectIsValid { get; }
		protected abstract int ExpectStatusCode { get; }

		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		protected virtual void ExpectResponse(TResponse response) { }

		[I] public virtual async Task HandlesStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(ExpectStatusCode));

		[I] public virtual async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(ExpectIsValid));

		[I] public virtual async Task ReturnsExpectedResponse() => await AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert)
		{
			if (!ExpectIsValid) return base.AssertOnAllResponses(assert);

			return base.AssertOnAllResponses((r) =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& IsNotRequestExceptionType(r.ApiCall.OriginalException.GetType()))
				{
					ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException).Throw();
					return;
				}

				using (var scope = new AssertionScope())
				{
					assert(r);
					var failures = scope.Discard();
					if (failures.Length <= 0) return;

					var failure = failures[0];
					scope.AddReportable("Failure", failure);
					scope.AddReportable("DebugInformation", r.DebugInformation);
					scope.FailWith($@"{{Failure}}
Response Under Test:
{{DebugInformation}}");
				}
			});
		}

		private static bool IsNotRequestExceptionType(Type exceptionType)
		{
#if DOTNETCORE
			return exceptionType != typeof(HttpRequestException);
#else
			return exceptionType != typeof(WebException);
#endif
		}
	}
}

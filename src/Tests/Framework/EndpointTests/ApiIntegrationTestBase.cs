using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : ClusterBase, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract int ExpectStatusCode { get; }
		protected abstract bool ExpectIsValid { get; }
		protected virtual void ExpectResponse(TResponse response) { }

		protected ApiIntegrationTestBase(ClusterBase cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override IElasticClient Client => this.Cluster.Client;
		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		[I] public async Task ReturnsExpectedStatusCode() =>
			await this.AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] public async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(this.ExpectIsValid));

		[I] public async Task ReturnsExpectedResponse() => await this.AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert)
		{
			if (!this.ExpectIsValid) return base.AssertOnAllResponses(assert);

			return base.AssertOnAllResponses((r) =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
					&& IsNotRequestExceptionType(r.ApiCall.OriginalException.GetType()))
				{
					ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException).Throw();
					return;
				}

				assert(r);
			});
		}

		private static bool IsNotRequestExceptionType(Type exceptionType)
		{
#if DOTNETCORE
			return exceptionType != typeof(System.Net.Http.HttpRequestException);
#else
			return exceptionType != typeof(WebException);
#endif
		}
	}
}

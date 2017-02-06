using System;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;

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

		[I]
		protected async Task HandlesStatusCode() =>
			await this.AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I]
		protected async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(this.ExpectIsValid));

		[I]
		protected async Task ReturnsExpectedResponse() => await this.AssertOnAllResponses(ExpectResponse);

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

	public abstract class ApiIntegrationAgainstNewIndexTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : ClusterBase, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiIntegrationAgainstNewIndexTestBase(ClusterBase cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values) client.CreateIndex(index, this.CreateIndexSettings).ShouldBeValid();
			var indices = Infer.Indices(values.Values.Select(i => (IndexName)i));
			client.ClusterHealth(f => f.WaitForStatus(WaitForStatus.Yellow).Index(indices))
				.ShouldBeValid();
		}

		protected virtual ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create;
	}
}

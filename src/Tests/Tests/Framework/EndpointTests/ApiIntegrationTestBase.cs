using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster , new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract int ExpectStatusCode { get; }
		protected abstract bool ExpectIsValid { get; }
		protected virtual void ExpectResponse(TResponse response) { }

		protected ApiIntegrationTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override IElasticClient Client => this.Cluster.Client;
		protected override TInitializer Initializer => Activator.CreateInstance<TInitializer>();

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public virtual async Task ReturnsExpectedStatusCode() =>
			await this.AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] public virtual async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(this.ExpectIsValid));

		[I] public virtual async Task ReturnsExpectedResponse() => await this.AssertOnAllResponses(ExpectResponse);

		protected override Task AssertOnAllResponses(Action<TResponse> assert)
		{
			if (!this.ExpectIsValid) return base.AssertOnAllResponses(assert);

			return base.AssertOnAllResponses((r) =>
			{
				if (TestClient.Configuration.RunIntegrationTests && !r.IsValid && r.ApiCall.OriginalException != null
				    && !(r.ApiCall.OriginalException is ElasticsearchClientException))
				{
					ExceptionDispatchInfo.Capture(r.ApiCall.OriginalException.Demystify()).Throw();
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
					scope.FailWith( $@"{{Failure}}
Response Under Test:
{{DebugInformation}}");

				}
			});
		}
	}
}

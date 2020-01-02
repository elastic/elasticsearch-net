using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.UnfreezeIndex
{
	public class UnfreezeIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, UnfreezeIndexResponse, IUnfreezeIndexRequest, UnfreezeIndexDescriptor, UnfreezeIndexRequest>
	{
		public UnfreezeIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UnfreezeIndexRequest Initializer => new UnfreezeIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_unfreeze";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Unfreeze(CallIsolatedValue),
			(client, f) => client.Indices.UnfreezeAsync(CallIsolatedValue),
			(client, r) => client.Indices.Unfreeze(r),
			(client, r) => client.Indices.UnfreezeAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var freeze = client.Indices.Freeze(CallIsolatedValue);
			freeze.IsValid.Should().BeTrue();
		}

		protected override void ExpectResponse(UnfreezeIndexResponse response) => response.Acknowledged.Should().BeTrue();
	}
}

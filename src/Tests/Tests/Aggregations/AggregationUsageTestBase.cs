using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using static Nest.Infer;
using Xunit;

namespace Tests.Aggregations
{
	public abstract class AggregationUsageTestBase
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected AggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		// Fixes Rider live test discovery see: https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] public override Task HandlesStatusCode() => base.HandlesStatusCode();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_search";

	}
}

using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IIndicesStatsResponse, IIndicesStatsRequest, IndicesStatsDescriptor, IndicesStatsRequest>
	{
		public IndicesStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<IndicesStatsDescriptor, IIndicesStatsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override IndicesStatsRequest Initializer => new IndicesStatsRequest(Infer.AllIndices);
		protected override string UrlPath => "/_stats";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.IndicesStats(Infer.AllIndices, f),
			(client, f) => client.IndicesStatsAsync(Infer.AllIndices, f),
			(client, r) => client.IndicesStats(r),
			(client, r) => client.IndicesStatsAsync(r)
		);
	}
}

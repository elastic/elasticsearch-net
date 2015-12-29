using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.Monitoring.IndicesStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class IndicesStatsApiTests : ApiIntegrationTestBase<IIndicesStatsResponse, IIndicesStatsRequest, IndicesStatsDescriptor, IndicesStatsRequest>
	{
		public IndicesStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndicesStats(Infer.AllIndices, f),
			fluentAsync: (client, f) => client.IndicesStatsAsync(Infer.AllIndices, f),
			request: (client, r) => client.IndicesStats(r),
			requestAsync: (client, r) => client.IndicesStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_stats";

		protected override Func<IndicesStatsDescriptor, IIndicesStatsRequest> Fluent => d => d;

		protected override IndicesStatsRequest Initializer => new IndicesStatsRequest(Infer.AllIndices);
	}
}

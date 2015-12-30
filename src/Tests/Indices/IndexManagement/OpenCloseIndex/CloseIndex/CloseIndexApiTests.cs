using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	[Collection(IntegrationContext.Indexing)]
	public class CloseIndexApiTests : ApiIntegrationTestBase<ICloseIndexResponse, ICloseIndexRequest, CloseIndexDescriptor, CloseIndexRequest>
	{
		public CloseIndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var index in values.Values) client.CreateIndex(index);
			client.ClusterHealth(f => f.WaitForStatus(WaitForStatus.Yellow));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CloseIndex(CallIsolatedValue),
			fluentAsync: (client, f) => client.CloseIndexAsync(CallIsolatedValue),
			request: (client, r) => client.CloseIndex(r),
			requestAsync: (client, r) => client.CloseIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_close";

		protected override CloseIndexRequest Initializer => new CloseIndexRequest(CallIsolatedValue);
	}
}
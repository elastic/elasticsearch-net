using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.IndicesExists
{
	[Collection(IntegrationContext.Indexing)]
	public class IndexExistsApiTests : ApiIntegrationTestBase<IExistsResponse, IIndexExistsRequest, IndexExistsDescriptor, IndexExistsRequest>
	{
		public IndexExistsApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndexExists(CallIsolatedValue),
			fluentAsync: (client, f) => client.IndexExistsAsync(CallIsolatedValue),
			request: (client, r) => client.IndexExists(r),
			requestAsync: (client, r) => client.IndexExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override IndexExistsRequest Initializer => new IndexExistsRequest(CallIsolatedValue);
	}
}
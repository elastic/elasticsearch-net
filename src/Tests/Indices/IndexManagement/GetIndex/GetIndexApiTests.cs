using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.GetIndex
{
	[Collection(IntegrationContext.Indexing)]
	public class GetIndexApiTests : ApiIntegrationTestBase<IGetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		public GetIndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndex(CallIsolatedValue),
			fluentAsync: (client, f) => client.GetIndexAsync(CallIsolatedValue),
			request: (client, r) => client.GetIndex(r),
			requestAsync: (client, r) => client.GetIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override GetIndexRequest Initializer => new GetIndexRequest(CallIsolatedValue);
	}
}

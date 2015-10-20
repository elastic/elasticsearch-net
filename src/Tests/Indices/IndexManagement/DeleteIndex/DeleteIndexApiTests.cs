using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	[Collection(IntegrationContext.Indexing)]
	public class DeleteIndexApiTests 
		: ApiIntegrationTestBase<IIndicesResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteIndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteIndex(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteIndexAsync(CallIsolatedValue),
			request: (client, r) => client.DeleteIndex(r),
			requestAsync: (client, r) => client.DeleteIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(CallIsolatedValue);
	}
}

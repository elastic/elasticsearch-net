using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	public class DeleteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, IDeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

	public class DeleteAllIndicesApiTests
		: ApiTestBase<WritableCluster, IDeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteAllIndicesApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteIndex(AllIndices),
			fluentAsync: (client, f) => client.DeleteIndexAsync(AllIndices),
			request: (client, r) => client.DeleteIndex(r),
			requestAsync: (client, r) => client.DeleteIndexAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_all";

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(AllIndices);
	}
}

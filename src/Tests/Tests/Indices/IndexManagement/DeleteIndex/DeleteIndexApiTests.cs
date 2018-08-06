using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	public class DeleteIndexApiTests : ApiIntegrationAgainstNewIndexTestBase
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

		protected override void ExpectResponse(IDeleteIndexResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}

	public class DeleteNonExistentIndexApiTests : ApiIntegrationTestBase
		<WritableCluster, IDeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteNonExistentIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteIndex(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteIndexAsync(CallIsolatedValue),
			request: (client, r) => client.DeleteIndex(r),
			requestAsync: (client, r) => client.DeleteIndexAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteIndexResponse response)
		{
			response.Acknowledged.Should().BeFalse();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.Error.Reason.Should().Be("no such index");
		}
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

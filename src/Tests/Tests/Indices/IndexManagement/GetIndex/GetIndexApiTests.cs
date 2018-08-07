using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		private static readonly IndexName ProjectIndex = Index<Project>();

		public GetIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndex(typeof(Project)),
			fluentAsync: (client, f) => client.GetIndexAsync(typeof(Project)),
			request: (client, r) => client.GetIndex(r),
			requestAsync: (client, r) => client.GetIndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project";

		protected override GetIndexRequest Initializer => new GetIndexRequest(ProjectIndex);

	    protected override void ExpectResponse(IGetIndexResponse response)
	    {
	        response.Indices.Should().NotBeNull();
	        response.Indices.Count.Should().BeGreaterThan(0);
	        var projectIndex = response.Indices[ProjectIndex];
	        projectIndex.Should().NotBeNull();
	    }
	}


	public class GetAllIndicesApiTests
		: ApiTestBase<ReadOnlyCluster,IGetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		public GetAllIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndex(AllIndices),
			fluentAsync: (client, f) => client.GetIndexAsync(AllIndices),
			request: (client, r) => client.GetIndex(r),
			requestAsync: (client, r) => client.GetIndexAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_all";

		protected override GetIndexRequest Initializer => new GetIndexRequest(AllIndices);
	}
}

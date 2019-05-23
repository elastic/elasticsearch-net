using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.GetIndex
{
	public class GetIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		private static readonly IndexName ProjectIndex = Index<Project>();

		public GetIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetIndexRequest Initializer => new GetIndexRequest(ProjectIndex);
		protected override string UrlPath => $"/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetIndex(typeof(Project)),
			(client, f) => client.Indices.GetIndexAsync(typeof(Project)),
			(client, r) => client.Indices.GetIndex(r),
			(client, r) => client.Indices.GetIndexAsync(r)
		);

		protected override void ExpectResponse(GetIndexResponse response)
		{
			response.Indices.Should().NotBeNull();
			response.Indices.Count.Should().BeGreaterThan(0);
			var projectIndex = response.Indices[ProjectIndex];
			projectIndex.Should().NotBeNull();
		}
	}


	public class GetAllIndicesApiTests
		: ApiTestBase<ReadOnlyCluster, GetIndexResponse, IGetIndexRequest, GetIndexDescriptor, GetIndexRequest>
	{
		public GetAllIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetIndexRequest Initializer => new GetIndexRequest(AllIndices);
		protected override string UrlPath => $"/_all";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetIndex(AllIndices),
			(client, f) => client.Indices.GetIndexAsync(AllIndices),
			(client, r) => client.Indices.GetIndex(r),
			(client, r) => client.Indices.GetIndexAsync(r)
		);
	}
}

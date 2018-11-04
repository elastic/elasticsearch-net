using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatCount
{
	public class CatCountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatCount(),
			(client, f) => client.CatCountAsync(),
			(client, r) => client.CatCount(r),
			(client, r) => client.CatCountAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatCountRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
	}

	public class CatCountSingleIndexApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountSingleIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatCount(c => c.Index<Project>()),
			(client, f) => client.CatCountAsync(c => c.Index<Project>()),
			(client, r) => client.CatCount(new CatCountRequest(typeof(Project))),
			(client, r) => client.CatCountAsync(new CatCountRequest(typeof(Project)))
		);

		protected override void ExpectResponse(ICatResponse<CatCountRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
	}
}

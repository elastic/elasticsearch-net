using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatAliases
{
	public class CatAliasesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatAliasesRecord>, ICatAliasesRequest, CatAliasesDescriptor, CatAliasesRequest>
	{
		public CatAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/aliases";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatAliases(),
			(client, f) => client.CatAliasesAsync(),
			(client, r) => client.CatAliases(r),
			(client, r) => client.CatAliasesAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatAliasesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Alias == DefaultSeeder.ProjectsAliasName);
	}
}

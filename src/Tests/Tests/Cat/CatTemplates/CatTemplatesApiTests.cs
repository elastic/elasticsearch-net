using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatTemplates
{
	[SkipVersion("<5.1.0", "CatTemplates is an API introduced in 5.1")]
	public class CatTemplatesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatTemplatesRecord>, ICatTemplatesRequest, CatTemplatesDescriptor, CatTemplatesRequest>
	{
		public CatTemplatesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/templates";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatTemplates(),
			(client, f) => client.CatTemplatesAsync(),
			(client, r) => client.CatTemplates(r),
			(client, r) => client.CatTemplatesAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatTemplatesRecord> response)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Template));
#pragma warning restore CS0618 // Type or member is obsolete
		}
	}
}

using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Cat.CatCount
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatCountApiTests : ApiIntegrationTestBase<ICatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatCount(),
			fluentAsync: (client, f) => client.CatCountAsync(),
			request: (client, r) => client.CatCount(r),
			requestAsync: (client, r) => client.CatCountAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count";

		protected override void ExpectResponse(ICatResponse<CatCountRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
		}
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class CatCountSingleIndexApiTests : ApiIntegrationTestBase<ICatResponse<CatCountRecord>, ICatCountRequest, CatCountDescriptor, CatCountRequest>
	{
		public CatCountSingleIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatCount(c=>c.Index<Project>()),
			fluentAsync: (client, f) => client.CatCountAsync(c=>c.Index<Project>()),
			request: (client, r) => client.CatCount(new CatCountRequest(typeof(Project))),
			requestAsync: (client, r) => client.CatCountAsync(new CatCountRequest(typeof(Project)))
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/count/project";

		protected override void ExpectResponse(ICatResponse<CatCountRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Count != "0" && !string.IsNullOrEmpty(a.Count));
		}
	}
}

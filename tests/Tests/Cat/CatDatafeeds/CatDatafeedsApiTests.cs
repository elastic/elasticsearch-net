using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatDatafeeds
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatDatafeedsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatDatafeedsRecord>, ICatDatafeedsRequest, CatDatafeedsDescriptor,
			CatDatafeedsRequest>
	{
		public CatDatafeedsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/ml/datafeeds";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Datafeeds(),
			(client, f) => client.Cat.DatafeedsAsync(),
			(client, r) => client.Cat.Datafeeds(r),
			(client, r) => client.Cat.DatafeedsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatDatafeedsRecord> response) => response.ShouldBeValid();
	}
}

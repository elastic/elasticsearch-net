using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatDataFrameAnalytics
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatDataFrameAnalyticsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatDataFrameAnalyticsRecord>, ICatDataFrameAnalyticsRequest, CatDataFrameAnalyticsDescriptor,
			CatDataFrameAnalyticsRequest>
	{
		public CatDataFrameAnalyticsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/ml/data_frame/analytics";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.DataFrameAnalytics(),
			(client, f) => client.Cat.DataFrameAnalyticsAsync(),
			(client, r) => client.Cat.DataFrameAnalytics(r),
			(client, r) => client.Cat.DataFrameAnalyticsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatDataFrameAnalyticsRecord> response) => response.ShouldBeValid();
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using Tests.XPack.MachineLearning;

namespace Tests.Cat.CatDataFrameAnalytics
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatDataFrameAnalyticsApiTests
		: MachineLearningIntegrationTestBase<CatResponse<CatDataFrameAnalyticsRecord>, ICatDataFrameAnalyticsRequest, CatDataFrameAnalyticsDescriptor,
			CatDataFrameAnalyticsRequest>
	{
		public CatDataFrameAnalyticsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

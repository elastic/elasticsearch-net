/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

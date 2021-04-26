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

using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PreviewDataFeed
{
	//TODO what does an invalid request return here? this API returns a json array for the happy path
	public class PreviewDatafeedApiTests
		: MachineLearningIntegrationTestBase<PreviewDatafeedResponse<Metric>, IPreviewDatafeedRequest, PreviewDatafeedDescriptor,
			PreviewDatafeedRequest>
	{
		public PreviewDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override PreviewDatafeedRequest Initializer => new PreviewDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ml/datafeeds/{CallIsolatedValue}-datafeed/_preview";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PreviewDatafeed<Metric>(f),
			(client, f) => client.MachineLearning.PreviewDatafeedAsync<Metric>(f),
			(client, r) => client.MachineLearning.PreviewDatafeed<Metric>(r),
			(client, r) => client.MachineLearning.PreviewDatafeedAsync<Metric>(r)
		);

		protected override PreviewDatafeedDescriptor NewDescriptor() => new PreviewDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(PreviewDatafeedResponse<Metric> response)
		{
			response.IsValid.Should().BeTrue();
			response.Data.Count.Should().BeGreaterThan(0);
		}
	}
}

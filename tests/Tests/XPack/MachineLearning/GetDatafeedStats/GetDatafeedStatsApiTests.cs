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
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_ml/datafeeds/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeedStats(f),
			(client, f) => client.MachineLearning.GetDatafeedStatsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeedStats(r),
			(client, r) => client.MachineLearning.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			var datafeedStats = response.Datafeeds.First();
			datafeedStats.State.Should().Be(DatafeedState.Stopped);

			if (Cluster.ClusterConfiguration.Version >= "7.4.0")
				datafeedStats.TimingStats.Should().NotBeNull();
		}
	}

	public class GetDatafeedStatsWithDatafeedIdApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsWithDatafeedIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override GetDatafeedStatsRequest Initializer => new GetDatafeedStatsRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeedStats(f),
			(client, f) => client.MachineLearning.GetDatafeedStatsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeedStats(r),
			(client, r) => client.MachineLearning.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}
}

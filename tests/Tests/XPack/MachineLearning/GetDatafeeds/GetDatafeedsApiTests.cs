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

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_ml/datafeeds";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeeds(f),
			(client, f) => client.MachineLearning.GetDatafeedsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeeds(r),
			(client, r) => client.MachineLearning.GetDatafeedsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			var firstDatafeed = response.Datafeeds.FirstOrDefault();

			firstDatafeed.Should().NotBeNull();
			firstDatafeed.DatafeedId.Should().NotBeNullOrWhiteSpace();
			firstDatafeed.JobId.Should().NotBeNullOrWhiteSpace();

			firstDatafeed.QueryDelay.Should().NotBeNull("QueryDelay");
			firstDatafeed.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			firstDatafeed.Indices.Should().NotBeNull("Indices");
			firstDatafeed.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			firstDatafeed.ScrollSize.Should().Be(1000);

			firstDatafeed.ChunkingConfig.Should().NotBeNull();
			firstDatafeed.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			firstDatafeed.Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}

	public class GetDatafeedsWithDatafeedIdApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsWithDatafeedIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override GetDatafeedsRequest Initializer => new GetDatafeedsRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeeds(f),
			(client, f) => client.MachineLearning.GetDatafeedsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeeds(r),
			(client, r) => client.MachineLearning.GetDatafeedsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			var firstDatafeed = response.Datafeeds.FirstOrDefault();

			firstDatafeed.Should().NotBeNull();
			firstDatafeed.DatafeedId.Should().NotBeNullOrWhiteSpace();
			firstDatafeed.JobId.Should().NotBeNullOrWhiteSpace();

			firstDatafeed.QueryDelay.Should().NotBeNull("QueryDelay");
			firstDatafeed.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			firstDatafeed.Indices.Should().NotBeNull("Indices");
			firstDatafeed.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			firstDatafeed.ScrollSize.Should().Be(1000);

			firstDatafeed.ChunkingConfig.Should().NotBeNull();
			firstDatafeed.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			firstDatafeed.Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}
}

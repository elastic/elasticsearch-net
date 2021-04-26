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
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetBuckets
{
	public class GetBucketsApiTests
		: MachineLearningIntegrationTestBase<GetBucketsResponse, IGetBucketsRequest, GetBucketsDescriptor, GetBucketsRequest>
	{
		public GetBucketsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetBucketsDescriptor, IGetBucketsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override GetBucketsRequest Initializer => new GetBucketsRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/results/buckets";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexBucket(client, callUniqueValue.Value, new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetBuckets(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetBucketsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetBuckets(r),
			(client, r) => client.MachineLearning.GetBucketsAsync(r)
		);

		protected override GetBucketsDescriptor NewDescriptor() => new GetBucketsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetBucketsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().Be(1);
			response.Buckets.Should().HaveCount(1);

			var bucket = response.Buckets.First();
			bucket.ResultType.Should().Be("bucket");
			bucket.AnomalyScore.Should().Be(90);
			bucket.BucketSpan.Should().Be(1);
			bucket.InitialAnomalyScore.Should().Be(0);
			bucket.EventCount.Should().Be(0);
			bucket.IsInterim.Should().Be(true);
			bucket.BucketInfluencers.Should().BeEmpty();
			bucket.ProcessingTimeMilliseconds.Should().Be(0);
			bucket.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
		}
	}

	public class GetBucketsWithTimestampApiTests
		: MachineLearningIntegrationTestBase<GetBucketsResponse, IGetBucketsRequest, GetBucketsDescriptor, GetBucketsRequest>
	{
		public GetBucketsWithTimestampApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected static DateTimeOffset TheTimestamp = new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero);

		protected override Func<GetBucketsDescriptor, IGetBucketsRequest> Fluent => f => f.Timestamp(TheTimestamp);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override GetBucketsRequest Initializer => new GetBucketsRequest(CallIsolatedValue, TheTimestamp);

		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/results/buckets/{TheTimestamp.ToUnixTimeMilliseconds()}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				IndexBucket(client, callUniqueValue.Value, new DateTimeOffset(2016, 6, 2, 00, 00, 00, TimeSpan.Zero));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetBuckets(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetBucketsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetBuckets(r),
			(client, r) => client.MachineLearning.GetBucketsAsync(r)
		);

		protected override GetBucketsDescriptor NewDescriptor() => new GetBucketsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetBucketsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().Be(1);
			response.Buckets.Should().HaveCount(1);

			var bucket = response.Buckets.First();
			bucket.ResultType.Should().Be("bucket");
			bucket.AnomalyScore.Should().Be(90);
			bucket.BucketSpan.Should().Be(1);
			bucket.InitialAnomalyScore.Should().Be(0);
			bucket.EventCount.Should().Be(0);
			bucket.IsInterim.Should().Be(true);
			bucket.BucketInfluencers.Should().BeEmpty();
			bucket.ProcessingTimeMilliseconds.Should().Be(0);
			bucket.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetOverallBuckets
{
	public class GetOverallBucketsApiTests
		: MachineLearningIntegrationTestBase<GetOverallBucketsResponse, IGetOverallBucketsRequest, GetOverallBucketsDescriptor,
			GetOverallBucketsRequest>
	{
		private const int BucketSpanSeconds = 3600;
		private const int OverallBucketCount = 3000;

		public GetOverallBucketsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override GetOverallBucketsRequest Initializer => new GetOverallBucketsRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/results/overall_buckets";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putJobResponse = client.MachineLearning.PutJob<object>(callUniqueValue.Value, f => f
					.Description("GetOverallBucketsApiTests")
					.AnalysisConfig(a => a
						.BucketSpan($"{BucketSpanSeconds}s")
						.Detectors(d => d
							.Count()
						)
					)
					.DataDescription(d => d
						.TimeFormat("epoch_ms")
					)
				);

				if (!putJobResponse.IsValid)
					throw new Exception($"Problem putting job {callUniqueValue.Value} for integration test: {putJobResponse.DebugInformation}");

				OpenJob(client, callUniqueValue.Value);
				PostJobData(client, callUniqueValue.Value, OverallBucketCount, BucketSpanSeconds);
				FlushJob(client, callUniqueValue.Value, true);
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				DeleteJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetOverallBuckets(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.GetOverallBucketsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetOverallBuckets(r),
			(client, r) => client.MachineLearning.GetOverallBucketsAsync(r)
		);

		protected override GetOverallBucketsDescriptor NewDescriptor() => new GetOverallBucketsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetOverallBucketsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().Be(OverallBucketCount);
			response.OverallBuckets.Should().HaveCount(OverallBucketCount);

			var bucket = response.OverallBuckets.First();
			bucket.ResultType.Should().Be("overall_bucket");
			bucket.OverallScore.Should().Be(0);
			bucket.BucketSpan.Should().Be(BucketSpanSeconds);
			bucket.IsInterim.Should().Be(false);
			bucket.Jobs.Should().HaveCount(1);

			var job = bucket.Jobs.First();
			job.JobId.Should().Be(CallIsolatedValue);
			job.MaxAnomalyScore.Should().Be(0);
		}
	}
}

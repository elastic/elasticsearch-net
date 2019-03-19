using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetOverallBuckets
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class GetOverallBucketsApiTests
		: MachineLearningIntegrationTestBase<IGetOverallBucketsResponse, IGetOverallBucketsRequest, GetOverallBucketsDescriptor,
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
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/overall_buckets";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putJobResponse = client.PutJob<object>(callUniqueValue.Value, f => f
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

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetOverallBuckets(CallIsolatedValue, f),
			(client, f) => client.GetOverallBucketsAsync(CallIsolatedValue, f),
			(client, r) => client.GetOverallBuckets(r),
			(client, r) => client.GetOverallBucketsAsync(r)
		);

		protected override GetOverallBucketsDescriptor NewDescriptor() => new GetOverallBucketsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IGetOverallBucketsResponse response)
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

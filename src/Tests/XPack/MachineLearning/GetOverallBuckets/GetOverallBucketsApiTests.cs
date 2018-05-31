using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetOverallBuckets
{
	public class GetOverallBucketsApiTests : MachineLearningIntegrationTestBase<IGetOverallBucketsResponse, IGetOverallBucketsRequest, GetOverallBucketsDescriptor, GetOverallBucketsRequest>
	{
		private const int BucketSpanSeconds = 3600;
		private const int OverallBucketCount = 3000;

		public GetOverallBucketsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

				var timestamp = 1483228800000L; // 2017-01-01T00:00:00Z
				var data = new List<object>(OverallBucketCount);
				for (var i = 0; i < OverallBucketCount; i++)
				{
					data.Add(new { time = timestamp });
					if (i % 1000 == 0)
					{
						data.AddRange(new []
						{
							new { time = timestamp },
							new { time = timestamp },
							new { time = timestamp }
						});
					}
					timestamp += BucketSpanSeconds * 1000;
				}

				var postJobDataResponse = client.PostJobData(callUniqueValue.Value, d => d.Data(data));
				if (!postJobDataResponse.IsValid)
					throw new Exception($"Problem posting data for integration test: {postJobDataResponse.DebugInformation}");

				FlushJob(client, callUniqueValue.Value, true);
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetOverallBuckets(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetOverallBucketsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetOverallBuckets(r),
			requestAsync: (client, r) => client.GetOverallBucketsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/overall_buckets";
		protected override bool SupportsDeserialization => true;
		protected override GetOverallBucketsDescriptor NewDescriptor() => new GetOverallBucketsDescriptor(CallIsolatedValue);
		protected override object ExpectJson => null;
		protected override Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> Fluent => f => f;
		protected override GetOverallBucketsRequest Initializer => new GetOverallBucketsRequest(CallIsolatedValue);

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

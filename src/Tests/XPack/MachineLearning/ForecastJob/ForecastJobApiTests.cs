using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.ForecastJob
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class ForecastJobApiTests : MachineLearningIntegrationTestBase<IForecastJobResponse, IForecastJobRequest, ForecastJobDescriptor, ForecastJobRequest>
	{
		private const int BucketSpanSeconds = 3600;

		public ForecastJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putJobResponse = client.PutJob<object>(callUniqueValue.Value, f => f
					.Description("ForecastJobApiTests")
					.AnalysisConfig(a => a
						.BucketSpan($"{BucketSpanSeconds}s")
						.Detectors(d => d
							.Mean(m => m
								.FieldName("value")
							)
						)
					)
					.DataDescription(d => d
						.TimeFormat("epoch")
					)
				);

				if (!putJobResponse.IsValid)
					throw new Exception($"Problem putting job {callUniqueValue.Value} for integration test: {putJobResponse.DebugInformation}");

				OpenJob(client, callUniqueValue.Value);

				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				var now = (DateTime.UtcNow - epoch).TotalSeconds;
				var timestamp = now - 50 * BucketSpanSeconds;
				var data = new List<object>(50);
				while (timestamp < now)
				{
					data.AddRange(new []
					{
						new { time = timestamp, value = 10d },
						new { time = timestamp, value = 30d }
					});
					timestamp += BucketSpanSeconds;
				}

				var postJobDataResponse = client.PostJobData(callUniqueValue.Value, d => d.Data(data));
				if (!postJobDataResponse.IsValid)
					throw new Exception($"Problem posting data for integration test: {postJobDataResponse.DebugInformation}");

				FlushJob(client, callUniqueValue.Value, false);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				CloseJob(client, callUniqueValue.Value);
				DeleteJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ForecastJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.ForecastJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.ForecastJob(r),
			requestAsync: (client, r) => client.ForecastJobAsync(r)
		);

		protected override ForecastJobDescriptor NewDescriptor() => new ForecastJobDescriptor(CallIsolatedValue);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_forecast";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<ForecastJobDescriptor, IForecastJobRequest> Fluent => f => f;
		protected override ForecastJobRequest Initializer => new ForecastJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(IForecastJobResponse response)
		{
			response.ShouldBeValid();
			response.ForecastId.Should().NotBeNullOrEmpty();
		}
	}
}

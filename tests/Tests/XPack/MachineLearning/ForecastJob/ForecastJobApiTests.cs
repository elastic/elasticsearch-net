// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.ForecastJob
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class ForecastJobApiTests
		: MachineLearningIntegrationTestBase<ForecastJobResponse, IForecastJobRequest, ForecastJobDescriptor, ForecastJobRequest>
	{
		private const int BucketSpanSeconds = 3600;

		public ForecastJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<ForecastJobDescriptor, IForecastJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override ForecastJobRequest Initializer => new ForecastJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/_forecast";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putJobResponse = client.MachineLearning.PutJob<object>(callUniqueValue.Value, f => f
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
					data.AddRange(new[]
					{
						new { time = timestamp, value = 10d },
						new { time = timestamp, value = 30d }
					});
					timestamp += BucketSpanSeconds;
				}

				var postJobDataResponse = client.MachineLearning.PostJobData(callUniqueValue.Value, d => d.Data(data));
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
			(client, f) => client.MachineLearning.ForecastJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.ForecastJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.ForecastJob(r),
			(client, r) => client.MachineLearning.ForecastJobAsync(r)
		);

		protected override ForecastJobDescriptor NewDescriptor() => new ForecastJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(ForecastJobResponse response)
		{
			response.ShouldBeValid();
			response.ForecastId.Should().NotBeNullOrEmpty();
		}
	}
}

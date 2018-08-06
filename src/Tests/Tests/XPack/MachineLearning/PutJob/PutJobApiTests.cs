using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobApiTests : MachineLearningIntegrationTestBase<IPutJobResponse, IPutJobRequest, PutJobDescriptor<Metric>, PutJobRequest>
	{
		public PutJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutJob(r),
			requestAsync: (client, r) => client.PutJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => false;
		protected override PutJobDescriptor<Metric> NewDescriptor() => new PutJobDescriptor<Metric>(CallIsolatedValue);

		protected override object ExpectJson => new
			{
				analysis_config = new
				{
					bucket_span = "30m",
					detectors = new[]
					{
						new
						{
							function = "sum",
							field_name = "total"
						}
					},
					latency = "0s",
				},
				data_description = new
				{
					time_field = "@timestamp"
				},
				description = "Lab 1 - Simple example",
				results_index_name = "server-metrics"
			};

		protected override Func<PutJobDescriptor<Metric>, IPutJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example")
			.ResultsIndexName("server-metrics")
			.AnalysisConfig(a => a
				.BucketSpan("30m")
				.Latency("0s")
				.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
			)
			.DataDescription(d => d.TimeField(r => r.Timestamp));

		protected override PutJobRequest Initializer =>
			new PutJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example",
				ResultsIndexName = "server-metrics",
				AnalysisConfig = new AnalysisConfig
				{
					BucketSpan = "30m",
					Latency = "0s",
					Detectors = new []
					{
						new SumDetector
						{
							FieldName = Field<Metric>(f => f.Total)
						}
					}
				},
				DataDescription = new DataDescription
				{
					TimeField = Field<Metric>(f => f.Timestamp)
				}
			};

		protected override void ExpectResponse(IPutJobResponse response)
		{
			response.ShouldBeValid();

			response.JobId.Should().Be(CallIsolatedValue);
			// "job_version" : "5.5.2"
			response.JobType.Should().Be("anomaly_detector");
			response.Description.Should().Be("Lab 1 - Simple example");
			response.CreateTime.Should().BeBefore(DateTimeOffset.UtcNow);

			response.AnalysisConfig.Should().NotBeNull();
			response.AnalysisConfig.BucketSpan.Should().Be(new Time("30m"));
			response.AnalysisConfig.Latency.Should().Be(new Time("0s"));

			response.AnalysisConfig.Detectors.Should().NotBeNull();
			response.AnalysisConfig.Detectors.OfType<SumDetector>().Should().NotBeNull();

			var sumDetector = response.AnalysisConfig.Detectors.Cast<SumDetector>().First();
			sumDetector.DetectorDescription.Should().Be("sum(total)");
			sumDetector.Function.Should().Be("sum");
			sumDetector.FieldName.Name.Should().Be("total");
			sumDetector.DetectorIndex.Should().Be(0);

			response.AnalysisConfig.Influencers.Should().BeEmpty();

			response.DataDescription.TimeField.Name.Should().Be("@timestamp");
			response.DataDescription.TimeFormat.Should().Be("epoch_ms");

			response.ModelSnapshotRetentionDays.Should().Be(1);

			// User-defined names are prepended with "custom-" by X-Pack ML
			response.ResultsIndexName.Should().Be("custom-server-metrics");
		}
	}
}

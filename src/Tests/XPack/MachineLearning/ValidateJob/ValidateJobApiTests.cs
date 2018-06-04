using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.ValidateJob
{
	public class ValidateJobApiTests : MachineLearningIntegrationTestBase<IValidateJobResponse, IValidateJobRequest, ValidateJobDescriptor<Metric>, ValidateJobRequest>
	{
		public ValidateJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ValidateJob(f),
			fluentAsync: (client, f) => client.ValidateJobAsync(f),
			request: (client, r) => client.ValidateJob(r),
			requestAsync: (client, r) => client.ValidateJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/_validate";
		protected override bool SupportsDeserialization => false;

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

		protected override Func<ValidateJobDescriptor<Metric>, IValidateJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example")
			.ResultsIndexName("server-metrics")
			.AnalysisConfig(a => a
				.BucketSpan(new Time("30m"))
				.Latency("0s")
				.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
			)
			.DataDescription(d => d.TimeField(r => r.Timestamp));

		protected override ValidateJobRequest Initializer =>
			new ValidateJobRequest()
			{
				Description = "Lab 1 - Simple example",
				ResultsIndexName = "server-metrics",
				AnalysisConfig = new AnalysisConfig
				{
					BucketSpan = new Time("30m"),
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

		protected override void ExpectResponse(IValidateJobResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.EstimateModelMemory
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class EstimateModelMemoryApiTests : MachineLearningIntegrationTestBase<EstimateModelMemoryResponse, IEstimateModelMemoryRequest, EstimateModelMemoryDescriptor<Metric>, EstimateModelMemoryRequest>
	{
		public EstimateModelMemoryApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override Func<EstimateModelMemoryDescriptor<Metric>, IEstimateModelMemoryRequest> Fluent => f => f
			.AnalysisConfig(a => a
				.BucketSpan("30m")
				.Latency("0s")
				.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
			)
			.OverallCardinality(m =>
				m.Field(ff => ff.Response, 50)
				 .Field(ff => ff.Accept, 10)
			)
			.MaxBucketCardinality(m =>
				m.Field(ff => ff.Response, 500)
				 .Field(ff => ff.Accept, 100)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

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
			overall_cardinality = new
			{
				response = 50,
				accept = 10,
			},
			max_bucket_cardinality = new
			{
				response = 500,
				accept = 100,
			}
		};

		protected override EstimateModelMemoryRequest Initializer => new EstimateModelMemoryRequest
		{
			AnalysisConfig = new AnalysisConfig
			{
				BucketSpan = "30m",
				Latency = "0s",
				Detectors = new[]
				{
					new SumDetector
					{
						FieldName = Field<Metric>(f => f.Total)
					}
				}
			},
			OverallCardinality = new OverallCardinality
			{
				{ Field<Metric>(f => f.Response), 50 },
				{ Field<Metric>(f => f.Accept), 10 }
			},
			MaxBucketCardinality = new MaxBucketCardinality
			{
				{ Field<Metric>(f => f.Response), 500 },
				{ Field<Metric>(f => f.Accept), 100 }
			}
		};

		protected override string UrlPath => $"/_ml/anomaly_detectors/_estimate_model_memory";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.EstimateModelMemory(f),
			(client, f) => client.MachineLearning.EstimateModelMemoryAsync(f),
			(client, r) => client.MachineLearning.EstimateModelMemory(r),
			(client, r) => client.MachineLearning.EstimateModelMemoryAsync(r)
		);

		protected override EstimateModelMemoryDescriptor<Metric> NewDescriptor() => new EstimateModelMemoryDescriptor<Metric>();

		protected override void ExpectResponse(EstimateModelMemoryResponse response) => response.ShouldBeValid();
	}
}

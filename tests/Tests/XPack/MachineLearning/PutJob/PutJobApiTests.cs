// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobApiTests : MachineLearningIntegrationTestBase<PutJobResponse, IPutJobRequest, PutJobDescriptor<Metric>, PutJobRequest>
	{
		public PutJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

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

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var response = DeleteJob(client, value.Value);
				if (!response.IsValid)
					throw new Exception($"Problem in integration teardown {response.DebugInformation}");
			}
		}

		protected override int ExpectStatusCode => 200;

		protected override Func<PutJobDescriptor<Metric>, IPutJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example")
			.ResultsIndexName("server-metrics")
			.AnalysisConfig(a => a
				.BucketSpan("30m")
				.Latency("0s")
				.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
			)
			.DataDescription(d => d.TimeField(r => r.Timestamp));

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutJobRequest Initializer =>
			new PutJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example",
				ResultsIndexName = "server-metrics",
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
				DataDescription = new DataDescription
				{
					TimeField = Field<Metric>(f => f.Timestamp)
				}
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PutJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PutJob(r),
			(client, r) => client.MachineLearning.PutJobAsync(r)
		);

		protected override PutJobDescriptor<Metric> NewDescriptor() => new PutJobDescriptor<Metric>(CallIsolatedValue);

		protected override void ExpectResponse(PutJobResponse response)
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

			response.ModelSnapshotRetentionDays.Should().Be(TestClient.Configuration.InRange(">=7.8.0")
				? 10
				: 1);

			// User-defined names are prepended with "custom-" by X-Pack ML
			response.ResultsIndexName.Should().Be("custom-server-metrics");
		}
	}

	[SkipVersion("<6.4.0", "Custom rules came in 6.4.0")]
	public class PutJobWithCustomRulesApiTests : MachineLearningIntegrationTestBase<PutJobResponse, IPutJobRequest, PutJobDescriptor<Metric>, PutJobRequest>
	{
		public PutJobWithCustomRulesApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			analysis_config = new
			{
				bucket_span = "30m",
				detectors = new[]
				{
					new
					{
						function = "count",
						by_field_name = "total",
						over_field_name = "host",
						partition_field_name = "service",
						custom_rules = new []
						{
							new
							{
								actions = new [] { "skip_result" },
								conditions = new []
								{
									new
									{
										applies_to = "actual",
										@operator = "lt",
										value = 0.2
									},
									new
									{
										applies_to = "actual",
										@operator = "gte",
										value = 0.1
									}
								},
								scope = new
								{
									total = new
									{
										filter_id = "filter_1"
									},
									host = new
									{
										filter_id = "filter_2",
										filter_type = "include"
									},
									service = new
									{
										filter_id = "filter_3",
										filter_type = "exclude"
									},
								}
							}
						}
					}
				},
				latency = "0s",
			},
			data_description = new
			{
				time_field = "@timestamp"
			},
			description = "Lab 1 - Simple example",
			results_index_name = "server-metrics",
			model_snapshot_retention_days = 1
		};

		protected override int ExpectStatusCode => 200;

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var response = DeleteJob(client, value.Value);
				if (!response.IsValid)
					throw new Exception($"Problem in integration teardown {response.DebugInformation}");
			}
		}

		protected override Func<PutJobDescriptor<Metric>, IPutJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example")
			.ResultsIndexName("server-metrics")
			.ModelSnapshotRetentionDays(1)
			.AnalysisConfig(a => a
				.BucketSpan("30m")
				.Latency("0s")
				.Detectors(d => d
					.Count(c => c
						.ByFieldName(r => r.Total)
						.OverFieldName(r => r.Host)
						.PartitionFieldName(r => r.Service)
						.CustomRules(cr => cr
							.Rule(ru => ru
								.Actions(RuleAction.SkipResult)
								.Conditions(co => co
									.Condition(con => con
										.AppliesTo(AppliesTo.Actual)
										.Operator(ConditionOperator.LessThan)
										.Value(0.2)
									)
									.Condition(con => con
										.AppliesTo(AppliesTo.Actual)
										.Operator(ConditionOperator.GreaterThanOrEqual)
										.Value(0.1)
									)
								)
								.Scope<Metric>(sc => sc
									.Scope(r => r.Total, new FilterRef { FilterId = "filter_1" })
									.Scope(r => r.Host, new FilterRef { FilterId = "filter_2", FilterType = RuleFilterType.Include })
									.Scope(r => r.Service, new FilterRef { FilterId = "filter_3", FilterType = RuleFilterType.Exclude })
								)
							)
						)
					)
				)
			)
			.DataDescription(d => d.TimeField(r => r.Timestamp));

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutJobRequest Initializer =>
			new PutJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example",
				ResultsIndexName = "server-metrics",
				ModelSnapshotRetentionDays = 1,
				AnalysisConfig = new AnalysisConfig
				{
					BucketSpan = "30m",
					Latency = "0s",
					Detectors = new[]
					{
						new CountDetector
						{
							ByFieldName = Field<Metric>(r => r.Total),
							OverFieldName = Field<Metric>(r => r.Host),
							PartitionFieldName = Field<Metric>(r => r.Service),
							CustomRules = new []
							{
								new DetectionRule
								{
									Actions = new [] { RuleAction.SkipResult },
									Conditions = new []
									{
										new RuleCondition
										{
											AppliesTo = AppliesTo.Actual,
											Operator = ConditionOperator.LessThan,
											Value = 0.2
										},
										new RuleCondition
										{
											AppliesTo = AppliesTo.Actual,
											Operator = ConditionOperator.GreaterThanOrEqual,
											Value = 0.1
										}
									},
									Scope = new Dictionary<Field, FilterRef>
									{
										{ Field<Metric>(f => f.Total), new FilterRef { FilterId = "filter_1" } },
										{ Field<Metric>(f => f.Host), new FilterRef { FilterId = "filter_2", FilterType = RuleFilterType.Include } },
										{ Field<Metric>(f => f.Service), new FilterRef { FilterId = "filter_3", FilterType = RuleFilterType.Exclude } },
									}
								}
							}
						}
					}
				},
				DataDescription = new DataDescription
				{
					TimeField = Field<Metric>(f => f.Timestamp)
				}
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PutJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PutJob(r),
			(client, r) => client.MachineLearning.PutJobAsync(r)
		);

		protected override PutJobDescriptor<Metric> NewDescriptor() => new PutJobDescriptor<Metric>(CallIsolatedValue);

		protected override void ExpectResponse(PutJobResponse response)
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
			response.AnalysisConfig.Detectors.OfType<CountDetector>().Should().NotBeNull();

			var countDetector = response.AnalysisConfig.Detectors.Cast<CountDetector>().First();
			countDetector.DetectorDescription.Should().Be("count by total over host partitionfield=service");
			countDetector.Function.Should().Be("count");
			countDetector.ByFieldName.Name.Should().Be("total");
			countDetector.OverFieldName.Name.Should().Be("host");
			countDetector.PartitionFieldName.Name.Should().Be("service");
			countDetector.DetectorIndex.Should().Be(0);
			countDetector.CustomRules.Should().NotBeNullOrEmpty().And.HaveCount(1);

			var customRule = countDetector.CustomRules.First();
			customRule.Actions.Should().NotBeNullOrEmpty().And.Contain(RuleAction.SkipResult);
			customRule.Scope.Should().NotBeNull().And.HaveCount(3);
			customRule.Conditions.Should().NotBeNull().And.HaveCount(2);

			response.AnalysisConfig.Influencers.Should().BeEmpty();

			response.DataDescription.TimeField.Name.Should().Be("@timestamp");
			response.DataDescription.TimeFormat.Should().Be("epoch_ms");

			response.ModelSnapshotRetentionDays.Should().Be(1);

			// User-defined names are prepended with "custom-" by X-Pack ML
			response.ResultsIndexName.Should().Be("custom-server-metrics");
		}
	}
}

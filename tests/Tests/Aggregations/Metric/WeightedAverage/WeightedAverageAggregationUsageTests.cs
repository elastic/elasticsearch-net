// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;
using ValueType = Nest.ValueType;

namespace Tests.Aggregations.Metric.WeightedAverage
{
	/**
	 * A single-value metrics aggregation that computes the weighted average of numeric values that are extracted
	 * from the aggregated documents. These values can be extracted either from specific numeric fields in the documents.
	 * When calculating a regular average, each datapoint has an equal "weight" i.e. it contributes equally to the final
	 * value. Weighted averages, on the other hand, weight each datapoint differently. The amount that each
	 * datapoint contributes to the final value is extracted from the document, or provided by a script.
	 *
	 * NOTE: Only available in Elasticsearch 6.4.0+
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-metrics-weight-avg-aggregation.html[Weighted Avg Aggregation]
	 */
	[SkipVersion("<6.4.0", "Introduced in Elasticsearch 6.4.0+")]
	public class WeightedAverageAggregationUsageTests : AggregationUsageTestBase
	{
		public WeightedAverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			weighted_avg_commits = new
			{
				weighted_avg = new
				{
					value = new
					{
						field = "numberOfCommits",
						missing = 0.0
					},
					weight = new
					{
						script = new
						{
							source = "(doc['numberOfContributors']?.value ?: 0) + 1"
						}
					},
					value_type = "long"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.WeightedAverage("weighted_avg_commits", avg => avg
				.Value(v => v.Field(p => p.NumberOfCommits).Missing(0))
				.Weight(w => w.Script("(doc['numberOfContributors']?.value ?: 0) + 1"))
				.ValueType(ValueType.Long)
			);

		protected override AggregationDictionary InitializerAggs =>
			new WeightedAverageAggregation("weighted_avg_commits")
			{
				Value = new WeightedAverageValue(Field<Project>(p => p.NumberOfCommits))
				{
					Missing = 0
				},
				Weight = new WeightedAverageValue(new InlineScript("(doc['numberOfContributors']?.value ?: 0) + 1")),
				ValueType = ValueType.Long
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsAvg = response.Aggregations.WeightedAverage("weighted_avg_commits");
			commitsAvg.Should().NotBeNull();
			commitsAvg.Value.Should().BeGreaterThan(0);
		}
	}
}

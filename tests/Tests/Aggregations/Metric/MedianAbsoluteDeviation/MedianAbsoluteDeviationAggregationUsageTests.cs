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

namespace Tests.Aggregations.Metric.MedianAbsoluteDeviation
{
	/**
	 * A single-value aggregation that approximates the median absolute deviation of its search results.
	 *
	 * Median absolute deviation is a measure of variability. It is a robust statistic, meaning that it is
	 * useful for describing data that may have outliers, or may not be normally distributed.
	 * For such data it can be more descriptive than standard deviation.
	 *
	 * It is calculated as the median of each data point's deviation from the median of the
	 * entire sample. That is, for a random variable `X`, the median absolute deviation is `median(|median(X) - Xi|)`.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-metrics-median-absolute-deviation-aggregation.html[Median Absolute Deviation Aggregation]
	 */
	[SkipVersion("<6.6.0", "Introduced in Elasticsearch 6.6.0")]
	public class MedianAbsoluteDeviationAggregationUsageTests : AggregationUsageTestBase
	{
		public MedianAbsoluteDeviationAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			average_commits = new
			{
				avg = new
				{
					field = "numberOfCommits",
				}
			},
			commit_variability = new
			{
				median_absolute_deviation = new
				{
					field = "numberOfCommits"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Average("average_commits", avg => avg
				.Field(p => p.NumberOfCommits)
			)
			.MedianAbsoluteDeviation("commit_variability", m => m
				.Field(f => f.NumberOfCommits)
			);

		protected override AggregationDictionary InitializerAggs =>
			new AverageAggregation("average_commits", Infer.Field<Project>(p => p.NumberOfCommits)) &&
			new MedianAbsoluteDeviationAggregation("commit_variability", Infer.Field<Project>(p => p.NumberOfCommits));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var medianAbsoluteDeviation = response.Aggregations.MedianAbsoluteDeviation("commit_variability");
			medianAbsoluteDeviation.Should().NotBeNull();
			medianAbsoluteDeviation.Value.Should().BeGreaterThan(0);
		}
	}
}

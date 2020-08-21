// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Percentiles
{
	public class PercentilesAggregationUsageTests : AggregationUsageTestBase
	{
		public PercentilesAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits_outlier = new
			{
				percentiles = new
				{
					field = "numberOfCommits",
					percents = new[] { 95.0, 99.0, 99.9 },
					hdr = new
					{
						number_of_significant_value_digits = 3
					},
					script = new
					{
						source = "doc['numberOfCommits'].value * 1.2",
					},
					missing = 0.0
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Percentiles("commits_outlier", pr => pr
				.Field(p => p.NumberOfCommits)
				.Percents(95, 99, 99.9)
				.Method(m => m
					.HDRHistogram(hdr => hdr
						.NumberOfSignificantValueDigits(3)
					)
				)
				.Script(ss => ss.Source("doc['numberOfCommits'].value * 1.2"))
				.Missing(0)
			);

		protected override AggregationDictionary InitializerAggs =>
			new PercentilesAggregation("commits_outlier", Field<Project>(p => p.NumberOfCommits))
			{
				Percents = new[] { 95, 99, 99.9 },
				Method = new HDRHistogramMethod
				{
					NumberOfSignificantValueDigits = 3
				},
				Script = new InlineScript("doc['numberOfCommits'].value * 1.2"),
				Missing = 0
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsOutlier = response.Aggregations.Percentiles("commits_outlier");
			commitsOutlier.Should().NotBeNull();
			commitsOutlier.Items.Should().NotBeNullOrEmpty();
			foreach (var item in commitsOutlier.Items)
				item.Value.Should().BeGreaterThan(0);
		}
	}

	// hide
	public class PercentilesAggregationNonKeyedValuesUsageTests : AggregationUsageTestBase
	{
		public PercentilesAggregationNonKeyedValuesUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits_outlier = new
			{
				percentiles = new
				{
					field = "numberOfCommits",
					percents = new[] { 95.0, 99.0, 99.9 },
					hdr = new
					{
						number_of_significant_value_digits = 3
					},
					script = new
					{
						source = "doc['numberOfCommits'].value * 1.2",
					},
					missing = 0.0,
					keyed = false
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Percentiles("commits_outlier", pr => pr
				.Field(p => p.NumberOfCommits)
				.Percents(95, 99, 99.9)
				.Method(m => m
					.HDRHistogram(hdr => hdr
						.NumberOfSignificantValueDigits(3)
					)
				)
				.Script(ss => ss.Source("doc['numberOfCommits'].value * 1.2"))
				.Missing(0)
				.Keyed(false)
			);

		protected override AggregationDictionary InitializerAggs =>
			new PercentilesAggregation("commits_outlier", Field<Project>(p => p.NumberOfCommits))
			{
				Percents = new[] { 95, 99, 99.9 },
				Method = new HDRHistogramMethod
				{
					NumberOfSignificantValueDigits = 3
				},
				Script = new InlineScript("doc['numberOfCommits'].value * 1.2"),
				Missing = 0,
				Keyed = false
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsOutlier = response.Aggregations.Percentiles("commits_outlier");
			commitsOutlier.Should().NotBeNull();
			commitsOutlier.Items.Should().NotBeNullOrEmpty();
			foreach (var item in commitsOutlier.Items)
				item.Value.Should().BeGreaterThan(0);
		}
	}
}

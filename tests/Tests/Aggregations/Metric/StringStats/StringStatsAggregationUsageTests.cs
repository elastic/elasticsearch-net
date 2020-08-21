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

namespace Tests.Aggregations.Metric.StringStats
{
	[SkipVersion("<7.6.0", "Available in 7.6.0 with at least basic license level")]
	public class StringStatsAggregationUsageTests : AggregationUsageTestBase
	{
		public StringStatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			name_stats = new
			{
				string_stats = new
				{
					field = "name"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.StringStats("name_stats", st => st
				.Field(p => p.Name)
			);

		protected override AggregationDictionary InitializerAggs =>
			new StringStatsAggregation("name_stats", Field<Project>(p => p.Name));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.StringStats("name_stats");
			commitStats.Should().NotBeNull();
			commitStats.AverageLength.Should().BeGreaterThan(0);
			commitStats.MaxLength.Should().BeGreaterThan(0);
			commitStats.MinLength.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Distribution.Should().NotBeNull().And.BeEmpty();
		}
	}

	// hide
	[SkipVersion("<7.6.0", "Available in 7.6.0 with at least basic license level")]
	public class StringStatsWithDistributionAggregationUsageTests : AggregationUsageTestBase
	{
		public StringStatsWithDistributionAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			name_stats = new
			{
				string_stats = new
				{
					field = "name",
					show_distribution = true
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.StringStats("name_stats", st => st
				.Field(p => p.Name)
				.ShowDistribution()
			);

		protected override AggregationDictionary InitializerAggs =>
			new StringStatsAggregation("name_stats", Field<Project>(p => p.Name))
			{
				ShowDistribution = true
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.StringStats("name_stats");
			commitStats.Should().NotBeNull();
			commitStats.AverageLength.Should().BeGreaterThan(0);
			commitStats.MaxLength.Should().BeGreaterThan(0);
			commitStats.MinLength.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Distribution.Should().NotBeNull().And.NotBeEmpty();
		}
	}
}

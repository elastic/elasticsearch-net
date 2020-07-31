// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.ExtendedStats
{
	public class ExtendedStatsAggregationUsageTests : AggregationUsageTestBase
	{
		public ExtendedStatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commit_stats = new
			{
				extended_stats = new
				{
					field = "numberOfCommits",
					sigma = 1d
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.ExtendedStats("commit_stats", es => es
				.Field(p => p.NumberOfCommits)
				.Sigma(1)
			);

		protected override AggregationDictionary InitializerAggs =>
			new ExtendedStatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits))
			{
				Sigma = 1
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.ExtendedStats("commit_stats");
			commitStats.Should().NotBeNull();
			commitStats.Average.Should().BeGreaterThan(0);
			commitStats.Max.Should().BeGreaterThan(0);
			commitStats.Min.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Sum.Should().BeGreaterThan(0);
			commitStats.SumOfSquares.Should().BeGreaterThan(0);
			commitStats.StdDeviation.Should().BeGreaterThan(0);
			commitStats.StdDeviationBounds.Should().NotBeNull();
			commitStats.StdDeviationBounds.Upper.Should().BeGreaterThan(0);
			commitStats.StdDeviationBounds.Lower.Should().NotBe(0);

			// hide
			if (TestClient.Configuration.InRange(">=7.9.0"))
			{
				commitStats.VariancePopulation.Should().BeGreaterThan(0);
				commitStats.VarianceSampling.Should().BeGreaterThan(0);
				commitStats.StdDeviationPopulation.Should().BeGreaterThan(0);
				commitStats.StdDeviationSampling.Should().BeGreaterThan(0);
				commitStats.StdDeviationBounds.UpperPopulation.Should().BeGreaterThan(0);
				commitStats.StdDeviationBounds.UpperSampling.Should().NotBe(0);
				commitStats.StdDeviationBounds.LowerPopulation.Should().NotBe(0);
				commitStats.StdDeviationBounds.LowerSampling.Should().NotBe(0);
			}
		}
	}

	/// <summary>
	/// Asserts that stats sum is 0 (and not null) when matching document count is 0
	/// </summary>
	// hide
	public class ExtendedStatsAggregationUsageDocCountZeroTests : AggregationUsageTestBase
	{
		public ExtendedStatsAggregationUsageDocCountZeroTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		// a query that no docs will match
		protected override QueryContainer QueryScope => base.QueryScope &&
			new TermQuery { Field = Field<Project>(f => f.Branches), Value = "non-existent branch name" };

		protected override object QueryScopeJson { get; } = new
		{
			@bool = new
			{
				must = new object[]
				{
					new { term = new { type = new { value = Project.TypeName } } },
					new { term = new { branches = new { value = "non-existent branch name" } } },
				}
			}

		};

		protected override object AggregationJson => new
		{
			commit_stats = new
			{
				extended_stats = new
				{
					field = "numberOfCommits",
					sigma = 1d
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.ExtendedStats("commit_stats", es => es
				.Field(p => p.NumberOfCommits)
				.Sigma(1)
			);

		protected override AggregationDictionary InitializerAggs =>
			new ExtendedStatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits))
			{
				Sigma = 1
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.ExtendedStats("commit_stats");
			commitStats.Count.Should().Be(0);
			commitStats.Sum.Should().Be(0);
			commitStats.Should().NotBeNull();
			commitStats.Average.Should().BeNull();
			commitStats.Max.Should().BeNull();
			commitStats.Min.Should().BeNull();
			commitStats.SumOfSquares.Should().BeNull();
			commitStats.Variance.Should().BeNull();
			commitStats.StdDeviation.Should().BeNull();
			commitStats.StdDeviationBounds.Upper.Should().BeNull();
			commitStats.StdDeviationBounds.Lower.Should().BeNull();
		}
	}
}

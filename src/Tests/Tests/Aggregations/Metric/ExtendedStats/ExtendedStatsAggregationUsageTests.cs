using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
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
		}
	}
}

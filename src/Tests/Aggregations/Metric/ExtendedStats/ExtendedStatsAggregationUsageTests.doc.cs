using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.ExtendedStats
{
	public class ExtendedStatsAggregationUsageTests : AggregationUsageTestBase
	{
		public ExtendedStatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commit_stats = new
				{
					extended_stats = new
					{
						field = "numberOfCommits"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.ExtendedStats("commit_stats", es => es
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ExtendedStatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitStats = response.Aggs.ExtendedStats("commit_stats");
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

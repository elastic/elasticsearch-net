using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Stats
{
	public class StatsAggregationUsageTests : AggregationUsageTestBase
	{
		public StatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commit_stats = new
			{
				stats = new
				{
					field = "numberOfCommits"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Stats("commit_stats", st => st
				.Field(p => p.NumberOfCommits)
			);

		protected override AggregationDictionary InitializerAggs =>
			new StatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitStats = response.Aggregations.Stats("commit_stats");
			commitStats.Should().NotBeNull();
			commitStats.Average.Should().BeGreaterThan(0);
			commitStats.Max.Should().BeGreaterThan(0);
			commitStats.Min.Should().BeGreaterThan(0);
			commitStats.Count.Should().BeGreaterThan(0);
			commitStats.Sum.Should().BeGreaterThan(0);
		}
	}
}

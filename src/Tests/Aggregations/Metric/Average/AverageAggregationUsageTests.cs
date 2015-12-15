using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.Aggregations.Metric.Average
{
	public class AverageAggregationUsageTests : AggregationUsageTestBase
	{
		public AverageAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				average_commits = new
				{
					avg = new
					{
						field = Field<Project>(p => p.NumberOfCommits),
						missing = 10.0,
						script = new
						{
							inline = "_value * 1.2",
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Average("average_commits", avg => avg
					.Field(p => p.NumberOfCommits)
					.Missing(10)
					.Script("_value * 1.2")
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new AverageAggregation("average_commits", Field<Project>(p => p.NumberOfCommits))
				{
					Missing = 10,
					Script = new InlineScript("_value * 1.2")
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitsAvg = response.Aggs.Average("average_commits");
			commitsAvg.Should().NotBeNull();
			commitsAvg.Value.Should().BeGreaterThan(0);
		}
	}
}

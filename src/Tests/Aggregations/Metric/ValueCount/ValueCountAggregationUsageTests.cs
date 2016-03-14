using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.ValueCount
{
	public class ValueCountAggregationUsageTests : AggregationUsageTestBase
	{
		public ValueCountAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commit_count = new
				{
					value_count = new
					{
						field = "numberOfCommits"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.ValueCount("commit_count", c => c
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ValueCountAggregation("commit_count", Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitCount = response.Aggs.ValueCount("commit_count");
			commitCount.Should().NotBeNull();
			commitCount.Value.Should().BeGreaterThan(0);
		}
	}
}

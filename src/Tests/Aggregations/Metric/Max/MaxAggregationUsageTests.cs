using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Max
{
	public class MaxAggregationUsageTests : AggregationUsageTestBase
	{
		public MaxAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				max_commits = new
				{
					max = new
					{
						field = "numberOfCommits"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Max("max_commits", m => m
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new MaxAggregation("max_commits", Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var max = response.Aggs.Max("max_commits");
			max.Should().NotBeNull();
			max.Value.Should().BeGreaterThan(0);
		}
	}
}

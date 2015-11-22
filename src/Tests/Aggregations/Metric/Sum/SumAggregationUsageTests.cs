using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;
using FluentAssertions;

namespace Tests.Aggregations.Metric.Sum
{
	public class SumAggregationUsageTests : AggregationUsageTestBase
	{
		public SumAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commits_sum = new
				{
					sum = new
					{
						field = Field<Project>(p => p.NumberOfCommits)
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Sum("commits_sum", sm => sm
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new SumAggregation("commits_sum", Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitsSum = response.Aggs.Sum("commits_sum");
			commitsSum.Should().NotBeNull();
			commitsSum.Value.Should().BeGreaterThan(0);
		}
	}
}

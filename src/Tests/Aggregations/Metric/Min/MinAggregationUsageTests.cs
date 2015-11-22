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

namespace Tests.Aggregations.Metric.Min
{
	public class MinAggregationUsageTests : AggregationUsageTestBase
	{
		public MinAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				min_commits = new
				{
					min = new
					{
						field = Field<Project>(p => p.NumberOfCommits)
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Min("min_commits", m => m
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new MinAggregation("min_commits", Field<Project>(p => p.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var min = response.Aggs.Max("min_commits");
			min.Should().NotBeNull();
			min.Value.Should().BeGreaterThan(0);
		}
	}
}

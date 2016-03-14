using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Range
{
	public class RangeAggregationUsageTests : AggregationUsageTestBase
	{
		public RangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commit_ranges = new
				{
					range = new
					{
						field = "numberOfCommits",
						ranges = new object[]
						{
							new { to = 100.0 },
							new { from = 100.0, to = 500.0 },
							new { from = 500.0 }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Range("commit_ranges", ra => ra
					.Field(p => p.NumberOfCommits)
					.Ranges(
						r => r.To(100),
						r => r.From(100).To(500),
						r => r.From(500)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new RangeAggregation("commit_ranges")
				{
					Field = Field<Project>(p => p.NumberOfCommits),
					Ranges = new List<Nest.Range>
					{
						{ new Nest.Range { To = 100 } },
						{ new Nest.Range { From = 100, To = 500 } },
						{ new Nest.Range { From = 500 } }
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitRanges = response.Aggs.Range("commit_ranges");
			commitRanges.Should().NotBeNull();
			commitRanges.Buckets.Count.Should().Be(3);
			commitRanges.Buckets.Where(r => r.Key == "*-100.0").FirstOrDefault().Should().NotBeNull();
			commitRanges.Buckets.Where(r => r.Key == "100.0-500.0").FirstOrDefault().Should().NotBeNull();
			commitRanges.Buckets.Where(r => r.Key == "500.0-*").FirstOrDefault().Should().NotBeNull();
		}
	}
}
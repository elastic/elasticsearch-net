using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Bucket.Terms
{
	[SkipVersion("<5.2.0", "Partitioning term aggregations responses is a new feature in 5.2.0")]
	public class PartitionTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public PartitionTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			size = 0,
			aggs = new
			{
				commits = new
				{
					terms = new
					{
						field = "numberOfCommits",
						size = 2,
						include = new
						{
							partition = 0,
							num_partitions = 50
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(a => a
				.Terms("commits", st => st
					.Field(p => p.NumberOfCommits)
					.Include(partion: 0, numberOfPartitions: 50)
					.Size(2)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Size = 0,
				Aggregations = new TermsAggregation("commits")
				{
					Field = Infer.Field<Project>(p => p.NumberOfCommits),
					Include = new TermsIncludeExclude
					{
						Partition = 0,
						NumberOfPartitions = 50
					},
					Size = 2
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commits = response.Aggs.Terms<int>("commits");
			commits.Should().NotBeNull();
			commits.DocCountErrorUpperBound.Should().HaveValue();
			commits.SumOtherDocCount.Should().HaveValue();
			commits.Buckets.Should().NotBeNull();
			commits.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in commits.Buckets)
			{
				item.Key.Should().BeGreaterThan(0);
				item.DocCount.Should().BeGreaterOrEqualTo(1);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Terms
{
	public class TermsAggregationUsageTests : AggregationUsageTestBase
	{
		public TermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				states = new
				{
					meta = new
					{
						foo = "bar"
					},
					terms = new
					{
						field = "state",
						min_doc_count = 2,
						size = 5,
						shard_size = 100,
						execution_hint = "map",
						missing = "n/a",
						script = new
						{
							inline = "'State of Being: '+_value",
							lang = "groovy"
						},
						order = new object[]
						{
							new { _term = "asc" },
							new { _count = "desc" }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Terms("states", st => st
					.Field(p => p.State)
					.MinimumDocumentCount(2)
					.Size(5)
					.ShardSize(100)
					.ExecutionHint(TermsAggregationExecutionHint.Map)
					.Missing("n/a")
					.Script(ss => ss.Inline("'State of Being: '+_value").Lang("groovy"))
					.Order(TermsOrder.TermAscending)
					.Order(TermsOrder.CountDescending)
					.Meta(m => m
						.Add("foo", "bar")
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State),
					MinimumDocumentCount = 2,
					Size = 5,
					ShardSize = 100,
					ExecutionHint = TermsAggregationExecutionHint.Map,
					Missing = "n/a",
					Script = new InlineScript("'State of Being: '+_value") { Lang = "groovy" },
					Order = new List<TermsOrder>
					{
						TermsOrder.TermAscending,
						TermsOrder.CountDescending
					},
					Meta = new Dictionary<string, object>
					{
						{ "foo", "bar" }
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var states = response.Aggs.Terms("states");
			states.Should().NotBeNull();
			states.DocCountErrorUpperBound.Should().HaveValue();
			states.SumOtherDocCount.Should().HaveValue();
			states.Buckets.Should().NotBeNull();
			states.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in states.Buckets)
			{
				item.Key.Should().NotBeNullOrEmpty();
				item.DocCount.Should().BeGreaterOrEqualTo(1);
			}
			states.Meta.Should().NotBeNull().And.HaveCount(1);
			states.Meta["foo"].Should().Be("bar");
		}
	}

	public class NumericTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public NumericTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commits = new
				{
				
					terms = new
					{
						field = "numberOfCommits"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Terms("commits", st => st
					.Field(p => p.NumberOfCommits)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new TermsAggregation("commits")
				{
					Field = Field<Project>(p => p.NumberOfCommits),
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

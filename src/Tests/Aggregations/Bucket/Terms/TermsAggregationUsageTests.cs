using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Terms
{
	/**
	 * A multi-bucket value source based aggregation where buckets are dynamically built - one per unique value.
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-terms-aggregation.html[terms aggregation] for more detail.
	 */
	public class TermsAggregationUsageTests : AggregationUsageTestBase
	{
		public TermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			size = 0,
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
						},
						order = new object[]
						{
							new {_term = "asc"},
							new {_count = "desc"}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(a => a
				.Terms("states", st => st
					.Field(p => p.State)
					.MinimumDocumentCount(2)
					.Size(5)
					.ShardSize(100)
					.ExecutionHint(TermsAggregationExecutionHint.Map)
					.Missing("n/a")
					.Script(ss => ss.Inline("'State of Being: '+_value"))
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
				Size = 0,
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State),
					MinimumDocumentCount = 2,
					Size = 5,
					ShardSize = 100,
					ExecutionHint = TermsAggregationExecutionHint.Map,
					Missing = "n/a",
					Script = new InlineScript("'State of Being: '+_value"),
					Order = new List<TermsOrder>
					{
						TermsOrder.TermAscending,
						TermsOrder.CountDescending
					},
					Meta = new Dictionary<string, object>
					{
						{"foo", "bar"}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
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

	/**
	 * [float]
	 * [[terms-pattern-filter]]
	 * == Filtering with a regular expression pattern
	 *
	 * Using terms aggregation with filtering to include values using a regular expression pattern
	 *
	 */
	public class TermsAggregationIncludePatternUsageTests : AggregationUsageTestBase
	{
		public TermsAggregationIncludePatternUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
		}

		protected override object ExpectJson => new
		{
			size = 0,
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
						field = "state.keyword",
						min_doc_count = 2,
						size = 5,
						shard_size = 100,
						execution_hint = "map",
						missing = "n/a",
						include = "(Stable|VeryActive)",
						order = new object[]
						{
							new {_term = "asc"},
							new {_count = "desc"}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(a => a
				.Terms("states", st => st
					.Field(p => p.State.Suffix("keyword"))
					.MinimumDocumentCount(2)
					.Size(5)
					.ShardSize(100)
					.ExecutionHint(TermsAggregationExecutionHint.Map)
					.Missing("n/a")
					.Include("(Stable|VeryActive)")
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
				Size = 0,
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State.Suffix("keyword")),
					MinimumDocumentCount = 2,
					Size = 5,
					ShardSize = 100,
					ExecutionHint = TermsAggregationExecutionHint.Map,
					Missing = "n/a",
					Include = new TermsInclude("(Stable|VeryActive)"),
					Order = new List<TermsOrder>
					{
						TermsOrder.TermAscending,
						TermsOrder.CountDescending
					},
					Meta = new Dictionary<string, object>
					{
						{"foo", "bar"}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
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

	/**
	 * [[terms-exact-value-filter]]
	 * [float]
	 * == Filtering with exact values
	 *
	 * Using terms aggregation with filtering to include only specific values
	 *
	 */
	public class TermsAggregationIncludeExactValuesUsageTests : AggregationUsageTestBase
	{
		public TermsAggregationIncludeExactValuesUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
		}

		protected override object ExpectJson => new
		{
			size = 0,
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
						field = "state.keyword",
						min_doc_count = 2,
						size = 5,
						shard_size = 100,
						execution_hint = "map",
						missing = "n/a",
						include = new[] {"Stable", "VeryActive"},
						order = new object[]
						{
							new {_term = "asc"},
							new {_count = "desc"}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(a => a
				.Terms("states", st => st
					.Field(p => p.State.Suffix("keyword"))
					.MinimumDocumentCount(2)
					.Size(5)
					.ShardSize(100)
					.ExecutionHint(TermsAggregationExecutionHint.Map)
					.Missing("n/a")
					.Include(new[] {StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString()})
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
				Size = 0,
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State.Suffix("keyword")),
					MinimumDocumentCount = 2,
					Size = 5,
					ShardSize = 100,
					ExecutionHint = TermsAggregationExecutionHint.Map,
					Missing = "n/a",
					Include = new TermsInclude(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() }),
					Order = new List<TermsOrder>
					{
						TermsOrder.TermAscending,
						TermsOrder.CountDescending
					},
					Meta = new Dictionary<string, object>
					{
						{"foo", "bar"}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
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

	/**
	 * [float]
	 * == Filtering with partitions
	 *
	 * A terms aggregation that uses partitioning to filter the terms that are returned in the response. Further terms
	 * can be returned by issuing additional requests with an incrementing `partition` number.
	 *
	 * [NOTE]
	 * --
	 * Partitioning is available only in Elasticsearch 5.2.0+
	 * --
	 */
	[SkipVersion("<5.2.0", "Partitioning term aggregations responses is a new feature in 5.2.0")]
	public class PartitionTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public PartitionTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
		}

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
						size = 5,
						include = new
						{
							partition = 0,
							num_partitions = 10
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
					.Include(partition: 0, numberOfPartitions: 10)
					.Size(5)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Size = 0,
				Aggregations = new TermsAggregation("commits")
				{
					Field = Infer.Field<Project>(p => p.NumberOfCommits),
					Include = new TermsInclude(0, 10),
					Size = 5
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
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

	/**
	 * [float]
	 * == Numeric fields
	 *
	 * A terms aggregation on a numeric field
	 */
	public class NumericTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public NumericTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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
						missing = -1,
						show_term_doc_count_error = true
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Index(DefaultSeeder.ProjectsAliasFilter)
			.Aggregations(a => a
				.Terms<int>("commits", st => st
					.Field(p => p.NumberOfCommits)
					.Missing(-1)
					.ShowTermDocCountError()
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>(DefaultSeeder.ProjectsAliasFilter)
			{
				Size = 0,
				Aggregations = new TermsAggregation<int>("commits")
				{
					Field = Field<Project>(p => p.NumberOfCommits),
					ShowTermDocCountError = true,
					Missing = -1
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
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
			commits.Buckets.Should().Contain(b => b.DocCountErrorUpperBound.HasValue);
		}
	}
}

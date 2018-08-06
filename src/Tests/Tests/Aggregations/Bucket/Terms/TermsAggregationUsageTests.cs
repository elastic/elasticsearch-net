using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
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

		protected override object AggregationJson => new
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
						source = "'State of Being: '+_value",
					},
					order = new object[]
					{
						new {_key = "asc"},
						new {_count = "desc"}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("states", st => st
				.Field(p => p.State)
				.MinimumDocumentCount(2)
				.Size(5)
				.ShardSize(100)
				.ExecutionHint(TermsAggregationExecutionHint.Map)
				.Missing("n/a")
				.Script(ss => ss.Source("'State of Being: '+_value"))
				.Order(o => o
					.KeyAscending()
					.CountDescending()
				)
				.Meta(m => m
					.Add("foo", "bar")
				)
			);


		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("states")
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
					TermsOrder.KeyAscending,
					TermsOrder.CountDescending
				},
				Meta = new Dictionary<string, object>
				{
					{"foo", "bar"}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var states = response.Aggregations.Terms("states");
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
		public TermsAggregationIncludePatternUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
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
						new {_key = "asc"},
						new {_count = "desc"}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("states", st => st
				.Field(p => p.State.Suffix("keyword"))
				.MinimumDocumentCount(2)
				.Size(5)
				.ShardSize(100)
				.ExecutionHint(TermsAggregationExecutionHint.Map)
				.Missing("n/a")
				.Include("(Stable|VeryActive)")
				.Order(o => o
					.KeyAscending()
					.CountDescending()
				)
				.Meta(m => m
					.Add("foo", "bar")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("states")
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
					TermsOrder.KeyAscending,
					TermsOrder.CountDescending
				},
				Meta = new Dictionary<string, object>
				{
					{"foo", "bar"}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var states = response.Aggregations.Terms("states");
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
		public TermsAggregationIncludeExactValuesUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
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
						new {_key = "asc"},
						new {_count = "desc"}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("states", st => st
				.Field(p => p.State.Suffix("keyword"))
				.MinimumDocumentCount(2)
				.Size(5)
				.ShardSize(100)
				.ExecutionHint(TermsAggregationExecutionHint.Map)
				.Missing("n/a")
				.Include(new[] {StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString()})
				.Order(o => o
					.KeyAscending()
					.CountDescending()
				)
				.Meta(m => m
					.Add("foo", "bar")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("states")
			{
				Field = Field<Project>(p => p.State.Suffix("keyword")),
				MinimumDocumentCount = 2,
				Size = 5,
				ShardSize = 100,
				ExecutionHint = TermsAggregationExecutionHint.Map,
				Missing = "n/a",
				Include = new TermsInclude(new[] {StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString()}),
				Order = new List<TermsOrder>
				{
					TermsOrder.KeyAscending,
					TermsOrder.CountDescending
				},
				Meta = new Dictionary<string, object>
				{
					{"foo", "bar"}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var states = response.Aggregations.Terms("states");
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
		public PartitionTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
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
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("commits", st => st
				.Field(p => p.NumberOfCommits)
				.Include(partition: 0, numberOfPartitions: 10)
				.Size(5)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("commits")
			{
				Field = Infer.Field<Project>(p => p.NumberOfCommits),
				Include = new TermsInclude(0, 10),
				Size = 5
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commits = response.Aggregations.Terms<int>("commits");
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
	public class NumericTermsAggregationUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		public NumericTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
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
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("commits", st => st
				.Field(p => p.NumberOfCommits)
				.Missing(-1)
				.ShowTermDocCountError()
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("commits")
			{
				Field = Field<Project>(p => p.NumberOfCommits),
				ShowTermDocCountError = true,
				Missing = -1
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commits = response.Aggregations.Terms<int>("commits");
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

	/**
	 * [float]
	 * == Nested terms aggregations
	 *
	 * A terms aggregation returns buckets that can contain more aggregations
	 */
	public class NestedTermsAggregationUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		public NestedTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits = new
			{
				terms = new { field = "numberOfCommits", },
				 aggs = new
				 {
					state = new
					{
						meta = new { x = "y" },
						terms = new { field = "state" }
					}
				},
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("commits", st => st
				.Field(p => p.NumberOfCommits)
				.Aggregations(aggs => aggs
					.Terms("state", t => t
						.Meta(m => m.Add("x", "y"))
						.Field(p => p.State)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("commits")
			{
				Field = Field<Project>(p => p.NumberOfCommits),
				Aggregations = new TermsAggregation("state")
				{
					Meta = new Dictionary<string, object> {{"x", "y"}},
					Field = Field<Project>(p => p.State),
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commits = response.Aggregations.Terms<int>("commits");
			commits.Should().NotBeNull();
			commits.DocCountErrorUpperBound.Should().HaveValue();
			commits.SumOtherDocCount.Should().HaveValue();
			commits.Buckets.Should().NotBeNull();
			commits.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in commits.Buckets)
			{
				item.Key.Should().BeGreaterThan(0);
				item.DocCount.Should().BeGreaterOrEqualTo(1);
				var states = item.Terms("state");
				states.Should().NotBeNull();
				states.Buckets.Should().NotBeEmpty();
				states.Meta.Should().NotBeEmpty("meta").And.ContainKey("x");
				foreach (var b in states.Buckets)
				{
					b.DocCount.Should().BeGreaterThan(0);
					b.Key.Should().NotBeNullOrEmpty();
				}
			}
		}
	}
	/**
	 * [float]
	 * == Typed Keys aggregations
	 *
	 * Starting with Elasticsearch 6.x you can provide a `typed_keys` parameter which will prefix all the aggregation names
	 * with the type of aggregation that is returned. The following modifies the previous nested terms aggregation and sends it again
	 * but this time with the `typed_keys` option set. The client should treat this in a an opaque fashion so let's assert that it does.
	 */

	public class TypedKeysTermsAggregationUsageTests : NestedTermsAggregationUsageTests
	{
		public TypedKeysTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => f => base.Fluent(f.TypedKeys());

		protected override SearchRequest<Project> Initializer
		{
			get
			{
				var r = base.Initializer;
				r.TypedKeys = true;
				return r;
			}
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.MultiTerms
{
	/**
	 * A multi-bucket value source based aggregation where buckets are dynamically built - one per unique set of values.
	 *
	 * See the Elasticsearch documentation on {ref_current}//search-aggregations-bucket-multi-terms-aggregation.html[multi terms aggregation] for more detail.
	 */
	[SkipVersion("<7.12.0", "Multi terms aggregation added in 7.12.0")]
	public class MultiTermsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public MultiTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }
		
		protected override object AggregationJson => new
		{
			states = new
			{
				meta = new
				{
					foo = "bar"
				},
				multi_terms = new
				{
					collect_mode = "breadth_first",
					terms = new object[]
					{
						new { field = "name" },
						new { field = "numberOfCommits", missing = 0 }
					},
					min_doc_count = 1,
					shard_min_doc_count = 1,
					size = 5,
					shard_size = 100,
					show_term_doc_count_error = true,
					order = new object[]
					{
						new { _key = "asc" },
						new { _count = "desc" }
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.MultiTerms("states", st => st
				.CollectMode(TermsAggregationCollectMode.BreadthFirst)
				.Terms(t => t.Field(f => f.Name), t => t.Field(f => f.NumberOfCommits).Missing(0))
				.MinimumDocumentCount(1)
				.Size(5)
				.ShardSize(100)
				.ShardMinimumDocumentCount(1)
				.ShowTermDocCountError(true)
				.Order(o => o
					.KeyAscending()
					.CountDescending()
				)
				.Meta(m => m
					.Add("foo", "bar")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new MultiTermsAggregation("states")
			{
				CollectMode = TermsAggregationCollectMode.BreadthFirst,
				Terms = new List<Term>
				{
					new() {Field = Field<Project>(f => f.Name) },
					new() {Field = Field<Project>(f => f.NumberOfCommits), Missing = 0 }
				},
				MinimumDocumentCount = 1,
				Size = 5,
				ShardSize = 100,
				ShardMinimumDocumentCount = 1,
				ShowTermDocCountError = true,
				Order = new List<TermsOrder>
				{
					TermsOrder.KeyAscending,
					TermsOrder.CountDescending
				},
				Meta = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var states = response.Aggregations.MultiTerms("states");
			states.Should().NotBeNull();
			states.DocCountErrorUpperBound.Should().HaveValue();
			states.SumOtherDocCount.Should().HaveValue();
			states.Buckets.Should().NotBeNull();
			states.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in states.Buckets)
			{
				item.Key.Should().NotBeNullOrEmpty();
				item.DocCount.Should().BeGreaterOrEqualTo(1);
				item.KeyAsString.Should().NotBeNullOrEmpty();
			}
			states.Meta.Should().NotBeNull().And.HaveCount(1);
			states.Meta["foo"].Should().Be("bar");
		}
	}
}

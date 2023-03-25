// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;

namespace Tests.Aggregations.Bucket;

public class MultiTermsAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public MultiTermsAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.MultiTerms("states", st => st
			.CollectMode(TermsAggregationCollectMode.BreadthFirst)
			.Terms(
				t => t.Field(f => f.Name),
				t => t.Field(f => f.NumberOfCommits).Missing(0))
			.MinDocCount(1)
			.Size(5)
			.ShardSize(100)
			.ShardMinDocCount(1)
			.ShowTermDocCountError(true)
			.Order(new []
			{
				new KeyValuePair<Field, SortOrder>("_key", SortOrder.Asc),
				new KeyValuePair<Field, SortOrder>("_count", SortOrder.Desc)
			})
			.Meta(m => m
				.Add("foo", "bar")
			)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new MultiTermsAggregation("states")
		{
			CollectMode = TermsAggregationCollectMode.BreadthFirst,
			Terms = new List<MultiTermLookup>
			{
				new() { Field = Infer.Field<Project>(f => f.Name) },
				new() { Field = Infer.Field<Project>(f => f.NumberOfCommits), Missing = 0 }
			},
			MinDocCount = 1,
			Size = 5,
			ShardSize = 100,
			ShardMinDocCount = 1,
			ShowTermDocCountError = true,
			Order = new[]
			{
				new KeyValuePair<Field, SortOrder>("_key", SortOrder.Asc),
				new KeyValuePair<Field, SortOrder>("_count", SortOrder.Desc)
			},
			Meta = new Dictionary<string, object>
			{
				{ "foo", "bar" }
			}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var states = response.Aggregations.GetMultiTerms("states");

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

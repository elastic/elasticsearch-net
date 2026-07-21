// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Aggregations.Bucket;

public class TermsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public TermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override bool VerifyJson => true;

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
					new { _key = "asc" },
					new { _count = "desc" }
				}
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Terms("states", st => st
			.Field(p => p.State)
			.MinDocCount(2)
			.Size(5)
			.ShardSize(100)
			.ExecutionHint(TermsAggregationExecutionHint.Map)
			.Missing("n/a")
			.Script(new Script(new InlineScript("'State of Being: '+_value")))
			.Order(new[]
			{
				AggregateOrder.KeyAscending,
				AggregateOrder.CountDescending
			})
			.Meta(m => m
				.Add("foo", "bar")
			)
		);

	protected override AggregationDictionary InitializerAggs =>
		new TermsAggregation("states")
		{
			Field = Field<Project>(p => p.State),
			MinDocCount = 2,
			Size = 5,
			ShardSize = 100,
			ExecutionHint = TermsAggregationExecutionHint.Map,
			Missing = "n/a",
			Script = new Script(new InlineScript("'State of Being: '+_value")),
			Order = new []
			{
				AggregateOrder.KeyAscending,
				AggregateOrder.CountDescending
			},
			Meta = new Dictionary<string, object>
			{
				{ "foo", "bar" }
			}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var states = response.Aggregations.GetStringTerms("states");
		states.Should().NotBeNull();
		states.DocCountErrorUpperBound.Should().HaveValue();
		states.SumOtherDocCount.Should().BeGreaterOrEqualTo(0);
		states.Buckets.Should().NotBeNull();

		var bucketsCollection = states.Buckets;

		bucketsCollection.Count.Should().BeGreaterThan(0);
		foreach (var item in bucketsCollection)
		{
			item.Key.ToString().Should().NotBeNullOrEmpty();
			item.DocCount.Should().BeGreaterOrEqualTo(1);
		}
		states.Meta.Should().NotBeNull().And.HaveCount(1);
		states.Meta["foo"].Should().Be("bar");
	}
}

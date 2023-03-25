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

public class RareTermsAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public RareTermsAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool CompareJsonStrings => true;

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.RareTerms("names", st => st
			.Field(p => p.Name)
			.Missing("n/a")
			.MaxDocCount(5)
			.Precision(0.001)
			.Meta(m => m
				.Add("foo", "bar")
			)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new RareTermsAggregation("names")
		{
			Field = Infer.Field<Project>(p => p.Name),
			MaxDocCount = 5,
			Precision = 0.001,
			Missing = "n/a",
			Meta = new Dictionary<string, object> { { "foo", "bar" } }
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();

		var rareTerms = response.Aggregations.GetStringRareTerms("names");

		rareTerms.Should().NotBeNull();
		rareTerms.Buckets.Should().NotBeNull();
		rareTerms.Buckets.Count.Should().BeGreaterThan(0);

		foreach (var item in rareTerms.Buckets)
		{
			item.Key.Should().NotBeNullOrEmpty();
			item.DocCount.Should().BeGreaterOrEqualTo(1);
		}

		rareTerms.Meta.Should().NotBeNull().And.HaveCount(1);
		rareTerms.Meta["foo"].Should().Be("bar");
	}
}

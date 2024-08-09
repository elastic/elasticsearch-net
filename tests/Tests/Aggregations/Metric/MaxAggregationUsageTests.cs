// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;


namespace Tests.Aggregations.Metric;

public class MaxAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public MaxAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		max_commits = new
		{
			max = new
			{
				field = "numberOfCommits"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Max("max_commits", m => m
			.Field(p => p.NumberOfCommits)
		);

	protected override AggregationDictionary InitializerAggs =>
		new MaxAggregation("max_commits", Field<Project>(p => p.NumberOfCommits));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var max = response.Aggregations.GetMax("max_commits");
		max.Should().NotBeNull();
		max.Value.Should().BeGreaterThan(0);
	}
}

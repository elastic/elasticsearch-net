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

public class ValueCountAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public ValueCountAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		commit_count = new
		{
			value_count = new
			{
				field = "numberOfCommits"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.ValueCount("commit_count", c => c
			.Field(p => p.NumberOfCommits)
		);

	protected override AggregationDictionary InitializerAggs =>
		new ValueCountAggregation("commit_count", Field<Project>(p => p.NumberOfCommits));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var commitCount = response.Aggregations.GetValueCount("commit_count");
		commitCount.Should().NotBeNull();
		commitCount.Value.Should().BeGreaterThan(0);
	}
}

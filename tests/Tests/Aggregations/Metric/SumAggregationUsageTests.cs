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

public class SumAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public SumAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		commits_sum = new
		{
			sum = new
			{
				field = "numberOfCommits"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Sum("commits_sum", sm => sm
			.Field(p => p.NumberOfCommits)
		);

	protected override AggregationDictionary InitializerAggs =>
		new SumAggregation("commits_sum", Field<Project>(p => p.NumberOfCommits));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var commitsSum = response.Aggregations.GetSum("commits_sum");
		commitsSum.Should().NotBeNull();
		commitsSum.Value.Should().BeGreaterThan(0);
	}
}

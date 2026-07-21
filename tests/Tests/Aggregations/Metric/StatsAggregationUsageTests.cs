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

public class StatsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public StatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		commit_stats = new
		{
			stats = new
			{
				field = "numberOfCommits"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Stats("commit_stats", st => st
			.Field(p => p.NumberOfCommits)
		);

	protected override AggregationDictionary InitializerAggs =>
		new StatsAggregation("commit_stats", Field<Project>(p => p.NumberOfCommits));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var commitStats = response.Aggregations.GetStats("commit_stats");
		commitStats.Should().NotBeNull();
		commitStats.Avg.Should().BeGreaterThan(0);
		commitStats.Max.Should().BeGreaterThan(0);
		commitStats.Min.Should().BeGreaterThan(0);
		commitStats.Count.Should().BeGreaterThan(0);
		commitStats.Sum.Should().BeGreaterThan(0);
	}
}


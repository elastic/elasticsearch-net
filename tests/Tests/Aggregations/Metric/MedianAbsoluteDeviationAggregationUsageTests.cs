// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Metric;

public class MedianAbsoluteDeviationAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public MedianAbsoluteDeviationAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		average_commits = new
		{
			avg = new
			{
				field = "numberOfCommits",
			}
		},
		commit_variability = new
		{
			median_absolute_deviation = new
			{
				field = "numberOfCommits"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Avg("average_commits", avg => avg
			.Field(p => p.NumberOfCommits)
		)
		.MedianAbsoluteDeviation("commit_variability", m => m
			.Field(f => f.NumberOfCommits)
		);

	protected override AggregationDictionary InitializerAggs =>
		new AverageAggregation("average_commits", Infer.Field<Project>(p => p.NumberOfCommits)) &&
		new MedianAbsoluteDeviationAggregation("commit_variability", Infer.Field<Project>(p => p.NumberOfCommits));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var medianAbsoluteDeviation = response.Aggregations.GetMedianAbsoluteDeviation("commit_variability");
		medianAbsoluteDeviation.Should().NotBeNull();
		medianAbsoluteDeviation.Value.Should().BeGreaterThan(0);
	}
}

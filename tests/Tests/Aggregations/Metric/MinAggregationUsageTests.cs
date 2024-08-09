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

public class MinAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public MinAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		min_last_activity = new
		{
			min = new
			{
				field = "lastActivity",
				format = "yyyy"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Min("min_last_activity", m => m
			.Field(p => p.LastActivity)
			.Format("yyyy")
		);

	protected override AggregationDictionary InitializerAggs =>
		new MinAggregation("min_last_activity", Field<Project>(p => p.LastActivity)) { Format = "yyyy" };

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var min = response.Aggregations.GetMin("min_last_activity");
		min.Should().NotBeNull();
		min.Value.Should().BeGreaterThan(0);
		min.ValueAsString.Should().NotBeNullOrEmpty();
	}
}

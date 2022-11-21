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

public class CardinalityAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public CardinalityAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		state_count = new
		{
			cardinality = new
			{
				field = "state",
				precision_threshold = 100
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Cardinality("state_count", c => c
			.Field(p => p.State)
			.PrecisionThreshold(100)
		);

	protected override AggregationDictionary InitializerAggs =>
		new CardinalityAggregation("state_count", Field<Project>(p => p.State))
		{
			PrecisionThreshold = 100
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var projectCount = response.Aggregations.GetCardinality("state_count");
		projectCount.Should().NotBeNull();
		projectCount.Value.Should().BeGreaterOrEqualTo(1);
	}
}

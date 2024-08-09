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

public class StringStatsAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public StringStatsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		name_stats = new
		{
			string_stats = new
			{
				field = "name"
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.StringStats("name_stats", st => st
			.Field(p => p.Name)
		);

	protected override AggregationDictionary InitializerAggs =>
		new StringStatsAggregation("name_stats", Field<Project>(p => p.Name));

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var commitStats = response.Aggregations.GetStringStats("name_stats");
		commitStats.Should().NotBeNull();
		commitStats.AvgLength.Should().BeGreaterThan(0);
		commitStats.MaxLength.Should().BeGreaterThan(0);
		commitStats.MinLength.Should().BeGreaterThan(0);
		commitStats.Count.Should().BeGreaterThan(0);
		commitStats.Distribution.Should().BeNull();
	}
}

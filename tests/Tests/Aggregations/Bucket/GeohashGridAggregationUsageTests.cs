// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using System.Linq;

namespace Tests.Aggregations.Bucket;

public class GeohashGridAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public GeohashGridAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.GeohashGrid("my_geohash_grid", g => g
			.Field(p => p.LocationPoint)
			.Precision(new GeohashPrecision(3))
			.Size(1000)
			.ShardSize(100)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new GeohashGridAggregation("my_geohash_grid")
		{
			Field = Infer.Field<Project>(p => p.LocationPoint),
			Precision = new GeohashPrecision(3),
			Size = 1000,
			ShardSize = 100
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var geohashGrid = response.Aggregations.GetGeohashGrid("my_geohash_grid");

		geohashGrid.Should().NotBeNull();
		var firstBucket = geohashGrid.Buckets.First();
		firstBucket.Key.Should().NotBeNullOrWhiteSpace();
		firstBucket.DocCount.Should().BeGreaterThan(0);
	}
}

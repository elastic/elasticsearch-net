// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoTileGrid
{
	public class GeoTileGridAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoTileGridAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			my_geotile = new
			{
				geotile_grid = new
				{
					field = "locationPoint",
					precision = 3,
					size = 1000,
					shard_size = 100
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoTile("my_geotile", g => g
				.Field(p => p.LocationPoint)
				.Precision(GeoTilePrecision.Precision3)
				.Size(1000)
				.ShardSize(100)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoTileGridAggregation("my_geotile")
			{
				Field = Field<Project>(p => p.LocationPoint),
				Precision = GeoTilePrecision.Precision3,
				Size = 1000,
				ShardSize = 100
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var myGeoTileGrid = response.Aggregations.GeoTile("my_geotile");
			myGeoTileGrid.Should().NotBeNull();
			var firstBucket = myGeoTileGrid.Buckets.First();
			firstBucket.Key.Should().NotBeNullOrWhiteSpace();
			firstBucket.DocCount.Should().BeGreaterThan(0);
		}
	}
}

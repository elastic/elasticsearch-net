using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.GeoHashGrid
{
	public class GeoHashGridAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoHashGridAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			my_geohash_grid = new
			{
				geohash_grid = new
				{
					field = "locationPoint",
					precision = 3,
					size = 1000,
					shard_size = 100
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoHash("my_geohash_grid", g => g
				.Field(p => p.LocationPoint)
				.GeoHashPrecision(GeoHashPrecision.Precision3)
				.Size(1000)
				.ShardSize(100)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoHashGridAggregation("my_geohash_grid")
			{
				Field = Field<Project>(p => p.LocationPoint),
				Precision = GeoHashPrecision.Precision3,
				Size = 1000,
				ShardSize = 100
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var myGeoHashGrid = response.Aggregations.GeoHash("my_geohash_grid");
			myGeoHashGrid.Should().NotBeNull();
		}
	}
}

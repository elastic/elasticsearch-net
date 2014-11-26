using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
    [TestFixture]
    public class GeoHashCellFilterTests : ParseFilterTestsBase
    {
        [Test]
        [TestCase("cacheName", "cacheKey", true)]
        public void GeoHashCell_LatLong_Precision_Deserializes(string cacheName, string cacheKey, bool cache)
        {
            var geoHashCellFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
                f => f.GeoHashCell,
                f => f.GeoHashCell(p => p.Origin, gh => gh
                    .Location(13.408, 52.5186)
                    .Precision(3)
                    .Neighbors(true)
                    )
                );

            geoHashCellFilter.Field.Should().Be("origin");
            geoHashCellFilter.Location.Should().Be("{ lat: 13.408, lon: 52.5186 }");
            geoHashCellFilter.Precision.Should().Be("3");
            geoHashCellFilter.Unit.Should().BeNull();
            geoHashCellFilter.Neighbors.Should().Be(true);
        }

        [Test]
        [TestCase("cacheName", "cacheKey", true)]
        public void GeoHashCell_LatLon_Distance_Deserializes(string cacheName, string cacheKey, bool cache)
        {
            var geoHashCellFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
                f => f.GeoHashCell,
                f => f.GeoHashCell(p => p.Origin, gh => gh
                    .Location(13.408, 52.5186)
                    .Distance(3, GeoUnit.Kilometers)
                    .Neighbors(true)
                    )
                );

            geoHashCellFilter.Field.Should().Be("origin");
            geoHashCellFilter.Location.Should().Be("{ lat: 13.408, lon: 52.5186 }");
            geoHashCellFilter.Precision.Should().Be("3");
            geoHashCellFilter.Unit.Should().Be(GeoUnit.Kilometers);
            geoHashCellFilter.Neighbors.Should().Be(true);
        }

        [Test]
        [TestCase("cacheName", "cacheKey", true)]
        public void GeoHashCell_Geohash_Deserializes(string cacheName, string cacheKey, bool cache)
        {
            var geoHashCellFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
                f => f.GeoHashCell,
                f => f.GeoHashCell(p => p.Origin, gh => gh
                    .Location("u30")
                    )
                );

            geoHashCellFilter.Field.Should().Be("origin");
            geoHashCellFilter.Location.Should().Be("u30");
            geoHashCellFilter.Neighbors.Should().Be(false);
        }
    }

}

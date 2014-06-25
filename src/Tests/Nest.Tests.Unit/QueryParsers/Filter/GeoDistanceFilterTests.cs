using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoDistanceFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistance_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoDistance,
				f=>f.GeoDistance(p=>p.Origin, gd=>gd
					.Distance(1.0, GeoUnit.Kilometers)
					.Location(2.1, 4.1)
					.Optimize(GeoOptimizeBBox.indexed)
					)
				);

			geoDistanceFilter.Field.Should().Be("origin");
			geoDistanceFilter.Location.Should().Be("2.1, 4.1");
			geoDistanceFilter.OptimizeBoundingBox.Should().Be(GeoOptimizeBBox.indexed);
		}
		
	}
}
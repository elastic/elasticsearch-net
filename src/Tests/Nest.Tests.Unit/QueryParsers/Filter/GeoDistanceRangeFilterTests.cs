using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoDistanceRangeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistanceRange_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceRangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoDistanceRange,
				f=>f.GeoDistanceRange(p=>p.Origin, d=>d
					.Location(Lat: 40, Lon: -70)
					.Distance(From: 12, To: 200, Unit: GeoUnit.km)
					.FromExclusive()
					.ToExclusive()
					.DistanceType(GeoDistanceType.arc)
					.Optimize(GeoOptimizeBBox.memory)
					)
				);

			geoDistanceRangeFilter.Field.Should().Be("origin");

			var from = (double)(geoDistanceRangeFilter.From);
			from.Should().Be(12);
			geoDistanceRangeFilter.Unit.Should().Be(GeoUnit.km);
			geoDistanceRangeFilter.DistanceType.Should().Be(GeoDistanceType.arc);
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistanceRange_FromString_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceRangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoDistanceRange,
				f=>f.GeoDistanceRange(p=>p.Origin, d=>d
					.Location(Lat: 40, Lon: -70)
					.Distance(From: "12km", To:"15km")
					.FromExclusive()
					.ToExclusive()
					.DistanceType(GeoDistanceType.arc)
					.Optimize(GeoOptimizeBBox.memory)
					)
				);
			geoDistanceRangeFilter.Field.Should().Be("origin");
			var from = (string)(geoDistanceRangeFilter.From);
			from.Should().Be("12km");
			geoDistanceRangeFilter.DistanceType.Should().Be(GeoDistanceType.arc);
		}
		
	}
}
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoPolygonFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoPolygon_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoPolygonFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoPolygon,
				f=>f.GeoPolygon(p => p.Origin, new List<Tuple<double, double>>
				{
					Tuple.Create(30.0, -80.0), Tuple.Create(20.0, -90.0)
				})
				);

			geoPolygonFilter.Field.Should().Be("origin");
			geoPolygonFilter.Points.Should().BeEquivalentTo(new []{"30, -80", "20, -90"});
		}
		
	}
}
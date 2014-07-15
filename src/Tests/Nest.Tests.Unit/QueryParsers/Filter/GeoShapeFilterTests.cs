using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoShapeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShape_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeEnvelope(p => p.Origin, d => d
					.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
				)
			);

			geoBaseShapeFilter.Field.Should().Be("origin");
			var geoShapeFilter = geoBaseShapeFilter as IGeoShapeEnvelopeFilter;
			geoShapeFilter.Should().NotBeNull();
			geoShapeFilter.Shape.Should().NotBeNull();
			geoShapeFilter.Shape.Type.Should().Be("envelope");
		}
		
	}
}
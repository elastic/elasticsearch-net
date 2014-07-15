using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoShapeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeEnvelope_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeEnvelope(p => p.Origin, d => d
					.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeEnvelopeFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("envelope");
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeCircle_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] { -45.0, 45.0 };

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeCircle(p => p.Origin, d => d
					.Coordinates(coordinates)
					.Radius("100m")
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeCircleFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("circle");
			filter.Shape.Radius.Should().Be("100m");
			filter.Shape.Coordinates.Should().BeEquivalentTo(coordinates);
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeLineString_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } };

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeLineString(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeLineStringFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("linestring");
			filter.Shape.Coordinates.SelectMany(c => c).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c));
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeMultiLineString_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] 
			{ 
				new[] { new[] { 102.0, 2.0 }, new[] { 103.0, 2.0 }, new[] { 103.0, 3.0 }, new[] { 102.0, 3.0 } },
				new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 } },
				new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 } } 
			};

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeMultiLineString(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeMultiLineStringFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("multilinestring");
			filter.Shape.Coordinates.SelectMany(c => c.SelectMany(cc => cc)).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c.SelectMany(cc => cc)));
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapePoint_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] { 1.0, 2.0 };

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapePoint(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapePointFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("point");
			filter.Shape.Coordinates.Should().BeEquivalentTo(coordinates);
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeMultiPoint_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } };

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeMultiPoint(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeMultiPointFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("multipoint");
			filter.Shape.Coordinates.SelectMany(c => c).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c));
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapePolygon_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] 
			{ 
				new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 }, new [] { 100.0, 0.0 } },
				new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 }, new [] { 100.2, 0.2 } }
			};

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapePolygon(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapePolygonFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("polygon");
			filter.Shape.Coordinates.SelectMany(c => c.SelectMany(cc => cc)).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c.SelectMany(cc => cc)));
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShapeMultiPolygon_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var coordinates = new[] 
			{
				new [] {
					new [] {
						new [] { 102.0, 2.0 }, new [] { 103.0, 2.0 }, new [] { 103.0, 3.0 }, new [] { 102.0, 3.0 }, new [] { 102.0, 2.0 }
					}
				},
				new [] {
					new [] {
						new [] { 100.0, 0.0 }, new [] { 101.0, 0.0 }, new [] { 101.0, 1.0 }, new [] {100.0, 1.0 }, new [] { 100.0, 0.0 }
					},
					new [] {
						new [] { 100.2, 0.2 }, new [] { 100.8, 0.2 }, new [] { 100.8, 0.8 }, new [] { 100.2, 0.8 }, new [] { 100.2, 0.2 }
					}
				}
			};

			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.GeoShape,
				f => f.GeoShapeMultiPolygon(p => p.Origin, d => d
					.Coordinates(coordinates)
				)
			);

			var filter = geoBaseShapeFilter as IGeoShapeMultiPolygonFilter;
			filter.Should().NotBeNull();
			filter.Field.Should().Be("origin");
			filter.Should().NotBeNull();
			filter.Shape.Should().NotBeNull();
			filter.Shape.Type.Should().Be("multipolygon");
		}
	}
}
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class GeoShapeQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void GeoShapeEnvelope_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeEnvelope(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
					)
				);

			var query = q as IGeoShapeEnvelopeQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Type.Should().Be("envelope");
			query.Shape.Coordinates.SelectMany(c=>c).Should()
				.BeEquivalentTo(new [] {13.0, 53.0, 14.0, 52.0 });
		}

		[Test]
		public void GeoShapeCircle_Deserializes()
		{
			var coordinates = new[] { -45.0, 45.0 };

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeCircle(gq => gq
					.OnField(p => p.MyGeoShape)
					.Radius("100m")
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapeCircleQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("circle");
			query.Shape.Radius.Should().Be("100m");
			query.Shape.Coordinates.Should().BeEquivalentTo(coordinates);
		}

		[Test]
		public void GeoShapeLineString_Deserializes()
		{
			var coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } };

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeLineString(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapeLineStringQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("linestring");
			query.Shape.Coordinates.SelectMany(c => c).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c));
		}

		[Test]
		public void GeoShapeMultiLineString_Deserializes()
		{
			var coordinates = new[] 
			{ 
				new[] { new[] { 102.0, 2.0 }, new[] { 103.0, 2.0 }, new[] { 103.0, 3.0 }, new[] { 102.0, 3.0 } },
				new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 } },
				new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 } } 
			};

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeMultiLineString(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapeMultiLineStringQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("multilinestring");
			query.Shape.Coordinates.SelectMany(c => c.SelectMany(cc => cc)).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c.SelectMany(cc => cc)));
		}

		[Test]
		public void GeoShapePoint_Deserializes()
		{
			var coordinates = new[] { 1.0, 2.0 };

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapePoint(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapePointQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("point");
			query.Shape.Coordinates.Should().BeEquivalentTo(coordinates);
		}

		[Test]
		public void GeoShapeMultiPoint_Deserializes()
		{
			var coordinates = new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } };

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeMultiPoint(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapeMultiPointQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("multipoint");
			query.Shape.Coordinates.SelectMany(c => c).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c));
		}

		[Test]
		public void GeoShapePolygon_Deserializes()
		{
			var coordinates = new[] 
			{ 
				new[] { new[] { 100.0, 0.0 }, new[] { 101.0, 0.0 }, new[] { 101.0, 1.0 }, new[] { 100.0, 1.0 }, new [] { 100.0, 0.0 } },
				new[] { new[] { 100.2, 0.2 }, new[] { 100.8, 0.2 }, new[] { 100.8, 0.8 }, new[] { 100.2, 0.8 }, new [] { 100.2, 0.2 } }
			};

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapePolygon(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapePolygonQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("polygon");
			query.Shape.Coordinates.SelectMany(c => c.SelectMany(cc => cc)).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c.SelectMany(cc => cc)));
		}

		[Test]
		public void GeoShapeMultiPolygon_Deserializes()
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

			var q = this.SerializeThenDeserialize(
				f => f.GeoShape,
				f => f.GeoShapeMultiPolygon(gq => gq
					.OnField(p => p.MyGeoShape)
					.Coordinates(coordinates)
					)
				);

			var query = q as IGeoShapeMultiPolygonQuery;
			query.Should().NotBeNull();
			query.Field.Should().Be("myGeoShape");
			query.Shape.Should().NotBeNull();
			query.Shape.Type.Should().Be("multipolygon");
			query.Shape.Coordinates.SelectMany(c => c.SelectMany(cc => cc.SelectMany(ccc => ccc))).Should()
				.BeEquivalentTo(coordinates.SelectMany(c => c.SelectMany(cc => cc.SelectMany(ccc => ccc))));
		}

	}
}
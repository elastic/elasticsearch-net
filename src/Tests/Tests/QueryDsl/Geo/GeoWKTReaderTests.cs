using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl.Geo
{
	public class GeoWKTReaderTests
	{
		[U]
		public void ParsePoint()
		{
			var wkt = "POINT (-77.03653 38.897676)";
			var shape = GeoWKTReader.Read(wkt);

			shape.Should().BeOfType<PointGeoShape>();
			var point = (PointGeoShape)shape;

			point.Coordinates.Latitude.Should().Be(38.897676);
			point.Coordinates.Longitude.Should().Be(-77.03653);
		}

		[U]
		public void ParseMultiPoint()
		{
			var wkt = "MULTIPOINT (102.0 2.0, 103.0 2.0)";
			var shape = GeoWKTReader.Read(wkt);

			var multiPoint = shape as MultiPointGeoShape;

			multiPoint.Should().NotBeNull();

			multiPoint.Coordinates.First().Latitude.Should().Be(2);
			multiPoint.Coordinates.First().Longitude.Should().Be(102);

			multiPoint.Coordinates.Last().Latitude.Should().Be(2);
			multiPoint.Coordinates.Last().Longitude.Should().Be(103);
		}

		[U]
		public void ParseLineString()
		{
			var wkt = "LINESTRING (-77.03653 38.897676, -77.009051 38.889939)";

			var shape = GeoWKTReader.Read(wkt);

			var lineString = shape as LineStringGeoShape;

			lineString.Should().NotBeNull();
			lineString.Coordinates.First().Latitude.Should().Be(38.897676);
			lineString.Coordinates.First().Longitude.Should().Be(-77.03653);

			lineString.Coordinates.Last().Latitude.Should().Be(38.889939);
			lineString.Coordinates.Last().Longitude.Should().Be(-77.009051);
		}

		[U]
		public void ParseMultiLineString()
		{
			var wkt = @"MULTILINESTRING ((102.0 2.0, 103.0 2.0, 103.0 3.0, 102.0 3.0),
										 (100.0 0.0, 101.0 0.0, 101.0 1.0, 100.0 1.0),
										 (100.2 0.2, 100.8 0.2, 100.8 0.8, 100.2 0.8))";

			var shape = GeoWKTReader.Read(wkt);

			var multiLineString = shape as MultiLineStringGeoShape;

			multiLineString.Should().NotBeNull();

			foreach (var lineString in multiLineString.Coordinates)
			foreach (var coordinate in lineString)
			{
				coordinate.Latitude.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(3);
				coordinate.Longitude.Should().BeGreaterOrEqualTo(100).And.BeLessOrEqualTo(103);
			}
		}

		[U]
		public void ParsePolygon()
		{
			var wkt = @"POLYGON ((100.0 0.0, 101.0 0.0, 101.0 1.0, 100.0 1.0, 100.0 0.0),
								 (100.2 0.2, 100.8 0.2, 100.8 0.8, 100.2 0.8, 100.2 0.2))";

			var shape = GeoWKTReader.Read(wkt);

			var polygon = shape as PolygonGeoShape;

			polygon.Should().NotBeNull();

			foreach (var ring in polygon.Coordinates)
			foreach (var coordinate in ring)
			{
				coordinate.Latitude.Should().BeLessOrEqualTo(1.0);
				coordinate.Longitude.Should().BeGreaterOrEqualTo(100.0);
			}
		}

		[U]
		public void ParseMultiPolygon()
		{
			var wkt = @"MULTIPOLYGON (
							((102.0 2.0, 103.0 2.0, 103.0 3.0, 102.0 3.0, 102.0 2.0)),
							((100.0 0.0, 101.0 0.0, 101.0 1.0, 100.0 1.0, 100.0 0.0),
							 (100.2 0.2, 100.8 0.2, 100.8 0.8, 100.2 0.8, 100.2 0.2)))";

			var shape = GeoWKTReader.Read(wkt);

			var multiPolygon = shape as MultiPolygonGeoShape;

			multiPolygon.Should().NotBeNull();
			multiPolygon.Coordinates.Should().HaveCount(2);

			foreach (var polygon in multiPolygon.Coordinates)
			foreach (var ring in polygon)
			{
				ring.Should().HaveCount(5);
				foreach (var coordinate in ring)
				{
					coordinate.Latitude.Should().BeLessOrEqualTo(3.0).And.BeGreaterOrEqualTo(0);
					coordinate.Longitude.Should().BeGreaterOrEqualTo(100.0).And.BeLessOrEqualTo(103.0);
				}
			}
		}

		[U]
		public void ParseGeometryCollection()
		{
			var wkt = "GEOMETRYCOLLECTION (POINT (100.0 0.0), LINESTRING (101.0 0.0, 102.0 1.0))";

			var shape = GeoWKTReader.Read(wkt);

			var geometryCollection = shape as GeometryCollection;

			geometryCollection.Should().NotBeNull();
			geometryCollection.Geometries.Should().HaveCount(2);

			geometryCollection.Geometries.First().Should().BeOfType<PointGeoShape>();
			geometryCollection.Geometries.Last().Should().BeOfType<LineStringGeoShape>();
		}

	}
}

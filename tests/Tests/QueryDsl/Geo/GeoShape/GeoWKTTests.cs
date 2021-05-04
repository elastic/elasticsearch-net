// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl.Geo.GeoShape
{
	public class GeoWKTTests
	{
		[U]
		public void ReadAndWritePoint()
		{
			var wkt = "POINT (-77.03653 38.897676)";
			var shape = GeoWKTReader.Read(wkt);

			shape.Should().BeOfType<PointGeoShape>();
			var point = (PointGeoShape)shape;

			point.Coordinates.Latitude.Should().Be(38.897676);
			point.Coordinates.Longitude.Should().Be(-77.03653);

			GeoWKTWriter.Write(point).Should().Be(wkt);
		}

		[U]
		public void ReadAndWritePointWithExponent()
		{
			var wkt = "POINT (1.2E2 -2.5E-05)";
			var shape = GeoWKTReader.Read(wkt);

			shape.Should().BeOfType<PointGeoShape>();
			var point = (PointGeoShape)shape;

			point.Coordinates.Latitude.Should().Be(-0.000025);
			point.Coordinates.Longitude.Should().Be(120);

			// 1.2E2 will be expanded
			GeoWKTWriter.Write(point).Should().Be("POINT (120 -2.5E-05)");
		}

		[U]
		public void ReadAndWriteMultiPoint()
		{
			var wkt = "MULTIPOINT (102.0 2.0, 103.0 2.0)";
			var shape = GeoWKTReader.Read(wkt);

			var multiPoint = shape as MultiPointGeoShape;

			multiPoint.Should().NotBeNull();

			var firstPoint = multiPoint.Coordinates.First();
			firstPoint.Latitude.Should().Be(2);
			firstPoint.Longitude.Should().Be(102);

			var lastPoint = multiPoint.Coordinates.Last();
			lastPoint.Latitude.Should().Be(2);
			lastPoint.Longitude.Should().Be(103);

			GeoWKTWriter.Write(multiPoint).Should().Be("MULTIPOINT (102 2, 103 2)");
		}

		[U]
		public void ReadAndWriteLineString()
		{
			var wkt = "LINESTRING (-77.03653 38.897676, -77.009051 38.889939)";

			var shape = GeoWKTReader.Read(wkt);

			var lineString = shape as LineStringGeoShape;

			lineString.Should().NotBeNull();
			lineString.Coordinates.First().Latitude.Should().Be(38.897676);
			lineString.Coordinates.First().Longitude.Should().Be(-77.03653);

			lineString.Coordinates.Last().Latitude.Should().Be(38.889939);
			lineString.Coordinates.Last().Longitude.Should().Be(-77.009051);

			GeoWKTWriter.Write(lineString).Should().Be(wkt);
		}

		[U]
		public void ReadMultiLineString()
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
		public void WriteMultiLineString()
		{
			var multLineString = new MultiLineStringGeoShape(new[]
			{
				new[]
				{
					new GeoCoordinate(2, 102),
					new GeoCoordinate(2, 103),
					new GeoCoordinate(3, 103),
					new GeoCoordinate(3, 102),
				},
				new[]
				{
					new GeoCoordinate(0, 100),
					new GeoCoordinate(0, 101),
					new GeoCoordinate(1, 101),
					new GeoCoordinate(1, 100),
				}
			});

			var wkt = GeoWKTWriter.Write(multLineString);
			wkt.Should().Be("MULTILINESTRING ((102 2, 103 2, 103 3, 102 3), (100 0, 101 0, 101 1, 100 1))");
		}

		[U]
		public void ReadPolygon()
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
		public void WritePolygon()
		{
			var polygon = new PolygonGeoShape(new[]
			{
				new[]
				{
					new GeoCoordinate(2, 102),
					new GeoCoordinate(2, 103),
					new GeoCoordinate(3, 103),
					new GeoCoordinate(3, 102),
				},
				new[]
				{
					new GeoCoordinate(0, 100),
					new GeoCoordinate(0, 101),
					new GeoCoordinate(1, 101),
					new GeoCoordinate(1, 100),
				}
			});

			var wkt = GeoWKTWriter.Write(polygon);
			wkt.Should().Be("POLYGON ((102 2, 103 2, 103 3, 102 3), (100 0, 101 0, 101 1, 100 1))");
		}

		[U]
		public void ReadMultiPolygon()
		{
			var wkt = @"MULTIPOLYGON (
							((102.0 2.0, 103.0 2.0, 103.0 3.0, 102.0 3.0, 102.0 2.0)),
							((100.0 0.0, 101.0 0.0, 101.0 1.0, 100.0 1.0, 100.0 0.0),
							 (100.2 0.2, 100.8 0.2, 100.8 0.8, 100.2 0.8, 100.2 0.2)))";

			var shape = GeoWKTReader.Read(wkt);

			var multiPolygon = shape as MultiPolygonGeoShape;

			multiPolygon!.Should().NotBeNull();
			multiPolygon.Coordinates.Should().HaveCount(2);

			foreach (var polygon in multiPolygon.Coordinates)
			foreach (var ring in polygon)
			{
				// ReSharper disable PossibleMultipleEnumeration
				ring.Should().HaveCount(5);
				foreach (var coordinate in ring)
				{
					coordinate.Latitude.Should().BeLessOrEqualTo(3.0).And.BeGreaterOrEqualTo(0);
					coordinate.Longitude.Should().BeGreaterOrEqualTo(100.0).And.BeLessOrEqualTo(103.0);
				}
				// ReSharper restore PossibleMultipleEnumeration
			}
		}

		[U]
		public void WriteMultiPolygon()
		{
			var multiPolygon = new MultiPolygonGeoShape(new[]
			{
				new[]
				{
					new[]
					{
						new GeoCoordinate(2, 102),
						new GeoCoordinate(2, 103),
						new GeoCoordinate(3, 103),
						new GeoCoordinate(3, 102),
						new GeoCoordinate(2, 102),
					}
				},
				new[]
				{
					new[]
					{
						new GeoCoordinate(0, 100),
						new GeoCoordinate(0, 101),
						new GeoCoordinate(1, 101),
						new GeoCoordinate(1, 100),
						new GeoCoordinate(0, 100),
					},
					new[]
					{
						new GeoCoordinate(0.2, 100.2),
						new GeoCoordinate(0.2, 100.8),
						new GeoCoordinate(0.8, 100.8),
						new GeoCoordinate(0.8, 100.2),
						new GeoCoordinate(0.2, 100.2),
					}
				}
			});

			GeoWKTWriter.Write(multiPolygon)
				.Should()
				.Be(
					"MULTIPOLYGON (((102 2, 103 2, 103 3, 102 3, 102 2)), ((100 0, 101 0, 101 1, 100 1, 100 0), (100.2 0.2, 100.8 0.2, 100.8 0.8, 100.2 0.8, 100.2 0.2)))");
		}

		[U]
		public void ReadAndWriteEnvelope()
		{
			var wkt = "BBOX (-74.1, -71.12, 40.73, 40.01)";
			var shape = GeoWKTReader.Read(wkt);
			var envelope = shape as EnvelopeGeoShape;

			envelope.Should().NotBeNull();
			envelope.Coordinates.First().Latitude.Should().Be(40.73);
			envelope.Coordinates.First().Longitude.Should().Be(-74.1);
			envelope.Coordinates.Last().Latitude.Should().Be(40.01);
			envelope.Coordinates.Last().Longitude.Should().Be(-71.12);

			GeoWKTWriter.Write(shape).Should().Be(wkt);
		}

		[U]
		public void WriteEnvelopeIgnoresZValues()
		{
			var envelope = new EnvelopeGeoShape(new[]
			{
				new GeoCoordinate(40.73, -74.1, 3),
				new GeoCoordinate(40.01, -71.12, 2)
			});

			GeoWKTWriter.Write(envelope).Should().Be("BBOX (-74.1, -71.12, 40.73, 40.01)");
		}

		[U]
		public void ReadAndWriteGeometryCollection()
		{
			var wkt = "GEOMETRYCOLLECTION (POINT (100 0), LINESTRING (101 0, 102 1))";
			var shape = GeoWKTReader.Read(wkt);
			var geometryCollection = shape as IGeometryCollection;

			geometryCollection.Should().NotBeNull();
			geometryCollection.Geometries.Should().HaveCount(2);
			geometryCollection.Geometries.First().Should().BeOfType<PointGeoShape>();
			geometryCollection.Geometries.Last().Should().BeOfType<LineStringGeoShape>();

			GeoWKTWriter.Write(geometryCollection).Should().Be(wkt);
		}

		[U]
		public void UnknownGeometryThrowsGeoWKTException()
		{
			var wkt = "UNKNOWN (100 0)";
			Action action = () => GeoWKTReader.Read(wkt);
			action.Should().Throw<GeoWKTException>().Which.Message.Should().Be("Unknown geometry type: UNKNOWN");
		}

		[U]
		public void MalformedPolygonThrowsGeoWKTException()
		{
			var wkt = "POLYGON ((100, 5) (100, 10) (90, 10), (90, 5), (100, 5)";
			Action action = () => GeoWKTReader.Read(wkt);
			action.Should().Throw<GeoWKTException>().Which.Message.Should().Be("Expected number but found: , at line 1, position 14");
		}

		[U]
		public void GeoWKTExceptionReturnsCorrectLineNumberAndPosition()
		{
			var wkt = "POLYGON (\n(100, 5) (100, 10) (90, 10), (90, 5), (100, 5)";
			Action action = () => GeoWKTReader.Read(wkt);
			action.Should().Throw<GeoWKTException>().Which.Message.Should().Be("Expected number but found: , at line 2, position 5");
		}
	}
}

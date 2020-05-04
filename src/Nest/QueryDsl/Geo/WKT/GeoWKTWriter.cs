// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Writes <see cref="IGeoShape" /> types to Well-Known Text (WKT)
	/// </summary>
	public class GeoWKTWriter
	{
		/// <summary>
		/// Writes a <see cref="IGeoShape" /> to Well-Known Text (WKT)
		/// </summary>
		public static string Write(IGeoShape shape) =>
			shape == null ? null : Write(shape, new StringBuilder());

		private static string Write(IGeoShape shape, StringBuilder builder)
		{
			switch (shape)
			{
				case IPointGeoShape point:
					WritePoint(point, builder);
					break;
				case IMultiPointGeoShape multiPoint:
					WriteMultiPoint(multiPoint, builder);
					break;
				case ILineStringGeoShape lineString:
					WriteLineString(lineString, builder);
					break;
				case IMultiLineStringGeoShape multiLineString:
					WriteMultiLineString(multiLineString, builder);
					break;
				case IPolygonGeoShape polygon:
					WritePolygon(polygon, builder);
					break;
				case IMultiPolygonGeoShape multiPolygon:
					WriteMultiPolygon(multiPolygon, builder);
					break;
				case IGeometryCollection geometryCollection:
					WriteGeometryCollection(geometryCollection, builder);
					break;
				case IEnvelopeGeoShape envelope:
					WriteEnvelope(envelope, builder);
					break;
				default:
					throw new GeoWKTException($"Unknown geometry type: {shape.GetType().Name}");
			}

			return builder.ToString();
		}

		private static void WritePoint(IPointGeoShape point, StringBuilder builder)
		{
			builder.Append(GeoShapeType.Point).Append(" (");
			WriteCoordinate(point.Coordinates, builder);
			builder.Append(")");
		}

		private static void WriteMultiPoint(IMultiPointGeoShape multiPoint, StringBuilder builder)
		{
			builder.Append(GeoShapeType.MultiPoint).Append(" (");
			WriteCoordinates(multiPoint.Coordinates, builder);
			builder.Append(")");
		}

		private static void WriteLineString(ILineStringGeoShape lineString, StringBuilder builder)
		{
			builder.Append(GeoShapeType.LineString).Append(" (");
			WriteCoordinates(lineString.Coordinates, builder);
			builder.Append(")");
		}

		private static void WriteMultiLineString(IMultiLineStringGeoShape multiLineString, StringBuilder builder)
		{
			builder.Append(GeoShapeType.MultiLineString).Append(" ");
			WriteCoordinatesList(multiLineString.Coordinates, builder);
		}

		private static void WritePolygon(IPolygonGeoShape polygon, StringBuilder builder)
		{
			builder.Append(GeoShapeType.Polygon).Append(" ");
			WriteCoordinatesList(polygon.Coordinates, builder);
		}

		private static void WriteMultiPolygon(IMultiPolygonGeoShape multiPolygon, StringBuilder builder)
		{
			builder.Append(GeoShapeType.MultiPolygon).Append(" (");
			var i = 0;
			foreach (var polygon in multiPolygon.Coordinates)
			{
				if (i > 0)
					builder.Append(", ");

				WriteCoordinatesList(polygon, builder);
				i++;
			}
			builder.Append(")");
		}

		private static void WriteGeometryCollection(IGeometryCollection geometryCollection, StringBuilder builder)
		{
			builder.Append(GeoShapeType.GeometryCollection).Append(" (");
			var i = 0;
			foreach (var shape in geometryCollection.Geometries)
			{
				if (i > 0)
					builder.Append(", ");

				Write(shape, builder);
				i++;
			}
			builder.Append(")");
		}

		private static void WriteEnvelope(IEnvelopeGeoShape envelope, StringBuilder builder)
		{
			builder.Append(GeoShapeType.BoundingBox).Append(" (");
			var topLeft = envelope.Coordinates.ElementAt(0);
			var bottomRight = envelope.Coordinates.ElementAt(1);

			// WKT specification expects the following order: minLon, maxLon, maxLat, minLat.
			// envelope is top_left (minLon, maxLat), bottom_right (maxLon, minLat)
			builder.Append(topLeft.Longitude.ToString(CultureInfo.InvariantCulture))
				.Append(", ")
				.Append(bottomRight.Longitude.ToString(CultureInfo.InvariantCulture))
				.Append(", ")
				.Append(topLeft.Latitude.ToString(CultureInfo.InvariantCulture))
				.Append(", ")
				.Append(bottomRight.Latitude.ToString(CultureInfo.InvariantCulture))
				.Append(")");
		}

		private static void WriteCoordinatesList(IEnumerable<IEnumerable<GeoCoordinate>> coordinates, StringBuilder builder)
		{
			builder.Append("(");
			var i = 0;
			foreach (var coordinateGroup in coordinates)
			{
				if (i > 0)
					builder.Append(", ");

				builder.Append("(");
				WriteCoordinates(coordinateGroup, builder);
				builder.Append(")");
				i++;
			}
			builder.Append(")");
		}

		private static void WriteCoordinates(IEnumerable<GeoCoordinate> coordinates, StringBuilder builder)
		{
			var i = 0;
			foreach (var coordinate in coordinates)
			{
				if (i > 0)
					builder.Append(", ");
				WriteCoordinate(coordinate, builder);
				i++;
			}
		}

		private static void WriteCoordinate(GeoCoordinate coordinate, StringBuilder builder)
		{
			builder.Append(coordinate.Longitude.ToString(CultureInfo.InvariantCulture))
				.Append(" ")
				.Append(coordinate.Latitude.ToString(CultureInfo.InvariantCulture));

			if (coordinate.Z.HasValue)
				builder.Append(" ").Append(coordinate.Z.Value.ToString(CultureInfo.InvariantCulture));
		}
	}
}

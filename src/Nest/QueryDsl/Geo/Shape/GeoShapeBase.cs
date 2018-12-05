using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeConverter))]
	public interface IGeoShape
	{
		/// <summary>
		/// The type of geo shape
		/// </summary>
		[DataMember(Name = "type")]
		string Type { get; }
	}

	internal enum GeoShapeFormat
	{
		GeoJson,
		WellKnownText
	}

	internal static class GeoShapeType
	{
		// WKT uses BBOX for envelope geo shape
		public const string BoundingBox = "BBOX";
		public const string Circle = "CIRCLE";
		public const string Envelope = "ENVELOPE";
		public const string GeometryCollection = "GEOMETRYCOLLECTION";
		public const string LineString = "LINESTRING";
		public const string MultiLineString = "MULTILINESTRING";
		public const string MultiPoint = "MULTIPOINT";
		public const string MultiPolygon = "MULTIPOLYGON";
		public const string Point = "POINT";
		public const string Polygon = "POLYGON";
	}

	/// <summary>
	/// Base type for geo shapes
	/// </summary>
	public abstract class GeoShapeBase : IGeoShape
	{
		protected GeoShapeBase(string type) => Type = type;

		/// <inheritdoc />
		public string Type { get; protected set; }

		internal GeoShapeFormat Format { get; set; }
	}

	internal class GeoShapeConverter : IJsonFormatter<IGeoShape>
	{
		public IGeoShape Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					return null;
				case JsonToken.String:
					return GeoWKTReader.Read(reader.ReadString());
				default:
					return ReadShape(ref reader, formatterResolver);
			}
		}

		public void Serialize(ref JsonWriter writer, IGeoShape value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value is GeoShapeBase shapeBase && shapeBase.Format == GeoShapeFormat.WellKnownText)
			{
				writer.WriteString(GeoWKTWriter.Write(shapeBase));
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName("type");
			writer.WriteString(value.Type);
			writer.WriteValueSeparator();

			switch (value)
			{
				case IPointGeoShape point:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<GeoCoordinate>();
					formatter.Serialize(ref writer, point.Coordinates, formatterResolver);
					break;
				}
				case IMultiPointGeoShape multiPoint:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<GeoCoordinate>>();
					formatter.Serialize(ref writer, multiPoint.Coordinates, formatterResolver);
					break;
				}
				case ILineStringGeoShape lineString:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<GeoCoordinate>>();
					formatter.Serialize(ref writer, lineString.Coordinates, formatterResolver);
					break;
				}
				case IMultiLineStringGeoShape multiLineString:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<IEnumerable<GeoCoordinate>>>();
					formatter.Serialize(ref writer, multiLineString.Coordinates, formatterResolver);
					break;
				}
				case IPolygonGeoShape polygon:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<IEnumerable<GeoCoordinate>>>();
					formatter.Serialize(ref writer, polygon.Coordinates, formatterResolver);
					break;
				}
				case IMultiPolygonGeoShape multiPolygon:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>();
					formatter.Serialize(ref writer, multiPolygon.Coordinates, formatterResolver);
					break;
				}
				case IEnvelopeGeoShape envelope:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<IEnumerable<GeoCoordinate>>();
					formatter.Serialize(ref writer, envelope.Coordinates, formatterResolver);
					break;
				}
				case ICircleGeoShape circle:
				{
					writer.WritePropertyName("coordinates");
					var formatter = formatterResolver.GetFormatter<GeoCoordinate>();
					formatter.Serialize(ref writer, circle.Coordinates, formatterResolver);
					writer.WriteValueSeparator();
					writer.WritePropertyName("radius");
					writer.WriteString(circle.Radius);
					break;
				}
				case IGeometryCollection collection:
				{
					writer.WritePropertyName("geometries");
					var formatter = formatterResolver.GetFormatter<IEnumerable<IGeoShape>>();
					formatter.Serialize(ref writer, collection.Geometries, formatterResolver);
					break;
				}
			}

			writer.WriteEndObject();
		}

		internal static IGeoShape ReadShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var segment = reader.ReadNextBlockSegment();
			var count = 0;
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			string typeName = null;

			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyName();
				if (propertyName == "type")
				{
					typeName = segmentReader.ReadString().ToUpperInvariant();
					break;
				}
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			switch (typeName)
			{
				case GeoShapeType.Circle:
					return ParseCircleGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.Envelope:
					return ParseEnvelopeGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.LineString:
					return ParseLineStringGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.MultiLineString:
					return ParseMultiLineStringGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.Point:
					return ParsePointGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.MultiPoint:
					return ParseMultiPointGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.Polygon:
					return ParsePolygonGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.MultiPolygon:
					return ParseMultiPolygonGeoShape(ref segmentReader, formatterResolver);
				case GeoShapeType.GeometryCollection:
					return ParseGeometryCollection(ref segmentReader, formatterResolver);
				default:
					return null;
			}
		}

		private static GeometryCollection ParseGeometryCollection(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var count = 0;
			var geoShapes = Enumerable.Empty<IGeoShape>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "geometries")
					geoShapes = formatterResolver.GetFormatter<IEnumerable<IGeoShape>>()
						.Deserialize(ref reader, formatterResolver);
			}

			return new GeometryCollection { Geometries = geoShapes };
		}

		private static MultiPolygonGeoShape ParseMultiPolygonGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new MultiPolygonGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>(ref reader, formatterResolver)
			};

		private static PolygonGeoShape ParsePolygonGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new PolygonGeoShape { Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(ref reader, formatterResolver) };

		private static MultiPointGeoShape ParseMultiPointGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new MultiPointGeoShape { Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(ref reader, formatterResolver) };

		private static PointGeoShape ParsePointGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new PointGeoShape { Coordinates = GetCoordinates<GeoCoordinate>(ref reader, formatterResolver) };

		private static MultiLineStringGeoShape ParseMultiLineStringGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new MultiLineStringGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(ref reader, formatterResolver)
			};

		private static LineStringGeoShape ParseLineStringGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new LineStringGeoShape { Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(ref reader, formatterResolver) };

		private static EnvelopeGeoShape ParseEnvelopeGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			new EnvelopeGeoShape { Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(ref reader, formatterResolver) };

		private static CircleGeoShape ParseCircleGeoShape(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var count = 0;
			string radius = null;
			GeoCoordinate coordinate = null;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case "coordinates":
						coordinate = formatterResolver.GetFormatter<GeoCoordinate>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case "radius":
						radius = reader.ReadString();
						break;
				}
			}

			return new CircleGeoShape
			{
				Coordinates = coordinate,
				Radius = radius
			};
		}

		private static T GetCoordinates<T>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "coordinates")
				{
					var formatter = formatterResolver.GetFormatter<T>();
					return formatter.Deserialize(ref reader, formatterResolver);
				}
			}

			return default(T);
		}
	}
}

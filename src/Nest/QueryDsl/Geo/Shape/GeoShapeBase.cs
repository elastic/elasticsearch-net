using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[ContractJsonConverterAttribute(typeof(GeoShapeConverter))]
	public interface IGeoShape
	{
		/// <summary>
		/// The type of geo shape
		/// </summary>
		[JsonProperty("type")]
		string Type { get; }
	}

	internal enum GeoShapeFormat
	{
		GeoJson,
		WellKnownText
	}

	internal static class GeoShapeType
	{
		public const string Point = "POINT";
		public const string MultiPoint = "MULTIPOINT";
		public const string LineString = "LINESTRING";
		public const string MultiLineString = "MULTILINESTRING";
		public const string Polygon = "POLYGON";
		public const string MultiPolygon = "MULTIPOLYGON";
		public const string Circle = "CIRCLE";
		public const string Envelope = "ENVELOPE";
		public const string GeometryCollection = "GEOMETRYCOLLECTION";

		// WKT uses BBOX for envelope geo shape
		public const string BoundingBox = "BBOX";
	}

	/// <summary>
	/// Base type for geo shapes
	/// </summary>
	public abstract class GeoShapeBase : IGeoShape
	{
		internal GeoShapeFormat Format { get; set; }
	    protected GeoShapeBase(string type) => this.Type = type;

		/// <inheritdoc />
		public string Type { get; protected set; }
	}

	internal class GeoShapeConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value is IGeoShape shape)
			{
				if (value is GeoShapeBase shapeBase && shapeBase.Format == GeoShapeFormat.WellKnownText)
				{
					writer.WriteValue(GeoWKTWriter.Write(shapeBase));
					return;
				}

				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue(shape.Type);

				switch (shape)
				{
					case IPointGeoShape point:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, point.Coordinates);
						break;
					case IMultiPointGeoShape multiPoint:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, multiPoint.Coordinates);
						break;
					case ILineStringGeoShape lineString:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, lineString.Coordinates);
						break;
					case IMultiLineStringGeoShape multiLineString:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, multiLineString.Coordinates);
						break;
					case IPolygonGeoShape polygon:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, polygon.Coordinates);
						break;
					case IMultiPolygonGeoShape multiPolygon:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, multiPolygon.Coordinates);
						break;
					case IEnvelopeGeoShape envelope:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, envelope.Coordinates);
						break;
					case ICircleGeoShape circle:
						writer.WritePropertyName("coordinates");
						serializer.Serialize(writer, circle.Coordinates);
						writer.WritePropertyName("radius");
						writer.WriteValue(circle.Radius);
						break;
					case IGeometryCollection collection:
						writer.WritePropertyName("geometries");
						serializer.Serialize(writer, collection.Geometries);
						break;
				}

				writer.WriteEndObject();
			}
			else
			{
				throw new NotSupportedException($"{value.GetType()} is not a supported {nameof(IGeoShape)}");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return null;
				case JsonToken.String:
					return GeoWKTReader.Read((string)reader.Value);
				default:
					var shape = JObject.Load(reader);
					return ReadJToken(shape, serializer);
			}
		}

		internal static IGeoShape ReadJToken(JToken shape, JsonSerializer serializer)
		{
			var typeName = shape["type"]?.Value<string>().ToUpperInvariant();
			switch (typeName)
			{
				case GeoShapeType.Circle:
					return ParseCircleGeoShape(shape, serializer);
				case GeoShapeType.Envelope:
					return ParseEnvelopeGeoShape(shape, serializer);
				case GeoShapeType.LineString:
					return ParseLineStringGeoShape(shape, serializer);
				case GeoShapeType.MultiLineString:
					return ParseMultiLineStringGeoShape(shape, serializer);
				case GeoShapeType.Point:
					return ParsePointGeoShape(shape, serializer);
				case GeoShapeType.MultiPoint:
					return ParseMultiPointGeoShape(shape, serializer);
				case GeoShapeType.Polygon:
					return ParsePolygonGeoShape(shape, serializer);
				case GeoShapeType.MultiPolygon:
					return ParseMultiPolygonGeoShape(shape, serializer);
				case GeoShapeType.GeometryCollection:
					return ParseGeometryCollection(shape, serializer);
				default:
					return null;
			}
		}

		public override bool CanConvert(Type objectType) => typeof(IGeoShape).IsAssignableFrom(objectType);

		private static GeometryCollection ParseGeometryCollection(JToken shape, JsonSerializer serializer)
		{
			if (!(shape["geometries"] is JArray geometries))
				return new GeometryCollection { Geometries = Enumerable.Empty<IGeoShape>() };

			var geoShapes = new List<IGeoShape>(geometries.Count);
			for (var index = 0; index < geometries.Count; index++)
			{
				var geometry = geometries[index];
				if (ReadJToken(geometry, serializer) is IGeoShape innerShape)
					geoShapes.Add(innerShape);
			}

			return new GeometryCollection { Geometries = geoShapes };
		}

		private static MultiPolygonGeoShape ParseMultiPolygonGeoShape(JToken shape, JsonSerializer serializer) =>
			new MultiPolygonGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>(shape, serializer)
			};

		private static PolygonGeoShape ParsePolygonGeoShape(JToken shape, JsonSerializer serializer) =>
			new PolygonGeoShape {Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)};

		private static MultiPointGeoShape ParseMultiPointGeoShape(JToken shape, JsonSerializer serializer) =>
			new MultiPointGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private static PointGeoShape ParsePointGeoShape(JToken shape, JsonSerializer serializer) =>
			new PointGeoShape {Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer)};

		private static MultiLineStringGeoShape ParseMultiLineStringGeoShape(JToken shape, JsonSerializer serializer) =>
			new MultiLineStringGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)
			};

		private static LineStringGeoShape ParseLineStringGeoShape(JToken shape, JsonSerializer serializer) =>
			new LineStringGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private static EnvelopeGeoShape ParseEnvelopeGeoShape(JToken shape, JsonSerializer serializer) =>
			new EnvelopeGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private static CircleGeoShape ParseCircleGeoShape(JToken shape, JsonSerializer serializer) =>
			new CircleGeoShape
			{
				Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer),
				Radius = shape["radius"]?.Value<string>()
			};

		private static T GetCoordinates<T>(JToken shape, JsonSerializer serializer)
		{
			var coordinates = shape["coordinates"];
			return coordinates != null
				? coordinates.ToObject<T>(serializer)
				: default(T);
		}
	}
}

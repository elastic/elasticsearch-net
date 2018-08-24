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

		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		[Obsolete("Removed in NEST 7.x. Use IgnoreUnmapped on IGeoShapeQuery")]
		bool? IgnoreUnmapped { get; set; }
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
		internal const string BoundingBox = "BBOX";
	}

	public abstract class GeoShapeBase : IGeoShape
	{
		internal GeoShapeFormat Format { get; set; }

	    protected GeoShapeBase(string type) => this.Type = type;

		/// <inheritdoc />
		public string Type { get; protected set; }

		/// <inheritdoc />
		[Obsolete("Removed in NEST 7.x. Use IgnoreUnmapped on IGeoShapeQuery")]
		public bool? IgnoreUnmapped { get; set; }
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

			// IGeometryCollection needs to be handled separately because it does not
			// implement IGeoShape, and can't because it would be a binary breaking change.
			// This is fixed in next major release.
			if (value is IGeometryCollection collection)
			{
				if (collection is GeometryCollection geometryCollection && geometryCollection.Format == GeoShapeFormat.WellKnownText)
				{
					writer.WriteValue(GeoWKTWriter.Write(collection));
					return;
				}

				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue(collection.Type);
				writer.WritePropertyName("geometries");
				serializer.Serialize(writer, collection.Geometries);
				writer.WriteEndObject();
			}
			else if (value is IGeoShape shape)
			{
				if (value is GeoShapeBase shapeBase && shapeBase.Format == GeoShapeFormat.WellKnownText)
				{
					writer.WriteValue(GeoWKTWriter.Write(shapeBase));
					return;
				}

				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue(shape.Type);
				writer.WritePropertyName("coordinates");
				switch (shape)
				{
					case IPointGeoShape point:
						serializer.Serialize(writer, point.Coordinates);
						break;
					case IMultiPointGeoShape multiPoint:
						serializer.Serialize(writer, multiPoint.Coordinates);
						break;
					case ILineStringGeoShape lineString:
						serializer.Serialize(writer, lineString.Coordinates);
						break;
					case IMultiLineStringGeoShape multiLineString:
						serializer.Serialize(writer, multiLineString.Coordinates);
						break;
					case IPolygonGeoShape polygon:
						serializer.Serialize(writer, polygon.Coordinates);
						break;
					case IMultiPolygonGeoShape multiPolyon:
						serializer.Serialize(writer, multiPolyon.Coordinates);
						break;
					case IEnvelopeGeoShape envelope:
						serializer.Serialize(writer, envelope.Coordinates);
						break;
					case ICircleGeoShape circle:
						serializer.Serialize(writer, circle.Coordinates);
						writer.WritePropertyName("radius");
						writer.WriteValue(circle.Radius);
						break;
				}
				writer.WriteEndObject();
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

		internal static object ReadJToken(JToken shape, JsonSerializer serializer)
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

		public override bool CanConvert(Type objectType) =>
			typeof(IGeoShape).IsAssignableFrom(objectType) || typeof(IGeometryCollection).IsAssignableFrom(objectType);

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

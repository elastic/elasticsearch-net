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

	/// <summary>
	/// Base type for geo shapes
	/// </summary>
	public abstract class GeoShapeBase : IGeoShape
	{
	    protected GeoShapeBase(string type) => this.Type = type;

		/// <inheritdoc />
		public string Type { get; protected set; }
	}

	internal class GeoShapeConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var shape = JObject.Load(reader);
			return ReadJToken(shape, serializer);
		}

		internal static IGeoShape ReadJToken(JToken shape, JsonSerializer serializer)
		{
			var typeName = shape["type"]?.Value<string>();
			switch (typeName)
			{
				case "circle":
					return ParseCircleGeoShape(shape, serializer);
				case "envelope":
					return ParseEnvelopeGeoShape(shape, serializer);
				case "linestring":
					return ParseLineStringGeoShape(shape, serializer);
				case "multilinestring":
					return ParseMultiLineStringGeoShape(shape, serializer);
				case "point":
					return ParsePointGeoShape(shape, serializer);
				case "multipoint":
					return ParseMultiPointGeoShape(shape, serializer);
				case "polygon":
					return ParsePolygonGeoShape(shape, serializer);
				case "multipolygon":
					return ParseMultiPolygonGeoShape(shape, serializer);
				case "geometrycollection":
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

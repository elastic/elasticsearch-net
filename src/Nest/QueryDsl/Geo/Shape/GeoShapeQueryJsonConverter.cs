using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class GeoShapeQueryJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public virtual T GetCoordinates<T>(JToken shape, JsonSerializer serializer)
		{
			var coordinates = shape["coordinates"];
			if (coordinates != null)
				return coordinates.ToObject<T>(serializer);
			return default(T);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;


			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			JToken shape;
			JToken indexedShape;
			IGeoShapeQuery query = null;
			if (jo.TryGetValue("shape", out shape))
				query = ParseShape(shape, serializer);
			else if (jo.TryGetValue("indexed_shape", out indexedShape))
				query = ParseIndexedShape(indexedShape);

			if (query == null) return null;
			var boost = jo["boost"]?.Value<double>();
			var name = jo["_name"]?.Value<string>();
			query.Boost = boost;
			query.Name = name;
			query.Field = field;
			return query;
		}

		private IGeoShapeQuery ParseIndexedShape(JToken indexedShape) =>
			new GeoIndexedShapeQuery {IndexedShape = (indexedShape as JObject)?.ToObject<FieldLookup>()};

		private IGeoShapeQuery ParseShape(JToken shape, JsonSerializer serializer)
		{
			var type = shape["type"];
			var typeName = type?.Value<string>();
			switch (typeName)
			{
				case "circle":
					var radius = shape["radius"];
					return new GeoShapeCircleQuery
					{
						Shape = new CircleGeoShape
						{
							Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer),
							Radius = radius?.Value<string>()
						}
					};
				case "envelope":
					return new GeoShapeEnvelopeQuery
					{
						Shape = new EnvelopeGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)}
					};
				case "linestring":
					return new GeoShapeLineStringQuery
					{
						Shape = new LineStringGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)}
					};
				case "multilinestring":
					return new GeoShapeMultiLineStringQuery
					{
						Shape = new MultiLineStringGeoShape
						{
							Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)
						}
					};
				case "point":
					return new GeoShapePointQuery
					{
						Shape = new PointGeoShape {Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer)}
					};
				case "multipoint":
					return new GeoShapeMultiPointQuery
					{
						Shape = new MultiPointGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)}
					};
				case "polygon":
					return new GeoShapePolygonQuery
					{
						Shape = new PolygonGeoShape {Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)}
					};
				case "multipolygon":
					return new GeoShapeMultiPolygonQuery
					{
						Shape = new MultiPolygonGeoShape
						{
							Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>(shape, serializer)
						}
					};
				default:
					return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}

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
				query = ParseShapeQuery(shape, serializer);
			else if (jo.TryGetValue("indexed_shape", out indexedShape))
				query = ParseIndexedShapeQuery(indexedShape);

			if (query == null) return null;
			var boost = jo["boost"]?.Value<double>();
			var name = jo["_name"]?.Value<string>();
			var relation = jo["relation"]?.Value<string>().ToEnum<GeoShapeRelation>();
			var ignoreUnmapped = jo["ignore_unmapped"]?.Value<bool>();
			query.Boost = boost;
			query.Name = name;
			query.Field = field;
			query.Relation = relation;
			query.IgnoreUnmapped = ignoreUnmapped;
			return query;
		}

		private IGeoShapeQuery ParseIndexedShapeQuery(JToken indexedShape) =>
			new GeoIndexedShapeQuery {IndexedShape = (indexedShape as JObject)?.ToObject<FieldLookup>()};

		private IGeoShapeQuery ParseShapeQuery(JToken shape, JsonSerializer serializer)
		{
			var type = shape["type"];
			var typeName = type?.Value<string>();
			switch (typeName)
			{
				case "circle":
					var radius = shape["radius"];
					return new GeoShapeCircleQuery
					{
						Shape = ParseCircleGeoShape(shape, serializer, radius)
					};
				case "envelope":
					return new GeoShapeEnvelopeQuery
					{
						Shape = ParseEnvelopeGeoShape(shape, serializer)
					};
				case "linestring":
					return new GeoShapeLineStringQuery
					{
						Shape = ParseLineStringGeoShape(shape, serializer)
					};
				case "multilinestring":
					return new GeoShapeMultiLineStringQuery
					{
						Shape = ParseMultiLineStringGeoShape(shape, serializer)
					};
				case "point":
					return new GeoShapePointQuery
					{
						Shape = ParsePointGeoShape(shape, serializer)
					};
				case "multipoint":
					return new GeoShapeMultiPointQuery
					{
						Shape = ParseMultiPointGeoShape(shape, serializer)
					};
				case "polygon":
					return new GeoShapePolygonQuery
					{
						Shape = ParsePolygonGeoShape(shape, serializer)
					};
				case "multipolygon":
					return new GeoShapeMultiPolygonQuery
					{
						Shape = ParseMultiPolygonGeoShape(shape, serializer)
					};
				case "geometrycollection":
					return new GeoShapeGeometryCollectionQuery
					{
						Shape = ParseGeometryCollection(shape, serializer)
					};
				default:
					return null;
			}
		}

		private GeometryCollection ParseGeometryCollection(JToken shape, JsonSerializer serializer)
		{
			var geometries = shape["geometries"] as JArray;
			if (geometries == null)
				return new GeometryCollection { Geometries = Enumerable.Empty<IGeoShape>() };

			var geoShapes = new List<IGeoShape>(geometries.Count);
			foreach (var geometry in geometries)
			{
				var type = geometry["type"];
				var typeName = type?.Value<string>();
				switch (typeName)
				{
					case "circle":
						var radius = geometry["radius"];
						geoShapes.Add(ParseCircleGeoShape(geometry, serializer, radius));
						break;
					case "envelope":
						geoShapes.Add(ParseEnvelopeGeoShape(geometry, serializer));
						break;
					case "linestring":
						geoShapes.Add(ParseLineStringGeoShape(geometry, serializer));
						break;
					case "multilinestring":
						geoShapes.Add(ParseMultiLineStringGeoShape(geometry, serializer));
						break;
					case "point":
						geoShapes.Add(ParsePointGeoShape(geometry, serializer));
						break;
					case "multipoint":
						geoShapes.Add(ParseMultiPointGeoShape(geometry, serializer));
						break;
					case "polygon":
						geoShapes.Add(ParsePolygonGeoShape(geometry, serializer));
						break;
					case "multipolygon":
						geoShapes.Add(ParseMultiPolygonGeoShape(geometry, serializer));
						break;
					default:
						throw new ArgumentException($"cannot parse geo_shape. unknown type '{typeName}'");
				}
			}

			return new GeometryCollection { Geometries = geoShapes };
		}

		private MultiPolygonGeoShape ParseMultiPolygonGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new MultiPolygonGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>(shape, serializer)
			};
		}

		private PolygonGeoShape ParsePolygonGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new PolygonGeoShape {Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)};
		}

		private MultiPointGeoShape ParseMultiPointGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new MultiPointGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};
		}

		private PointGeoShape ParsePointGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new PointGeoShape {Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer)};
		}

		private MultiLineStringGeoShape ParseMultiLineStringGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new MultiLineStringGeoShape
			{
				Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)
			};
		}

		private LineStringGeoShape ParseLineStringGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new LineStringGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};
		}

		private EnvelopeGeoShape ParseEnvelopeGeoShape(JToken shape, JsonSerializer serializer)
		{
			return new EnvelopeGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};
		}

		private CircleGeoShape ParseCircleGeoShape(JToken shape, JsonSerializer serializer, JToken radius)
		{
			return new CircleGeoShape
			{
				Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer),
				Radius = radius?.Value<string>()
			};
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}

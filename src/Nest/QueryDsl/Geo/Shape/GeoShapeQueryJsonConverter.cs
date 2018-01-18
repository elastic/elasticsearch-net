using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	/// <summary>
	/// Marks an instance where _name and boost do not exist as children of the variable field but as siblings
	/// </summary>
	internal class GeoShapeQueryFieldNameConverter : FieldNameQueryJsonConverter<GeoShapeCircleQuery>
	{
		private static string[] SkipProperties = {"boost", "_name"};
		protected override bool SkipWriteProperty(string propertyName) => SkipProperties.Contains(propertyName);

		protected override void SerializeJson(JsonWriter writer, object value, IFieldNameQuery castValue, JsonSerializer serializer)
		{
			var fieldName = castValue.Field;
			if (fieldName == null) return;

			var settings = serializer.GetConnectionSettings();
			var field = settings?.Inferrer.Field(fieldName);
			if (field.IsNullOrEmpty()) return;

			writer.WriteStartObject();
			var name = castValue.Name;
			var boost = castValue.Boost;
			if (!name.IsNullOrEmpty()) writer.WriteProperty(serializer, "_name", name);
			if (boost != null) writer.WriteProperty(serializer, "boost", boost);
			writer.WritePropertyName(field);
			this.Reserialize(writer, value, serializer);
			writer.WriteEndObject();
		}
	}
	internal class GeoShapeQueryJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public virtual T GetCoordinates<T>(JToken shape, JsonSerializer serializer)
		{
			var coordinates = shape["coordinates"];
			return coordinates != null
				? coordinates.ToObject<T>(serializer)
				: default(T);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues) return null;

			double? boost = null;
			string name  = null;
			if (j.TryGetValue("boost", out var b) && (b.Type != JTokenType.Array && b.Type != JTokenType.Object))
			{
				j.Remove("boost");
				boost = b.Value<double?>();
			}
			if (j.TryGetValue("_name", out var n) && n.Type == JTokenType.String)
			{
				j.Remove("_name");
				name = n.Value<string>();
			}
			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			IGeoShapeQuery query = null;

			if (jo.TryGetValue("shape", out var shape))
				query = ParseShapeQuery(shape, serializer);
			else if (jo.TryGetValue("indexed_shape", out var indexedShape))
				query = ParseIndexedShapeQuery(indexedShape);

			if (query == null) return null;
			var relation = jo["relation"]?.Value<string>().ToEnum<GeoShapeRelation>();
			query.Boost = boost;
			query.Name = name;
			query.Field = field;
			query.Relation = relation;
			return query;
		}

		private IGeoShapeQuery ParseIndexedShapeQuery(JToken indexedShape) =>
			new GeoIndexedShapeQuery {IndexedShape = (indexedShape as JObject)?.ToObject<FieldLookup>()};

		private IGeoShapeQuery ParseShapeQuery(JToken shape, JsonSerializer serializer)
		{
			var type = shape["type"];
			var typeName = type?.Value<string>();
			var ignoreUnmapped = shape["ignore_unmapped"]?.Value<bool?>();
			switch (typeName)
			{
				case "circle":
					var radius = shape["radius"];
					return new GeoShapeCircleQuery
					{
						Shape = SetIgnoreUnmapped(ParseCircleGeoShape(shape, serializer, radius), ignoreUnmapped)
					};
				case "envelope":
					return new GeoShapeEnvelopeQuery
					{
						Shape = SetIgnoreUnmapped(ParseEnvelopeGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "linestring":
					return new GeoShapeLineStringQuery
					{
						Shape = SetIgnoreUnmapped(ParseLineStringGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "multilinestring":
					return new GeoShapeMultiLineStringQuery
					{
						Shape = SetIgnoreUnmapped(ParseMultiLineStringGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "point":
					return new GeoShapePointQuery
					{
						Shape = SetIgnoreUnmapped(ParsePointGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "multipoint":
					return new GeoShapeMultiPointQuery
					{
						Shape = SetIgnoreUnmapped(ParseMultiPointGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "polygon":
					return new GeoShapePolygonQuery
					{
						Shape = SetIgnoreUnmapped(ParsePolygonGeoShape(shape, serializer), ignoreUnmapped)
					};
				case "multipolygon":
					return new GeoShapeMultiPolygonQuery
					{
						Shape = SetIgnoreUnmapped(ParseMultiPolygonGeoShape(shape, serializer), ignoreUnmapped)
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
			if (!(shape["geometries"] is JArray geometries))
				return new GeometryCollection { Geometries = Enumerable.Empty<IGeoShape>() };

			var geoShapes = new List<IGeoShape>(geometries.Count);

			void AddGeoShape<TShape>(TShape s, bool? ignoreUnmapped) where TShape : IGeoShape
			{
				s = SetIgnoreUnmapped(s, ignoreUnmapped);
				geoShapes.Add(s);
			}

			foreach (var geometry in geometries)
			{
				var ignoreUnmapped = geometry["ignore_unmapped"]?.Value<bool?>();
				var type = geometry["type"];
				var typeName = type?.Value<string>();
				switch (typeName)
				{
					case "circle":
						var radius = geometry["radius"];
						AddGeoShape(ParseCircleGeoShape(geometry, serializer, radius), ignoreUnmapped);
						break;
					case "envelope":
						AddGeoShape(ParseEnvelopeGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "linestring":
						AddGeoShape(ParseLineStringGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "multilinestring":
						AddGeoShape(ParseMultiLineStringGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "point":
						AddGeoShape(ParsePointGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "multipoint":
						AddGeoShape(ParseMultiPointGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "polygon":
						AddGeoShape(ParsePolygonGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					case "multipolygon":
						AddGeoShape(ParseMultiPolygonGeoShape(geometry, serializer), ignoreUnmapped);
						break;
					default:
						throw new ArgumentException($"cannot parse geo_shape. unknown type '{typeName}'");
				}
			}

			return new GeometryCollection { Geometries = geoShapes };
		}

		private MultiPolygonGeoShape ParseMultiPolygonGeoShape(JToken shape, JsonSerializer serializer) =>
			new MultiPolygonGeoShape
		{
			Coordinates = GetCoordinates<IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>>>(shape, serializer)
		};

		private PolygonGeoShape ParsePolygonGeoShape(JToken shape, JsonSerializer serializer) =>
			new PolygonGeoShape {Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)};

		private MultiPointGeoShape ParseMultiPointGeoShape(JToken shape, JsonSerializer serializer) =>
			new MultiPointGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private PointGeoShape ParsePointGeoShape(JToken shape, JsonSerializer serializer) =>
			new PointGeoShape {Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer)};

		private MultiLineStringGeoShape ParseMultiLineStringGeoShape(JToken shape, JsonSerializer serializer) => new MultiLineStringGeoShape
		{
			Coordinates = GetCoordinates<IEnumerable<IEnumerable<GeoCoordinate>>>(shape, serializer)
		};

		private LineStringGeoShape ParseLineStringGeoShape(JToken shape, JsonSerializer serializer) =>
			new LineStringGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private EnvelopeGeoShape ParseEnvelopeGeoShape(JToken shape, JsonSerializer serializer) =>
			new EnvelopeGeoShape {Coordinates = GetCoordinates<IEnumerable<GeoCoordinate>>(shape, serializer)};

		private CircleGeoShape ParseCircleGeoShape(JToken shape, JsonSerializer serializer, JToken radius) => new CircleGeoShape
		{
			Coordinates = GetCoordinates<GeoCoordinate>(shape, serializer),
			Radius = radius?.Value<string>()
		};


		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		private static TShape SetIgnoreUnmapped<TShape>(TShape shape, bool? ignoreUnmapped) where TShape : IGeoShape
		{
			shape.IgnoreUnmapped = ignoreUnmapped;
			return shape;
		}
	}
}

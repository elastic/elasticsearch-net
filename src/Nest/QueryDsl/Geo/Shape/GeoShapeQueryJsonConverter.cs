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
		private static readonly string[] SkipProperties = {"boost", "_name"};
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

		private static IGeoShapeQuery ParseIndexedShapeQuery(JToken indexedShape) =>
			new GeoIndexedShapeQuery {IndexedShape = (indexedShape as JObject)?.ToObject<FieldLookup>()};

		private static IGeoShapeQuery ParseShapeQuery(JToken shape, JsonSerializer serializer)
		{
			var type = shape["type"];
			var typeName = type?.Value<string>();
			var ignoreUnmapped = shape["ignore_unmapped"]?.Value<bool?>();

			var geometry = GeoShapeConverter.ReadJToken(shape, serializer);

			switch (typeName)
			{
				case "circle":
					return new GeoShapeCircleQuery
					{
						Shape = SetIgnoreUnmapped(geometry as ICircleGeoShape, ignoreUnmapped)
					};
				case "envelope":
					return new GeoShapeEnvelopeQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IEnvelopeGeoShape, ignoreUnmapped)
					};
				case "linestring":
					return new GeoShapeLineStringQuery
					{
						Shape = SetIgnoreUnmapped(geometry as ILineStringGeoShape, ignoreUnmapped)
					};
				case "multilinestring":
					return new GeoShapeMultiLineStringQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IMultiLineStringGeoShape, ignoreUnmapped)
					};
				case "point":
					return new GeoShapePointQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IPointGeoShape, ignoreUnmapped)
					};
				case "multipoint":
					return new GeoShapeMultiPointQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IMultiPointGeoShape, ignoreUnmapped)
					};
				case "polygon":
					return new GeoShapePolygonQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IPolygonGeoShape, ignoreUnmapped)
					};
				case "multipolygon":
					return new GeoShapeMultiPolygonQuery
					{
						Shape = SetIgnoreUnmapped(geometry as IMultiPolygonGeoShape, ignoreUnmapped)
					};
				case "geometrycollection":
					var geometryCollection = geometry as IGeometryCollection;
					if (geometryCollection != null)
					{
						foreach (var innerGeometry in geometryCollection.Geometries)
							SetIgnoreUnmapped(innerGeometry, ignoreUnmapped);
					}

					return new GeoShapeGeometryCollectionQuery { Shape = geometryCollection };
				default:
					return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		private static TShape SetIgnoreUnmapped<TShape>(TShape shape, bool? ignoreUnmapped) where TShape : IGeoShape
		{
			if (shape != null)
				shape.IgnoreUnmapped = ignoreUnmapped;
			return shape;
		}
	}
}

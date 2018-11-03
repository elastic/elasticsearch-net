using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	/// <summary>
	/// Marks an instance where _name, boost and ignore_unmapped do
	/// not exist as children of the variable field but as siblings
	/// </summary>
	internal class GeoShapeQueryFieldNameConverter : ReserializeJsonConverter<GeoShapeCircleQuery, IGeoShapeQuery>
	{
		private static readonly string[] SkipProperties = { "boost", "_name", "ignore_unmapped" };

		protected override bool SkipWriteProperty(string propertyName) => SkipProperties.Contains(propertyName);

		protected override void SerializeJson(JsonWriter writer, object value, IGeoShapeQuery castValue, JsonSerializer serializer)
		{
			var fieldName = castValue.Field;
			if (fieldName == null) return;

			var settings = serializer.GetConnectionSettings();
			var field = settings?.Inferrer.Field(fieldName);
			if (field.IsNullOrEmpty()) return;

			writer.WriteStartObject();
			var name = castValue.Name;
			var boost = castValue.Boost;
			var ignoreUnmapped = castValue.IgnoreUnmapped;
			if (!name.IsNullOrEmpty()) writer.WriteProperty(serializer, "_name", name);
			if (boost != null) writer.WriteProperty(serializer, "boost", boost);
			if (ignoreUnmapped != null) writer.WriteProperty(serializer, "ignore_unmapped", ignoreUnmapped);
			writer.WritePropertyName(field);
			Reserialize(writer, value, serializer);
			writer.WriteEndObject();
		}
	}

	internal class GeoShapeQueryJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		// TODO: remove in 7.x
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
			string name = null;
			bool? ignoreUnmapped = null;
			if (j.TryGetValue("boost", out var boostToken) && boostToken.Type != JTokenType.Array && boostToken.Type != JTokenType.Object)
			{
				j.Remove("boost");
				boost = boostToken.Value<double?>();
			}
			if (j.TryGetValue("_name", out var nameToken) && nameToken.Type == JTokenType.String)
			{
				j.Remove("_name");
				name = nameToken.Value<string>();
			}
			if (j.TryGetValue("ignore_unmapped", out var ignoreUnmappedToken) && ignoreUnmappedToken.Type == JTokenType.Boolean)
			{
				j.Remove("ignore_unmapped");
				ignoreUnmapped = ignoreUnmappedToken.Value<bool?>();
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
			query.IgnoreUnmapped = ignoreUnmapped;
			return query;
		}

		private static IGeoShapeQuery ParseIndexedShapeQuery(JToken indexedShape) =>
			new GeoIndexedShapeQuery { IndexedShape = (indexedShape as JObject)?.ToObject<FieldLookup>() };

		private static IGeoShapeQuery ParseShapeQuery(JToken shape, JsonSerializer serializer)
		{
			var type = shape["type"];
			var typeName = type?.Value<string>().ToUpperInvariant();
			var geometry = GeoShapeConverter.ReadJToken(shape, serializer);
			switch (typeName)
			{
				case GeoShapeType.Circle:
					return new GeoShapeCircleQuery { Shape = geometry as ICircleGeoShape };
				case GeoShapeType.Envelope:
					return new GeoShapeEnvelopeQuery { Shape = geometry as IEnvelopeGeoShape };
				case GeoShapeType.LineString:
					return new GeoShapeLineStringQuery { Shape = geometry as ILineStringGeoShape };
				case GeoShapeType.MultiLineString:
					return new GeoShapeMultiLineStringQuery { Shape = geometry as IMultiLineStringGeoShape };
				case GeoShapeType.Point:
					return new GeoShapePointQuery { Shape = geometry as IPointGeoShape };
				case GeoShapeType.MultiPoint:
					return new GeoShapeMultiPointQuery { Shape = geometry as IMultiPointGeoShape };
				case GeoShapeType.Polygon:
					return new GeoShapePolygonQuery { Shape = geometry as IPolygonGeoShape };
				case GeoShapeType.MultiPolygon:
					return new GeoShapeMultiPolygonQuery { Shape = geometry as IMultiPolygonGeoShape };
				case GeoShapeType.GeometryCollection:
					return new GeoShapeGeometryCollectionQuery { Shape = geometry as IGeometryCollection };
				default:
					return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class PropertyConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private IElasticsearchProperty GetTypeFromJObject(JObject po, JsonSerializer serializer)
		{
			JToken typeToken;
			JToken propertiesToken;
			var type = string.Empty;

			var hasType = po.TryGetValue("type", out typeToken);
			if (hasType)
				type = typeToken.Value<string>().ToLowerInvariant();
			else if (po.TryGetValue("properties", out propertiesToken))
				type = "object";

			serializer.TypeNameHandling = TypeNameHandling.None;

			switch (type)
			{
				case "string":
					return serializer.Deserialize(po.CreateReader(), typeof(StringProperty)) as StringProperty;
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
					return serializer.Deserialize(po.CreateReader(), typeof(NumberProperty)) as NumberProperty;
				case "date":
					return serializer.Deserialize(po.CreateReader(), typeof(DateProperty)) as DateProperty;
				case "boolean":
					return serializer.Deserialize(po.CreateReader(), typeof(BooleanProperty)) as BooleanProperty;
				case "binary":
					return serializer.Deserialize(po.CreateReader(), typeof(BinaryProperty)) as BinaryProperty;
				case "object":
					return serializer.Deserialize(po.CreateReader(), typeof(ObjectProperty)) as ObjectProperty;
				case "nested":
					return serializer.Deserialize(po.CreateReader(), typeof(NestedProperty)) as NestedProperty;
				case "ip":
					return serializer.Deserialize(po.CreateReader(), typeof(IpProperty)) as IpProperty;
				case "geo_point":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoPointProperty)) as GeoPointProperty;
				case "geo_shape":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoShapeProperty)) as GeoShapeProperty;
				case "attachment":
					return serializer.Deserialize(po.CreateReader(), typeof(AttachmentProperty)) as AttachmentProperty;
			}

			return null;
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			var esType = this.GetTypeFromJObject(o, serializer);
			return esType;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IElasticsearchProperty);
		}

	}
	

	public class FieldMappingConverter : JsonConverter
	{
		private readonly DictionaryKeysAreNotFieldNamesJsonConverter _dictionaryConverter =
			new DictionaryKeysAreNotFieldNamesJsonConverter();

		private readonly PropertyConverter _elasticTypeConverter = new PropertyConverter();

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Dictionary<string, IFieldMapping>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
					 as IFieldMapping;
				if (mapping == null)
				{
					if (name == "_all") mapping = po.ToObject<AllField>();
					if (name == "_id") mapping = po.ToObject<IdField>();
					if (name == "_type") mapping = po.ToObject<TypeField>();
					if (name == "_source") mapping = po.ToObject<SourceField>();
					if (name == "_analyzer") mapping = po.ToObject<AnalyzerField>();
					if (name == "_routing") mapping = po.ToObject<RoutingField>();
					if (name == "_index") mapping = po.ToObject<IndexField>();
					if (name == "_size") mapping = po.ToObject<SizeField>();
					if (name == "_timestamp") mapping = po.ToObject<TimestampField>();
					if (name == "_ttl") mapping = po.ToObject<TtlField>();
					if (name == "_parent") mapping = po.ToObject<ParentField>();
					//TODO _field_names does not seem to have a special mapping (just returns like _uid) needs CONFIRMATION
				}
				if (mapping == null) continue;

				var esType = mapping as IElasticsearchProperty;
				if (esType != null)
				esType.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDictionary<string, IFieldMapping>);
		}
	}

	public class ElasticTypesConverter : JsonConverter
	{
		private readonly DictionaryKeysAreNotFieldNamesJsonConverter _dictionaryConverter = new DictionaryKeysAreNotFieldNamesJsonConverter();
		private readonly PropertyConverter _elasticTypeConverter = new PropertyConverter();

		public override bool CanWrite
		{
			get { return true; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}

	
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var r = new Dictionary<FieldName, IElasticsearchProperty>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
					 as IElasticsearchProperty;
				if (mapping == null)
					continue;
				mapping.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDictionary<string, IElasticsearchProperty>);
		}

	}
}
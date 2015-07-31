using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class ElasticTypeConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private IElasticType GetTypeFromJObject(JObject po, JsonSerializer serializer)
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
					return serializer.Deserialize(po.CreateReader(), typeof(StringType)) as StringType;
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
					return serializer.Deserialize(po.CreateReader(), typeof(NumberType)) as NumberType;
				case "date":
					return serializer.Deserialize(po.CreateReader(), typeof(DateType)) as DateType;
				case "boolean":
					return serializer.Deserialize(po.CreateReader(), typeof(BooleanType)) as BooleanType;
				case "binary":
					return serializer.Deserialize(po.CreateReader(), typeof(BinaryType)) as BinaryType;
				case "object":
					return serializer.Deserialize(po.CreateReader(), typeof(ObjectType)) as ObjectType;
				case "nested":
					return serializer.Deserialize(po.CreateReader(), typeof(NestedType)) as NestedType;
				case "ip":
					return serializer.Deserialize(po.CreateReader(), typeof(IpType)) as IpType;
				case "geo_point":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoPointType)) as GeoPointType;
				case "geo_shape":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoShapeType)) as GeoShapeType;
				case "attachment":
					return serializer.Deserialize(po.CreateReader(), typeof(AttachmentType)) as AttachmentType;
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
			return objectType == typeof(IElasticType);
		}

	}
	

	public class FieldMappingConverter : JsonConverter
	{
		private readonly DictionaryKeysAreNotFieldNamesJsonConverter _dictionaryConverter =
			new DictionaryKeysAreNotFieldNamesJsonConverter();

		private readonly ElasticTypeConverter _elasticTypeConverter = new ElasticTypeConverter();

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

				var esType = mapping as IElasticType;
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
		private readonly ElasticTypeConverter _elasticTypeConverter = new ElasticTypeConverter();

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
			var r = new Dictionary<FieldName, IElasticType>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
					 as IElasticType;
				if (mapping == null)
					continue;
				mapping.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDictionary<string, IElasticType>);
		}

	}
}
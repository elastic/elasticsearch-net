using System;
using System.Collections.Generic;
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
			JToken fieldsToken;
			var type = string.Empty;

			var hasType = po.TryGetValue("type", out typeToken);
			if (hasType)
				type = typeToken.Value<string>().ToLowerInvariant();
			else if (po.TryGetValue("properties", out propertiesToken))
				type = "object";

			var originalType = type;
			var hasFields = po.TryGetValue("fields", out fieldsToken);
			if (hasFields)
				type = "multi_field";

			serializer.TypeNameHandling = TypeNameHandling.None;

			switch (type)
			{
				case "string":
					return serializer.Deserialize(po.CreateReader(), typeof(StringMapping)) as StringMapping;
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
					return serializer.Deserialize(po.CreateReader(), typeof(NumberMapping)) as NumberMapping;
				case "date":
					return serializer.Deserialize(po.CreateReader(), typeof(DateMapping)) as DateMapping;
				case "boolean":
					return serializer.Deserialize(po.CreateReader(), typeof(BooleanMapping)) as BooleanMapping;
				case "binary":
					return serializer.Deserialize(po.CreateReader(), typeof(BinaryMapping)) as BinaryMapping;
				case "object":
					return serializer.Deserialize(po.CreateReader(), typeof(ObjectMapping)) as ObjectMapping;
				case "nested":
					return serializer.Deserialize(po.CreateReader(), typeof(NestedObjectMapping)) as NestedObjectMapping;
				case "multi_field":
					var m =serializer.Deserialize(po.CreateReader(), typeof(MultiFieldMapping)) as MultiFieldMapping;
					m.Type = originalType;
					return m;
				case "ip":
					return serializer.Deserialize(po.CreateReader(), typeof(IPMapping)) as IPMapping;
				case "geo_point":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoPointMapping)) as GeoPointMapping;
				case "geo_shape":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoShapeMapping)) as GeoShapeMapping;
				case "attachment":
					return serializer.Deserialize(po.CreateReader(), typeof(AttachmentMapping)) as AttachmentMapping;
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


	public class ElasticTypesConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return true; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			new DictionaryKeysAreNotPropertyNamesJsonConverter().WriteJson(writer, value, serializer);
		}

	
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var r = new Dictionary<PropertyNameMarker, IElasticType>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = new ElasticTypeConverter().ReadJson(po.CreateReader(), objectType, existingValue, serializer)
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
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertiesJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter _dictionaryConverter = new VerbatimDictionaryKeysJsonConverter();
		private readonly PropertyJsonConverter _elasticTypeConverter = new PropertyJsonConverter();

		public override bool CanConvert(Type objectType) => objectType == typeof(IProperties);
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dict = value as IDictionary<PropertyName, IProperty>;
			if (dict == null) return;
			var settings = serializer.GetConnectionSettings();
			var props = new Properties();
			foreach (var kv in dict)
			{
				var v = kv.Value as IPropertyWithClrOrigin;
				if (v?.ClrOrigin == null)
				{
					props.Add(kv.Key, kv.Value);
					continue;
				}
				//We do not have to take .Name into account from serializer PropertyName (kv.Key) already handles this
				var serializerMapping = settings.Serializer?.CreatePropertyMapping(v.ClrOrigin);
				if (serializerMapping == null || !serializerMapping.Ignore) 
					props.Add(kv.Key, kv.Value);
			}
			_dictionaryConverter.WriteJson(writer, props, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Properties();
			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
				as IProperty;
				if (mapping == null)
					continue;
				mapping.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

	}
}
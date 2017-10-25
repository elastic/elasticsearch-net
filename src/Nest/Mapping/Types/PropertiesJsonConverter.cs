using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertiesJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter<PropertyName, IProperty> _dictionaryConverter =
			new VerbatimDictionaryKeysJsonConverter<PropertyName, IProperty>();

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
				var propertyInfo = v?.ClrOrigin;
				if (propertyInfo == null)
				{
					props.Add(kv.Key, kv.Value);
					continue;
				}
				// Check against connection settings mappings
                IPropertyMapping propertyMapping;
				if (settings.PropertyMappings.TryGetValue(propertyInfo, out propertyMapping))
				{
					if (propertyMapping.Ignore) continue;
					props.Add(propertyMapping.Name, kv.Value);
					continue;
				}
				// Check against attribute mapping, CreatePropertyMapping caches.
				// We do not have to take .Name into account from serializer PropertyName (kv.Key) already handles this
				propertyMapping = settings.PropertyMappingProvider?.CreatePropertyMapping(propertyInfo);
				if (propertyMapping  == null || !propertyMapping.Ignore)
					props.Add(kv.Key, kv.Value);
			}
			_dictionaryConverter.WriteJson(writer, props, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Properties();
			var o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null) continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer) as IProperty;
				if (mapping == null) continue;

				mapping.Name = name;

				r.Add(name, mapping);
			}
			return r;
		}

	}
}

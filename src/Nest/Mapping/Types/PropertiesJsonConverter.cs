using System;
using System.Collections.Generic;
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
			if (!(value is IDictionary<PropertyName, IProperty> dict)) return;
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
				if (settings.PropertyMappings.TryGetValue(propertyInfo, out var propertyMapping))
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
			var s = serializer.GetConnectionSettings();
			var r = new Properties(s);
			var o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				if (!(p.First is JObject po)) continue;
				if (!(_elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer) is IProperty mapping)) continue;

				mapping.Name = name;
				r.Add(name, mapping);
			}
			return r;
		}

	}
}

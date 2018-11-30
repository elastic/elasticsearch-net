using System.Collections.Generic;
using System.Linq;
using Utf8Json;

namespace Nest
{
	internal class PropertiesFormatter : IJsonFormatter<Properties>
	{
		public Properties Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var settings = formatterResolver.GetConnectionSettings();
			var properties = new Properties(settings);
			var propertyFormatter = formatterResolver.GetFormatter<IProperty>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var name = reader.ReadPropertyName();
				if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				{
					reader.ReadNext();
					continue;
				}

				var property = propertyFormatter.Deserialize(ref reader, formatterResolver);
				property.Name = name;
				properties.Add(name, property);
			}

			return properties;
		}

		public void Serialize(ref JsonWriter writer, Properties value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var properties = new Dictionary<PropertyName, IProperty>(value.Count());

			foreach (var kv in value)
			{
				var clrOrigin = kv.Value as IPropertyWithClrOrigin;
				var propertyInfo = clrOrigin?.ClrOrigin;
				if (propertyInfo == null)
				{
					properties.Add(kv.Key, kv.Value);
					continue;
				}
				// Check against connection settings mappings
				if (settings.PropertyMappings.TryGetValue(propertyInfo, out var propertyMapping))
				{
					if (propertyMapping.Ignore) continue;

					properties.Add(propertyMapping.Name, kv.Value);
					continue;
				}
				// Check against attribute mapping, CreatePropertyMapping caches.
				// We do not have to take .Name into account from serializer PropertyName (kv.Key) already handles this
				propertyMapping = settings.PropertyMappingProvider?.CreatePropertyMapping(propertyInfo);
				if (propertyMapping == null || !propertyMapping.Ignore)
					properties.Add(kv.Key, kv.Value);
			}

			var formatter = formatterResolver.GetFormatter<Dictionary<PropertyName, IProperty>>();
			formatter.Serialize(ref writer, properties, formatterResolver);
		}
	}
}

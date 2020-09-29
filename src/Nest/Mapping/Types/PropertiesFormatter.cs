// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;
namespace Nest
{
	internal class PropertiesFormatter : IJsonFormatter<IProperties>
	{
		private static readonly InterfaceDictionaryFormatter<PropertyName, IProperty> Formatter =
			new InterfaceDictionaryFormatter<PropertyName, IProperty>();

		public IProperties Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
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
					reader.ReadNextBlock();
					continue;
				}

				var property = propertyFormatter.Deserialize(ref reader, formatterResolver);
				property.Name = name;
				properties.Add(name, property);
			}

			return properties;
		}

		public void Serialize(ref JsonWriter writer, IProperties value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();

			// HACK: Deduplicate property mappings with an instance of Properties that has access to ConnectionSettings to sanitize PropertyName keys
			var properties = new Properties(settings);

			foreach (var kv in value)
			{
				var clrOrigin = kv.Value as IPropertyWithClrOrigin;
				var propertyInfo = clrOrigin?.ClrOrigin;
				if (propertyInfo == null)
				{
					properties[kv.Key] = kv.Value;
					continue;
				}
				// Check against connection settings mappings
				if (settings.PropertyMappings.TryGetValue(propertyInfo, out var propertyMapping))
				{
					if (propertyMapping.Ignore)
						continue;

					properties[propertyMapping.Name] = kv.Value;
					continue;
				}
				// Check against attribute mapping, CreatePropertyMapping caches.
				// We do not have to take .Name into account from serializer PropertyName (kv.Key) already handles this
				propertyMapping = settings.PropertyMappingProvider?.CreatePropertyMapping(propertyInfo);
				if (propertyMapping == null || !propertyMapping.Ignore)
					properties[kv.Key] = kv.Value;
			}

			Formatter.Serialize(ref writer, properties, formatterResolver);
		}
	}
}

using Utf8Json;

namespace Nest
{
	internal class PropertiesFormatter : IJsonFormatter<IProperties>
	{
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
					reader.ReadNext();
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
			var propertyNameFormatter = formatterResolver.GetFormatter<PropertyName>();
			var propertyFormatter = formatterResolver.GetFormatter<IProperty>();
			var written = false;

			writer.WriteBeginObject();

			foreach (var kv in value)
			{
				var clrOrigin = kv.Value as IPropertyWithClrOrigin;
				var propertyInfo = clrOrigin?.ClrOrigin;
				if (propertyInfo == null)
				{
					if (written)
						writer.WriteValueSeparator();

					propertyNameFormatter.Serialize(ref writer, kv.Key, formatterResolver);
					writer.WriteNameSeparator();
					propertyFormatter.Serialize(ref writer, kv.Value, formatterResolver);
					written = true;
					continue;
				}
				// Check against connection settings mappings
				if (settings.PropertyMappings.TryGetValue(propertyInfo, out var propertyMapping))
				{
					if (propertyMapping.Ignore)
						continue;

					if (written)
						writer.WriteValueSeparator();

					writer.WritePropertyName(propertyMapping.Name);
					propertyFormatter.Serialize(ref writer, kv.Value, formatterResolver);
					written = true;
					continue;
				}
				// Check against attribute mapping, CreatePropertyMapping caches.
				// We do not have to take .Name into account from serializer PropertyName (kv.Key) already handles this
				propertyMapping = settings.PropertyMappingProvider?.CreatePropertyMapping(propertyInfo);
				if (propertyMapping == null || !propertyMapping.Ignore)
				{
					if (written)
						writer.WriteValueSeparator();

					propertyNameFormatter.Serialize(ref writer, kv.Key, formatterResolver);
					writer.WriteNameSeparator();
					propertyFormatter.Serialize(ref writer, kv.Value, formatterResolver);
					written = true;
				}
			}

			writer.WriteEndObject();
		}
	}
}

using Utf8Json;

namespace Nest
{
	internal class PropertyNameFormatter : IJsonFormatter<PropertyName>
	{
		public PropertyName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
			{
				reader.ReadNext();
				return null;
			}

			PropertyName propertyName = reader.ReadString();
			return propertyName;
		}

		public void Serialize(ref JsonWriter writer, PropertyName value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var infer = formatterResolver.GetConnectionSettings().Inferrer;
			writer.WriteString(infer.PropertyName(value));
		}
	}
}

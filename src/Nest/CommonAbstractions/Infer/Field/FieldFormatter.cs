using Utf8Json;

namespace Nest
{
	internal class FieldFormatter : IJsonFormatter<Field>
	{
		public Field Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
				return null;

			Field field = reader.ReadString();
			return field;
		}

		public void Serialize(ref JsonWriter writer, Field value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			writer.WriteString(settings.Inferrer.Field(value));
		}
	}
}

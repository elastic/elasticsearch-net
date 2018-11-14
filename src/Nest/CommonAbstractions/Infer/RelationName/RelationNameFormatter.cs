using Utf8Json;

namespace Nest
{
	internal class RelationNameFormatter : IJsonFormatter<RelationName>
	{
		public RelationName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
			{
				RelationName relationName = reader.ReadString();
				return relationName;
			}

			return null;
		}

		public void Serialize(ref JsonWriter writer, RelationName value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			writer.WriteString(settings.Inferrer.RelationName(value));
		}
	}
}

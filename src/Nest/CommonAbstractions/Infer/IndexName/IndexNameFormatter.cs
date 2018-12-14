using Utf8Json;

namespace Nest
{
	internal class IndexNameFormatter : IJsonFormatter<IndexName>, IObjectPropertyNameFormatter<IndexName>
	{
		public IndexName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String) return null;

			IndexName indexName = reader.ReadString();
			return indexName;
		}

		public void Serialize(ref JsonWriter writer, IndexName value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var indexName = settings.Inferrer.IndexName(value);
			writer.WriteString(indexName);
		}

		public void SerializeToPropertyName(ref JsonWriter writer, IndexName value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver);

		public IndexName DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);

	}
}

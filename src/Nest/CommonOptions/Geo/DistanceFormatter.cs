using Elasticsearch.Net;

namespace Nest
{
	internal class DistanceFormatter : IJsonFormatter<Distance>
	{
		public Distance Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
				return null;

			var value = reader.ReadString();
			return value == null
				? null
				: new Distance(value);
		}

		public void Serialize(ref JsonWriter writer, Distance value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteString(value.ToString());
		}
	}
}

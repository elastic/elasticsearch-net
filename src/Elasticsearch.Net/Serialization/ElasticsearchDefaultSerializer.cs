using System.Linq;

namespace Elasticsearch.Net
{
	public class ElasticsearchDefaultSerializer : IElasticsearchSerializer
	{
		public T Deserialize<T>(byte[] bytes) where T : class
		{
			return SimpleJson.DeserializeObject<T>(bytes.Utf8String());
		}

		public byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serialized = SimpleJson.SerializeObject(data);
			if (formatting == SerializationFormatting.None)
				serialized = RemoveNewLinesAndTabs(serialized);
			return serialized.Utf8Bytes();
		}

		public static string RemoveNewLinesAndTabs(string input)
		{
			return new string(input
				.Where(c => c != '\r' && c != '\n')
				.ToArray());
		}
	}
}
using System;
using System.Linq;

namespace Elasticsearch.Net.Serialization
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(byte[] bytes) where T : class;
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);
	}
}
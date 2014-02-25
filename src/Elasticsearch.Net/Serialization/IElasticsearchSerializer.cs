using System;
using System.Linq;

namespace Elasticsearch.Net
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(byte[] bytes) where T : class;
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);
	}
}
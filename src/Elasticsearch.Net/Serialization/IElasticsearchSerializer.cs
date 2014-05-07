using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Serialization
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(Stream stream);
		Task<T> DeserializeAsync<T>(Stream stream);
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);
	}
}
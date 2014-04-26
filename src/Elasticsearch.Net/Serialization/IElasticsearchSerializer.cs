using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Serialization
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(IElasticsearchResponse response, Stream stream, object deserializeState);
		Task<T> DeserializeAsync<T>(IElasticsearchResponse reponse, Stream stream, object deserializeState);
		byte[] Serialize(object data, SerializationFormatting formatting = SerializationFormatting.Indented);
	}
}
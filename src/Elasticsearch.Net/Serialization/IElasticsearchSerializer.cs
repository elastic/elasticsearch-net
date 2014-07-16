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

		/// <summary>
		/// Used to stringify valuetypes to string (i.e querystring parameters and route parameters).
		/// </summary>
		string Stringify(object valueType);
	}
}
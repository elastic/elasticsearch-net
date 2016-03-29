using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(Stream stream);

		Task<T> DeserializeAsync<T>(Stream responseStream, CancellationToken cancellationToken = default(CancellationToken));

		void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented);

		IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
	}

	public static class ElasticsearchSerializerExtensions
	{
		public static byte[] SerializeToBytes(this IElasticsearchSerializer serializer, object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.ToArray();
			}
		}
		public static string SerializeToString(this IElasticsearchSerializer serializer, object data, SerializationFormatting formatting = SerializationFormatting.Indented) => 
			serializer.SerializeToBytes(data, formatting).Utf8String();
	}

}
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ElasticsearchDefaultSerializer : IElasticsearchSerializer
	{
		private const int BufferSize = 81920;

		public static readonly ElasticsearchDefaultSerializer Instance = new ElasticsearchDefaultSerializer();
		private static readonly ElasticsearchNetJsonStrategy Strategy = new ElasticsearchNetJsonStrategy();

		public IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo) => null;

		public T Deserialize<T>(Stream stream)
		{
			if (stream == null) return default(T);

			using (var ms = new MemoryStream())
			using (stream)
			{
				stream.CopyTo(ms);
				var buffer = ms.ToArray();
				if (buffer.Length <= 1)
					return default(T);

				return SimpleJson.DeserializeObject<T>(buffer.Utf8String(), Strategy);
			}
		}

		public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
				return default(T);

			using (var ms = new MemoryStream())
			using (stream)
			{
				await stream.CopyToAsync(ms, BufferSize, cancellationToken).ConfigureAwait(false);
				var buffer = ms.ToArray();
				if (buffer.Length <= 1)
					return default(T);

				var r = SimpleJson.DeserializeObject<T>(buffer.Utf8String(), Strategy);
				return r;
			}
		}

		public void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serialized = SimpleJson.SerializeObject(data, Strategy);
			if (formatting == SerializationFormatting.None)
				serialized = RemoveNewLinesAndTabs(serialized);
			using (var ms = new MemoryStream(serialized.Utf8Bytes())) ms.CopyTo(writableStream);
		}

		private static string RemoveNewLinesAndTabs(string input) => new string(input
			.Where(c => c != '\r' && c != '\n')
			.ToArray());
	}
}

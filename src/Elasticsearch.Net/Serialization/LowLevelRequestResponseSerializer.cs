using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer
	{
		private const int BufferSize = 81920;
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();
		private static readonly ElasticsearchNetJsonStrategy Strategy = new ElasticsearchNetJsonStrategy();

		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null) return Default(type);

			using (var ms = new MemoryStream())
			using (stream)
			{
				stream.CopyTo(ms);
				if (ms.Length <= 1) return Default(type);

				return SimpleJson.DeserializeObject(ms.Utf8CharArray(), type, Strategy);
			}
		}

		public T Deserialize<T>(Stream stream) => (T)Deserialize(typeof(T), stream);

		public async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null) return Default(type);

			using (var ms = new MemoryStream())
			using (stream)
			{
				await stream.CopyToAsync(ms, BufferSize, cancellationToken).ConfigureAwait(false);
				if (ms.Length <= 1) return Default(type);

				var r = SimpleJson.DeserializeObject(ms.Utf8CharArray(), type, Strategy);
				return r;
			}
		}

		public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			var o = await DeserializeAsync(typeof(T), stream, cancellationToken).ConfigureAwait(false);
			return (T)o;
		}

		public void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serialized = SimpleJson.SerializeObject(data, Strategy);
			if (formatting == SerializationFormatting.None) serialized = RemoveNewLinesAndTabs(serialized);
			using (var ms = new MemoryStream(serialized.Utf8Bytes())) ms.CopyTo(writableStream);
		}

		public async Task SerializeAsync<T>(T data, Stream writableStream, SerializationFormatting formatting,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			var serialized = SimpleJson.SerializeObject(data, Strategy);
			if (formatting == SerializationFormatting.None) serialized = RemoveNewLinesAndTabs(serialized);
			using (var ms = new MemoryStream(serialized.Utf8Bytes())) await ms.CopyToAsync(writableStream).ConfigureAwait(false);
		}

		private static object Default(Type type) => type.IsValueType ? type.CreateInstance() : null;

		private static string RemoveNewLinesAndTabs(string input) => new string(input
			.Where(c => c != '\r' && c != '\n')
			.ToArray());
	}
}

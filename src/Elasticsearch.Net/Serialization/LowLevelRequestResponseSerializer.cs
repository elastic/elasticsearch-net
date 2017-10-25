using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer
	{
		private static readonly ElasticsearchNetJsonStrategy Strategy = new ElasticsearchNetJsonStrategy();
		private const int BufferSize = 81920;

		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();


		public object Default(Type type) => type.IsValueType() ? type.CreateInstance() : null;
		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null) return Default(type);

			using (var ms = new MemoryStream())
			using(stream)
			{
				stream.CopyTo(ms);
				var buffer = ms.ToArray();
				if (buffer.Length <= 1) return Default(type);
				return SimpleJson.DeserializeObject(buffer.Utf8String(), type, Strategy);
			}
		}

		public T Deserialize<T>(Stream stream) => (T) this.Deserialize(typeof(T), stream);

		public async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null) return Default(type);

			using (var ms = new MemoryStream())
			using (stream)
			{
				await stream.CopyToAsync(ms, BufferSize, cancellationToken).ConfigureAwait(false);
				var buffer = ms.ToArray();
				if (buffer.Length <= 1) return Default(type);
				var r = SimpleJson.DeserializeObject(buffer.Utf8String(), type, Strategy);
				return r;
			}
		}

		public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			var o = await this.DeserializeAsync(typeof(T), stream, cancellationToken);
			return (T) o;
		}

		public void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serialized = SimpleJson.SerializeObject(data, LowLevelRequestResponseSerializer.Strategy);
			if (formatting == SerializationFormatting.None)
				serialized = RemoveNewLinesAndTabs(serialized);
			using (var ms = new MemoryStream(serialized.Utf8Bytes()))
			{
				ms.CopyTo(writableStream);
			}
		}

		private static string RemoveNewLinesAndTabs(string input)
		{
			return new string(input
				.Where(c => c != '\r' && c != '\n')
				.ToArray());
		}
	}
}

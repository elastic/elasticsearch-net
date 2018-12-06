using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class InternalSerializer : IElasticsearchSerializer
	{
		internal const int DefaultBufferSize = 1024;

		internal static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);

		public InternalSerializer(IConnectionSettingsValues settings, IJsonFormatterResolver formatterResolver)
		{
			Settings = settings;
			FormatterResolver = formatterResolver;
		}

		public IJsonFormatterResolver FormatterResolver { get; }

		/// <summary>
		/// The size of the buffer to use when writing the serialized request
		/// to the request stream
		/// </summary>
		// Performance tests as part of https://github.com/elastic/elasticsearch-net/issues/1899 indicate this
		// to be a good compromise buffer size for performance throughput and bytes allocated.
		protected virtual int BufferSize => DefaultBufferSize;

		protected IConnectionSettingsValues Settings { get; }

		public T Deserialize<T>(Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return default(T);

			return JsonSerializer.Deserialize<T>(stream, FormatterResolver);
		}

		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return type.DefaultValue();

			return JsonSerializer.NonGeneric.Deserialize(type, stream, FormatterResolver);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(default(T));

			return JsonSerializer.DeserializeAsync<T>(stream, FormatterResolver);
		}


		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(type.DefaultValue());

			return JsonSerializer.NonGeneric.DeserializeAsync(type, stream, FormatterResolver);
		}

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented) =>
			JsonSerializer.Serialize(writableStream, data, FormatterResolver);

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken)
		) => JsonSerializer.SerializeAsync(stream, data, FormatterResolver);
	}
}

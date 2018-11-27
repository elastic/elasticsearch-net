using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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
			Utf8Json.JsonSerializer.SetDefaultResolver(formatterResolver);
		}

		/// <summary>
		/// The size of the buffer to use when writing the serialized request
		/// to the request stream
		/// </summary>
		// Performance tests as part of https://github.com/elastic/elasticsearch-net/issues/1899 indicate this
		// to be a good compromise buffer size for performance throughput and bytes allocated.
		protected virtual int BufferSize => DefaultBufferSize;

		protected IConnectionSettingsValues Settings { get; }
		public IJsonFormatterResolver FormatterResolver { get; }

		public T Deserialize<T>(Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return default(T);
			return Utf8Json.JsonSerializer.Deserialize<T>(stream);
		}

		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return type.DefaultValue();
			return Utf8Json.JsonSerializer.NonGeneric.Deserialize(type, stream);
		}

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			Utf8Json.JsonSerializer.Serialize(writableStream, data); // TODO: indenting
		}

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			return Utf8Json.JsonSerializer.SerializeAsync(stream, data); // TODO: indenting
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(default(T));
			return Utf8Json.JsonSerializer.DeserializeAsync<T>(stream);
		}


		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(type.DefaultValue());
			return Utf8Json.JsonSerializer.NonGeneric.DeserializeAsync(type, stream);
		}
	}
}

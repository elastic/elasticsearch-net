using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase : IElasticsearchSerializer
	{
		// Default buffer size of StreamWriter, which is private :(
		internal const int DefaultBufferSize = 1024;

		internal static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		protected virtual int BufferSize => DefaultBufferSize;

		private readonly JsonSerializer _serializer;
		private readonly JsonSerializer _collapsedSerializer;

		public T Deserialize<T>(Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize<T>(jsonTextReader);
		}

		public object Deserialize(Type type, Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize(jsonTextReader, type);
		}

		public virtual async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await jsonTextReader.ReadTokenWithDateParseHandlingNoneAsync(cancellationToken).ConfigureAwait(false);
				return token.ToObject<T>(this._serializer);
			}
		}

		public virtual async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await jsonTextReader.ReadTokenWithDateParseHandlingNoneAsync(cancellationToken).ConfigureAwait(false);
				return token.ToObject(type, this._serializer);
			}
		}

		public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				var serializer = formatting == SerializationFormatting.Indented ? _serializer : _collapsedSerializer;
				serializer.Serialize(jsonWriter, data);
			}
		}

		private static readonly Task CompletedTask = Task.CompletedTask;

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			//This makes no sense now but we need the async method on the interface in 6.x so we can start swapping this out
			//for an implementation that does make sense without having to wait for 7.x
			this.Serialize(data, stream, formatting);
			return CompletedTask;
		}
	}
}

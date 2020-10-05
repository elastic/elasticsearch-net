// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Newtonsoft.Json;

namespace Nest.JsonNetSerializer
{
	public abstract partial class ConnectionSettingsAwareSerializerBase : ITransportSerializer
	{
		// Default buffer size of StreamWriter, which is private :(
		internal const int DefaultBufferSize = 1024;

		private static readonly Task CompletedTask = Task.CompletedTask;

		internal static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		private readonly JsonSerializer _collapsedSerializer;

		private readonly JsonSerializer _serializer;
		protected virtual int BufferSize => DefaultBufferSize;

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

		public virtual async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await jsonTextReader.ReadTokenWithDateParseHandlingNoneAsync(cancellationToken).ConfigureAwait(false);
				return token.ToObject<T>(_serializer);
			}
		}

		public virtual async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await jsonTextReader.ReadTokenWithDateParseHandlingNoneAsync(cancellationToken).ConfigureAwait(false);
				return token.ToObject(type, _serializer);
			}
		}

		public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				var serializer = formatting == SerializationFormatting.Indented ? _serializer : _collapsedSerializer;
				serializer.Serialize(jsonWriter, data);
			}
		}

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		)
		{
			//This makes no sense now but we need the async method on the interface in 6.x so we can start swapping this out
			//for an implementation that does make sense without having to wait for 7.x
			Serialize(data, stream, formatting);
			return CompletedTask;
		}
	}
}

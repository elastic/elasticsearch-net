// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class DefaultHighLevelSerializer : ITransportSerializer
	{
		private static readonly JsonSerializerOptions Options = new()
		{
			Converters = { new JsonStringEnumConverter() }
		};

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload
		public T Deserialize<T>(Stream stream)
		{
			if (stream.Length == 0) return default;
			using var reader = new StreamReader(stream);
			return JsonSerializer.Deserialize<T>(reader.ReadToEnd(), Options);
		}

		public object Deserialize(Type type, Stream stream) =>
			throw new NotImplementedException();

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			stream.Length == 0 ? Task.FromResult(default(T)) : JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask();

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken).AsTask();

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload
		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var json = JsonSerializer.Serialize(data);
			using var writer = new StreamWriter(writableStream);
			writer.Write(json);
		}

		public Task SerializeAsync<T>(T data, Stream stream,
			SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => JsonSerializer.SerializeAsync(stream, data, Options, cancellationToken);
	}
}

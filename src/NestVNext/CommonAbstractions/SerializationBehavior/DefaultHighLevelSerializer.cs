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
			Converters = {new JsonStringEnumConverter()}
		};
		
		public T Deserialize<T>(Stream stream) =>
			throw new NotImplementedException();

		public object Deserialize(Type type, Stream stream) =>
			throw new NotImplementedException();

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask();

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken).AsTask();

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			throw new NotImplementedException();

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => throw new NotImplementedException();
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization
{
	internal class BulkOperationsNewlineDelimitedJsonSerializer : Serializer
	{
		private readonly IElasticsearchClientSettings _settings;

		public BulkOperationsNewlineDelimitedJsonSerializer(IElasticsearchClientSettings settings) => _settings = settings;

		public override object Deserialize(Type type, Stream stream) => throw new NotImplementedException();
		public override T Deserialize<T>(Stream stream) => throw new NotImplementedException();
		public override Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) => throw new NotImplementedException();
		public override Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (data is not IStreamSerializable streamSerializable)
				throw new InvalidOperationException("TODO");

			streamSerializable.Serialize(stream, _settings, formatting);
		}

		public override Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None, CancellationToken cancellationToken = default) => throw new NotImplementedException();
	}
}

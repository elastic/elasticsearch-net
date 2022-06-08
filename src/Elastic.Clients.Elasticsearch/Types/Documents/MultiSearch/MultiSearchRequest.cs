// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	// STUBBED POC

	public partial class MultiSearchRequest : IStreamSerializable
	{
		public List<RequestItem> Searches { get; set; }

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (Searches is null)
				return;

			foreach (var search in Searches)
			{
				if (search is IStreamSerializable serializable)
					serializable.Serialize(stream, settings, formatting);
			}
		}

		async Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (Searches is null)
				return;

			foreach (var search in Searches)
			{
				if (search is IStreamSerializable serializable)
					await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
			}
		}
	}

	public class RequestItem : IStreamSerializable
	{
		public RequestItem(MultisearchBody body) => Body = body;

		public RequestItem(MultisearchHeader header, MultisearchBody body)
		{
			Header = header;
			Body = body;
		}	

		public MultisearchHeader Header { get; init; }
		public MultisearchBody Body { get; init; }

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (Body is null)
				return;

			if (settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
			{
				JsonSerializer.Serialize(stream, Header, options);
				stream.WriteByte((byte)'\n');
				JsonSerializer.Serialize(stream, Body, options);
				stream.WriteByte((byte)'\n');
			}
		}

		async Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (Body is null)
				return;

			if (settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
			{
				await JsonSerializer.SerializeAsync(stream, Header, options).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
				await JsonSerializer.SerializeAsync(stream, Body, options).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
			}
		}
	}
}

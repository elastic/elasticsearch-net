// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class MultiSearchRequestDescriptor<TDocument>
	{
		internal override void BeforeRequest() => TypedKeys(true);
	}

	public sealed partial class MultiSearchRequestDescriptor
	{
		internal override void BeforeRequest() => TypedKeys(true);
	}

	public partial class MultiSearchRequest
	{
		internal override void BeforeRequest() => TypedKeys = true;
	}

	// POC - If we have more than one union doing this, can we autogenerate with correct ctors etc.
	public class SearchRequestItem : IStreamSerializable
	{
		public SearchRequestItem(MultisearchBody body) => Body = body;

		public SearchRequestItem(MultisearchHeader header, MultisearchBody body)
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

	public partial class MultiSearchResponse<TDocument>
	{
		public override bool IsValid => base.IsValid && (Responses?.All(b => b.Item1 is not null && b.Item1.Status == 200) ?? true);

		[JsonIgnore]
		public int TotalResponses => Responses.HasAny() ? Responses.Count() : 0;
	}
}

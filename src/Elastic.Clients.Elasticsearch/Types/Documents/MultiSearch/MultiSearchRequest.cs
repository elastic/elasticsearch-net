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
	// STUBBED POC

	public sealed partial class MultiSearchRequestDescriptor<TDocument> : IStreamSerializable
	{
		// Temporary implementation - TODO - Code gen properly

		private List<RequestItem> _searches = new List<RequestItem>();

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (_searches is null)
				return;

			foreach (var search in _searches)
			{
				if (search is IStreamSerializable serializable)
					serializable.Serialize(stream, settings, formatting);
			}
		}

		async Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			if (_searches is null)
				return;

			foreach (var search in _searches)
			{
				if (search is IStreamSerializable serializable)
					await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
			}
		}

		public MultiSearchRequestDescriptor<TDocument> AddSearch(RequestItem item)
		{
			_searches.Add(item);
			return this;
		}

		internal override void BeforeRequest() => TypedKeys(true);
	}

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

		internal override void BeforeRequest() => TypedKeys = true;
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

	public partial class ResponseBody<TDocument>
	{
		[JsonIgnore]
		public IReadOnlyCollection<Hit<TDocument>> Hits => HitsMetadata.Hits;

		[JsonIgnore]
		public IReadOnlyCollection<TDocument> Documents => HitsMetadata.Hits.Select(s => s.Source).ToReadOnlyCollection();

		[JsonIgnore]
		public long Total => HitsMetadata?.Total?.Value ?? -1;
	}

	public partial class MultiSearchResponse<TDocument>
	{
		public override bool IsValid => base.IsValid && Responses.All(b => b.Item1 is not null && b.Item1.Status == 200);

		[JsonIgnore]
		public int TotalResponses => Responses.HasAny() ? Responses.Count() : 0;
	}
}

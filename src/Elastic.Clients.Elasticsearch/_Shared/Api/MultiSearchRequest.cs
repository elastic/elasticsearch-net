// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class MultiSearchResponse<TDocument>
{
	public override bool IsValidResponse => base.IsValidResponse && (Responses?.All(b => b.Value1 is not null && b.Value1.Status == 200) ?? true);

	[JsonIgnore]
	public int TotalResponses => Responses.Count > 0 ? Responses.Count : 0;
}

public partial class MultiSearchRequest : IStreamSerializable
{
	// Any request may contain aggregations so we force `typed_keys` in order to successfully
	// deserialize them.
	internal override void BeforeRequest()
	{
		if (Searches.Any(x => x.Body.Aggregations is { Count: > 0 } || x.Body.Suggest is { Suggesters: { Count: > 0 } }))
		{
			TypedKeys ??= true;
		}
	}

	void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		if (Searches is null)
			return;
		foreach (var item in Searches)
		{
			if (item is IStreamSerializable serializable)
				serializable.Serialize(stream, settings, formatting);
		}
	}

	async Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		if (Searches is null)
			return;
		foreach (var item in Searches)
		{
			if (item is IStreamSerializable serializable)
				await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
		}
	}
}

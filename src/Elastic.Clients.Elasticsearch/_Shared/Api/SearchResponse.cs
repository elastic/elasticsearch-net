// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public partial class SearchResponse<TDocument>
{
	[JsonIgnore]
	public IReadOnlyCollection<Core.Search.Hit<TDocument>> Hits => HitsMetadata.Hits;

	[JsonIgnore]
	public IReadOnlyCollection<TDocument> Documents => HitsMetadata.Hits.Select(s => s.Source).ToReadOnlyCollection();

	[JsonIgnore]
	public long Total => HitsMetadata?.Total?.Item1?.Value ?? HitsMetadata?.Total?.Item2 ?? -1;
}

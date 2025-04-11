// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class AsyncSearch<TDocument>
{
	/// <summary>
	/// Shortcut to the hits returned for this search.
	/// </summary>
	[JsonIgnore]
	public IReadOnlyCollection<Core.Search.Hit<TDocument>> Hits => HitsMetadata.Hits;

	/// <summary>
	/// The source documents from the matching hits.
	/// </summary>
	[JsonIgnore]
	public IReadOnlyCollection<TDocument> Documents => HitsMetadata.Hits.Select(s => s.Source).ToReadOnlyCollection();

	/// <summary>
	/// The total number of hits returned for this search.
	/// </summary>
	[JsonIgnore]
	public long Total => HitsMetadata?.Total?.Value1?.Value ?? HitsMetadata?.Total?.Value2 ?? -1;
}

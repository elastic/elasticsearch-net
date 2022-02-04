// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class GetAsyncSearchResponse<TDocument>
{
	[JsonIgnore]
	public IReadOnlyCollection<Hit<TDocument>> Hits => Response.HitsMetadata.Hits;

	[JsonIgnore]
	public IReadOnlyCollection<TDocument> Documents => Response.HitsMetadata.Hits.Select(s => s.Source).ToReadOnlyCollection();

	[JsonIgnore]
	public long Total => Response.HitsMetadata?.Total?.Value ?? -1;
}

public abstract partial class AsyncSearchResponseBase
{
	[JsonIgnore]
	public DateTimeOffset StartTime => DateTimeUtil.UnixEpoch.AddMilliseconds(StartTimeInMillis.Item2.Value); // TODO - Fix use of EpochMillis

	[JsonIgnore]
	public DateTimeOffset ExpirationTime => DateTimeUtil.UnixEpoch.AddMilliseconds(ExpirationTimeInMillis.Item2.Value); // TODO - Fix use of EpochMillis
}

public sealed partial class AsyncSearchSubmitRequestDescriptor<TDocument>
{
	public AsyncSearchSubmitRequestDescriptor<TDocument> MatchAll(Action<MatchAllQueryDescriptor>? selector = null) => Query(q => q.MatchAll(selector));
}

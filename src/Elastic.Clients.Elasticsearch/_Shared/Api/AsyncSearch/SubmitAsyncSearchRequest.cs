// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class SubmitAsyncSearchRequest
{
	// Any request may contain aggregations so we force `typed_keys` in order to successfully
	// // deserialize them.
	internal override void BeforeRequest() => TypedKeys ??= true;
}

public readonly partial struct SubmitAsyncSearchRequestDescriptor
{
	public SubmitAsyncSearchRequestDescriptor MatchAll(Action<MatchAllQueryDescriptor>? action = null) => Query(q => q.MatchAll(action));
}

public readonly partial struct SubmitAsyncSearchRequestDescriptor<TDocument>
{
	public SubmitAsyncSearchRequestDescriptor<TDocument> MatchAll(Action<MatchAllQueryDescriptor>? action = null) => Query(q => q.MatchAll(action));
}

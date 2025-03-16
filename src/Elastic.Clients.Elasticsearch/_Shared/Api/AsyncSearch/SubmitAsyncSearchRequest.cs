// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class SubmitAsyncSearchRequest
{
	// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
	internal override void BeforeRequest() => TypedKeys = true;
}

public sealed partial class SubmitAsyncSearchRequestDescriptor
{
	public SubmitAsyncSearchRequestDescriptor MatchAll(Action<MatchAllQueryDescriptor>? selector = null) => selector is null ? Query(q => q.MatchAll(new MatchAllQuery())) : Query(q => q.MatchAll(selector));

	internal override void BeforeRequest() => TypedKeys(true);
}

public sealed partial class SubmitAsyncSearchRequestDescriptor<TDocument>
{
	public SubmitAsyncSearchRequestDescriptor<TDocument> MatchAll()
	{
		Query(new MatchAllQuery());
		return Self;
	}

	internal override void BeforeRequest() => TypedKeys(true);
}

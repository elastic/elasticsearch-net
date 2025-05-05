// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class GetAsyncSearchRequest
{
	// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
	internal override void BeforeRequest() => TypedKeys = true;
}

public sealed partial class GetAsyncSearchRequestDescriptor<TDocument>
{
	// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
	internal override void BeforeRequest() => TypedKeys(true);
}

public sealed partial class GetAsyncSearchRequestDescriptor
{
	// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
	internal override void BeforeRequest() => TypedKeys(true);
}

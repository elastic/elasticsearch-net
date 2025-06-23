// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Security;

public partial class QueryApiKeysRequest
{
	// Any request may contain aggregations so we force `typed_keys` in order to successfully
	// deserialize them.
	internal override void BeforeRequest()
	{
		if (Aggregations is { Count: > 0 })
		{
			TypedKeys ??= true;
		}
	}
}

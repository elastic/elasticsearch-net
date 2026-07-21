// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Rollup;

public partial class RollupSearchRequest
{
	// Any request may contain aggregations so we force `typed_keys` in order to successfully
	// deserialize them.
	internal override void BeforeRequest() => TypedKeys ??= true;
}

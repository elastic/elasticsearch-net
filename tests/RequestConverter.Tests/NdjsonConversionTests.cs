// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

using Xunit;

namespace RequestConverter.Tests;

public class NdjsonConversionTests
{
	[Fact]
	public void Bulk_ndjson_body_converts_to_operations_collection()
	{
		var body = "{\"index\":{\"_id\":\"1\"}}\n{\"field1\":\"value1\"}\n{\"delete\":{\"_id\":\"2\"}}\n";

		var result = global::RequestConverter.RequestConverter.Convert(
			global::RequestConverter.RequestConverter.DefaultSerializer,
			"bulk",
			pathParameters: new Dictionary<string, string> { ["index"] = "my-index" },
			queryParameters: null,
			body: body);

		Assert.Contains("BulkIndexOperation", result.Code);
		Assert.Contains("BulkDeleteOperation", result.Code);
		Assert.Contains("\"field1\"", result.Code);
	}

	[Fact]
	public void Msearch_ndjson_body_converts_and_reports_the_item_namespace()
	{
		var body = "{\"index\":\"test\"}\n{\"query\":{\"match_all\":{}}}\n";

		var result = global::RequestConverter.RequestConverter.Convert(
			global::RequestConverter.RequestConverter.DefaultSerializer,
			"msearch",
			pathParameters: null,
			queryParameters: null,
			body: body);

		Assert.Contains("SearchRequestItem", result.Code);
		// Fails before Task 2: the hand-written formatter bypasses WriteTypeRef, so the item's namespace
		// is never reported and a Simplified-style caller gets no using directive for it.
		Assert.Contains("Elastic.Clients.Elasticsearch.Core.MSearch", result.Namespaces);
	}
}

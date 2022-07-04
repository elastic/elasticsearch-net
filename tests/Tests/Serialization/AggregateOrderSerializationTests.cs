// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Xunit;
using VerifyXunit;
using Elastic.Clients.Elasticsearch.Aggregations;
using System.Collections.Generic;

namespace Tests.Serialization;

[UsesVerify]
[SystemTextJsonOnly]
public class AggregateOrderSerializationTests : SerializerTestBase
{
	[U]
	public async Task SerializesWithSingleItem()
	{
		var aggregateOrder = AggregateOrder.CountAscending;

		var json = await SerializeAndGetJsonStringAsync(aggregateOrder);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesWithMultipleItems()
	{
		var aggregateOrder = new AggregateOrder
		{
			new KeyValuePair<Field, SortOrder>("field-1", SortOrder.Asc),
			new KeyValuePair<Field, SortOrder>("field-2", SortOrder.Desc)
		};

		var json = await SerializeAndGetJsonStringAsync(aggregateOrder);

		await Verifier.VerifyJson(json);
	}

	[U]
	public void DeserializesWithSingleItem()
	{
		var json = @"{""_count"":""asc""}";

		var result = DeserializeJsonString<AggregateOrder>(json);

		result.Should().HaveCount(1);
		result[0].Key.Should().Be("_count");
		result[0].Value.Should().Be(SortOrder.Asc);
	}

	[U]
	public void DeserializesWithMultipleItems()
	{
		var json = @"[{""field-1"":""asc""},{""field-2"":""desc""}]";

		var result = DeserializeJsonString<AggregateOrder>(json);

		result.Should().HaveCount(2);
		result[0].Key.Should().Be("field-1");
		result[0].Value.Should().Be(SortOrder.Asc);
		result[1].Key.Should().Be("field-2");
		result[1].Value.Should().Be(SortOrder.Desc);
	}
}

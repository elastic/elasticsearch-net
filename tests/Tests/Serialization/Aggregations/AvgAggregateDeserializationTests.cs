// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class AvgAggregateDeserializationTests : SourceSerializerTestBase
{
	[U]
	public void CanDeserialize_AvgAggregate()
	{
		var json = @"{""aggregations"":{""avg#my-agg-name"":{""value"":75}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var agg = search.Aggregations.Avg("my-agg-name");
		agg.Value.HasValue.Should().BeTrue();
		agg.Value.Should().Be(75);
	}

	[U]
	public void CanDeserialize_AvgAggregate_WithNullValue()
	{
		var json = @"{""aggregations"":{""avg#my-agg-name"":{""value"":null}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var agg = search.Aggregations.Avg("my-agg-name");
		agg.Value.HasValue.Should().BeFalse();
	}
}

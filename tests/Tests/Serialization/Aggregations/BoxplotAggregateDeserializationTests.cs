// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class BoxplotAggregateDeserializationTests : SerializerTestBase
{
	[U]
	public void CanDeserializeAggregate()
	{
		var json = @"{""aggregations"":{""boxplot#my-agg-name"":{""min"":1,""max"":990,""q1"":165,""q2"":445,""q3"":725,""lower"":5,""upper"":992}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var agg = search.Aggregations.BoxPlot("my-agg-name");
		agg.Min.Should().Be(1);
		agg.Max.Should().Be(990);
		agg.Q1.Should().Be(165);
		agg.Q2.Should().Be(445);
		agg.Q3.Should().Be(725);
		agg.Lower.Should().Be(5);
		agg.Upper.Should().Be(992);
	}
}

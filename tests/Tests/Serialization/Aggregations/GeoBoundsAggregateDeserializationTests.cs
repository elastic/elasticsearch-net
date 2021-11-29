// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class GeoBoundsAggregateDeserializationTests : SourceSerializerTestBase
{
	[U]
	public void CanDeserializeAggregate()
	{
		var json = @"{""aggregations"":{""geo_bounds#my-agg-name"":{""bounds"":{""top_left"":{""lat"":48.86111099738628,""lon"":2.3269999679178},""bottom_right"":{""lat"":48.85999997612089,""lon"":2.3363889567553997}}}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var agg = search.Aggregations.GeoBounds("my-agg-name");
		// TODO
	}
}

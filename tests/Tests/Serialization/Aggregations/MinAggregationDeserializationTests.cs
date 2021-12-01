// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Aggregations;

namespace Tests.Serialization;

public class MinAggregationDeserializationTests : SourceSerializerTestBase
{
	[U]
	public void CanDeserialize()
	{
		var json = @"{""min"":{""field"":""lastActivity""},""meta"":{""meta_1"":""value_1"",""meta_2"":2,""meta_3"":{""meta_3"":""value_3""}}}";

		var stream = WrapInStream(json);

		var aggregationContainer = _requestResponseSerializer.Deserialize<AggregationContainer>(stream);
				
		aggregationContainer.Meta.Should().HaveCount(3);

		var aggregation = aggregationContainer.Variant.Should().BeOfType<MinAggregation>().Subject;

		aggregation.Field.Name.Should().Be("lastActivity");
	}
}

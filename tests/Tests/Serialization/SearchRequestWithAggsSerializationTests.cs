// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Domain;

namespace Tests.Serialization;

public class SearchRequestDewscriptorWithAggregationsSerializationTests : SerializerTestBase
{
	private const string SearchJson = @"{""aggregations"":{""my-terms-agg"":{""terms"":{""field"":""name""}}}}";

	[U]
	public void CanSerializeSearchRequestDescriptor_WithSimpleTermsAggregation()
	{
		var descriptor = new SearchRequestDescriptor<Project>(c => c.Aggregations(a => a.Terms("my-terms-agg", t => t.Field("name"))));
		var json = SerializeAndGetJsonString(descriptor);
		json.Should().Be(SearchJson);
	}
}

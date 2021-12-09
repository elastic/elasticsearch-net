using System.Collections.Generic;
using Tests.Domain;

namespace Tests.Serialization;

public class SearchRequestWithAggsSerializationTests : SourceSerializerTestBase
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

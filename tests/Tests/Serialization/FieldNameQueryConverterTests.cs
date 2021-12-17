using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;

namespace Tests.Serialization;

public class QueryContainerWithFieldNameQuerySerializationTests : SerializerTestBase
{
	private const string BasicMatchQueryJson = @"{""match"":{""name"":{""query"":""NEST""}}}";

	[U]
	public void CanSerializeQueryContainerDescriptor_WithSimpleMatchQuery()
	{
		var descriptor = new QueryContainerDescriptor<Project>(c => c.Match(m => m.Field("name").Query("NEST")));
		var json = SerializeAndGetJsonString(descriptor);
		json.Should().Be(BasicMatchQueryJson);
	}

	// TODO - Object initializer test

	[U]
	public void CanDeserializeQueryContainer_WithSimpleMatchQuery()
	{
		var stream = WrapInStream(BasicMatchQueryJson);

		var queryContainer = _requestResponseSerializer.Deserialize<QueryContainer>(stream);
		var matchQuery = queryContainer.Variant.Should().BeOfType<MatchQuery>().Subject;
		matchQuery.Field.Should().Be("name");
		matchQuery.Query.Should().Be("NEST");
	}
}

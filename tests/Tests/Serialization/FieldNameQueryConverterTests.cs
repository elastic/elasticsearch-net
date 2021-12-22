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


public class FieldValuesConverterTests : SerializerTestBase
{
	private const string BasicMatchQueryJson = @"{""user.id"":[""kimchy""],""@timestamp"":[""4098435132000""],""http.response.bytes"":[1070000],""http.response.status_code"":[200]}";

	[U]
	public void CanDeserializeQueryContainer_WithSimpleMatchQuery()
	{
		var stream = WrapInStream(BasicMatchQueryJson);

		var fieldValues = _requestResponseSerializer.Deserialize<FieldValues>(stream);
		fieldValues.Value<string>("user.id").Should().Be("kimchy");
		fieldValues.Values<string>("@timestamp").Should().HaveCount(1);
		fieldValues.Values<string>("@timestamp")[0].Should().Be("4098435132000");
	}
}

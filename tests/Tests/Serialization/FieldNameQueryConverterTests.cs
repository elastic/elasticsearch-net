using System.IO;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.Serialization;

public class QueryContainerWithFieldNameQuerySerializationTests : SourceSerializerTestBase
{
	[U]
	public void CanSerializeQueryContainerWithMatchQuery()
	{
	}

	[U]
	public void CanDeserializeQueryContainerWithMatchQuery()
	{
		var stream = WrapInStream(@"{""match"":{""name"":{""query"":""NEST""}}}");

		var queryContainer = _sourceSerializer.Deserialize<QueryContainer>(stream);
		var matchQuery = queryContainer.Variant.Should().BeOfType<MatchQuery>().Subject;
		matchQuery.Field.Should().Be("name");
		matchQuery.Query.Should().Be("NEST");
	}
}

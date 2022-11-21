// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Queries;

[UsesVerify]
public class Query_WithFieldNameQuery_SerializationTests : SerializerTestBase
{
	private const string BasicMatchQueryJson = @"{""match"":{""name"":{""query"":""NEST""}}}";

	[U]
	public async Task CanSerialize_QueryDescriptor_WithSimpleMatchQuery()
	{
		var descriptor = new QueryDescriptor<Project>(c => c.Match(m => m.Field("name").Query("NEST")));
		var json = SerializeAndGetJsonString(descriptor);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task CanDeserialize_Query_WithSimpleMatchQuery()
	{
		var stream = WrapInStream(BasicMatchQueryJson);

		var queryContainer = _requestResponseSerializer.Deserialize<Query>(stream);
		var matchQuery = queryContainer.Variant.Should().BeOfType<MatchQuery>().Subject;
		await Verifier.Verify(matchQuery);
	}
}

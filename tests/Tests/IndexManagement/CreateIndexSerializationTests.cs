// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Threading.Tasks;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.IndexManagement;

[UsesVerify]
public class CreateIndexSerializationTests : SerializerTestBase
{
	[U]
	public async Task CreateIndexWithAliases_SerializesCorrectly()
	{
		var alias1 = new Alias();
		var alias2 = new Alias { Filter = QueryContainer.Term(new TermQuery("username") { Value = "stevegordon" }), Routing = "shard-1" };

		var descriptor = new CreateRequestDescriptor("test")
			.Aliases(aliases => aliases
				.Add("alias_1", alias1)
				.Add("alias_2", alias2));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		var createRequest = new CreateRequest("test")
		{
			Aliases = new()
			{
				{  "alias_1", alias1 },
				{  "alias_2", alias2 }
			}
		};

		var objectJson = await SerializeAndGetJsonStringAsync(createRequest);
		objectJson.Should().Be(json);
	}
}

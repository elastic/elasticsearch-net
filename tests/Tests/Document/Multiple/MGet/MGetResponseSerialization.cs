// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Tests.Serialization;
using Xunit;

namespace Tests.Document.Multiple.MGet;

public class MGetResponseSerialization : SerializerTestBase
{
	private const string ResponseJson = @"{
    ""docs"": [
		{
			""_index"": ""foos"",
			""_id"": ""001"",
			""_version"": 5,
			""_seq_no"": 8,
			""_primary_term"": 1,
			""found"": true,
			""_source"": {
				""id"": ""001"",
				""name"": ""FooA""
			}
		}, 
		{
			""_index"": ""foos"",
			""_id"": ""002"",
			""_version"": 5,
			""_seq_no"": 9,
			""_primary_term"": 1,
			""found"": true,
			""_source"": {
				""id"": ""002"",
				""name"": ""FooB""
			}
		}, 
		{
			""_index"": ""foos"",
			""_id"": ""nonexistant"",
			""found"": false
		}
	]
}";

	[U]
	public void MultiGetResponse_DeserializesCorrectly_WhenIdsAreNotFound()
	{
		var response = DeserializeJsonString<MultiGetResponse<Foo>>(ResponseJson);

		response.Docs.Should().HaveCount(3);
		response.Docs.ElementAt(0).Match(r => r.Found.Should().BeTrue(), _ => Assert.Fail("Union item should not have matched."));
		response.Docs.ElementAt(1).Match(r => r.Found.Should().BeTrue(), _ => Assert.Fail("Union item should not have matched."));
		response.Docs.ElementAt(2).Match(r => r.Found.Should().BeFalse(), _ => Assert.Fail("Union item should not have matched."));
	}
}

internal class Foo
{
	public string Id { get; set; }
	public string Name { get; set; }
}

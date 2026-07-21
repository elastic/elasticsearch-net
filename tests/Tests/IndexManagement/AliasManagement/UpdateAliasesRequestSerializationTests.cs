// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.IndexManagement.AliasManagement;

[UsesVerify]
public class UpdateAliasesRequestSerializationTests : SerializerTestBase
{
	[U]
	public async Task UpdateAliasesRequest_SerializesCorrectly()
	{
		var descriptor = new UpdateAliasesRequestDescriptor()
			.Actions(
				a => a.Add(a => a.Index("index-02").Alias("test-alias")),
				a => a.Remove(a => a.Index("index-01").Alias("test-alias")));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		var createRequest = new UpdateAliasesRequest()
		{
			Actions = new Action[]
			{
				Action.Add(new () { Index = "index-02", Alias = "test-alias" }),
				Action.Remove(new () { Index = "index-01", Alias = "test-alias" })
			}
		};

		var objectJson = await SerializeAndGetJsonStringAsync(createRequest);
		objectJson.Should().Be(json);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.Xunit;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
[SystemTextJsonOnly]
public class SortCombinationsSerializationTests : SerializerTestBase
{
	[U]
	public async Task SerializesFieldSort_WithStringField()
	{
		var dictionary = new SortCombinations(SortOptions.Field("string-field", new FieldSort { Order = SortOrder.Asc }));
		var json = await SerializeAndGetJsonStringAsync(dictionary);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesFieldSort_WithInferredField()
	{
		var dictionary = new SortCombinations(SortOptions.Field(Infer.Field<Project>(p => p.Name), new FieldSort { Order = SortOrder.Asc }));
		var json = await SerializeAndGetJsonStringAsync(dictionary);
		await Verifier.VerifyJson(json);
	}

	[U]
	public void DeserializesFieldSort_WithStringField()
	{
		var json = @"{""name"":{""order"":""asc""}}";
		var result = DeserializeJsonString<SortCombinations>(json);

		var sortOptions = result.Item2;

		sortOptions.Should().NotBeNull();
		var fieldSort = sortOptions.Variant.Should().BeOfType<FieldSort>().Subject;

		fieldSort.Order.Should().Be(SortOrder.Asc);
	}
}

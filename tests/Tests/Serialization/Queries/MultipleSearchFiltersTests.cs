// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class MultipleSearchFiltersTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize_AvgAggregation_Descriptor()
	{
		var search = new SearchRequestDescriptor<Person>(search => search
			.Query(q => q
				.Bool(b => b
					.Filter(
						f => f.Term(t => t.Field(f => f.Age).Value(37)),
						f => f.Term(t => t.Field(f => f.Name).Value("Steve"))
					))));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	private class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
}

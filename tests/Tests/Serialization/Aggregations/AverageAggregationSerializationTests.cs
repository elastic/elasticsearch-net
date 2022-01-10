// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class AverageAggregationSerializationTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize_AvgAggregation_Descriptor()
	{
		var search = new SearchRequestDescriptor<Person>(search => search
			.Aggregations(aggs => aggs
				.Average("test_average", avg => avg
					.Script(s => s.Source("emit(Math.min(100, doc['grade'].value * 1.2))")))));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	private class Person { public string Name { get; set; } }
}

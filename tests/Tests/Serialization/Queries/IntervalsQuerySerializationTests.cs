// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class IntervalsQuerySerializationTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize()
	{
		var search = new SearchRequestDescriptor<Project>(search => search
			.Query(q => q
				.Intervals(i => i
					.Field(f => f.Name)
					.Boost(2.0f)
					.QueryName("testing-intervals")
					.Match(m => m
						.Query("Steve")
						.MaxGaps(0)
						.Ordered()))));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}
}

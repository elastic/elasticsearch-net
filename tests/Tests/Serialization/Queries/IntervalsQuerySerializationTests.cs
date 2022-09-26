// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class IntervalsQuerySerializationTests : SerializerTestBase
{
	[U]
	public async Task Descriptor_CanSerialize()
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

	[U]
	public async Task Object_CanSerialize()
	{
		var intervalsMatch = new IntervalsMatch
		{
			Query = "Steve",
			MaxGaps = 0,
			Ordered = true
		};

		var query = IntervalsQuery.Match(Infer.Field<Project>(f => f.Name), intervalsMatch);
		query.QueryName = "testing-intervals";
		query.Boost = 2.0f;

		var search = new SearchRequestDescriptor<Project>(search => search
			.Query(q => q.Intervals(query)));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class IntervalsQuerySerializationTests : SerializerTestBase
{
	private readonly IntervalsMatch _intervalsMatch = new()
	{
		Query = "Steve",
		MaxGaps = 0,
		Ordered = true
	};

	[U]
	public async Task IntervalsQueryDescriptor_CanSerialize()
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
	public async Task IntervalsQueryDescriptor_CanSerialize_WithObjectVariant()
	{
		var search = new SearchRequestDescriptor<Project>(search => search
			.Query(q => q
				.Intervals(i => i
					.Field(f => f.Name)
					.Boost(2.0f)
					.QueryName("testing-intervals")
					.Match(_intervalsMatch))));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task IntervalsQuery_CanSerialize()
	{
		var query = IntervalsQuery.Match(Infer.Field<Project>(f => f.Name), _intervalsMatch);
		query.QueryName = "testing-intervals";
		query.Boost = 2.0f;

		var search = new SearchRequestDescriptor<Project>(search => search
			.Query(q => q.Intervals(query)));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	public async Task IntervalsQuery_CanDeserialize()
	{
		var stream = WrapInStream("{\"\"query\"\":{\"\"intervals\"\":{\"\"name\"\":{\"\"boost\"\":2,\"\"match\"\":" +
			"{\"\"max_gaps\"\":0,\"\"ordered\"\":true,\"\"query\"\":\"\"Steve\"\"},\"\"_name\"\":\"\"testing-intervals\"\"}}}}");

		var queryContainer = _requestResponseSerializer.Deserialize<QueryContainer>(stream);
		var intervalsQuery = queryContainer.Variant.Should().BeOfType<IntervalsQuery>().Subject;
		await Verifier.Verify(intervalsQuery);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Queries;

[UsesVerify]
public class RangeQuerySerializationTests : SerializerTestBase
{
	private const string Field = "my-field";
	private const string QueryName = "named_query";

	private static readonly DateRangeQuery DateRangeQueryWithoutFormat = new(Field)
	{
		QueryName = QueryName,
		Boost = 1.1f,
		Gte = DateMath.Now.Subtract("1y").RoundTo(DateMathTimeUnit.Month),
		Lt = DateMath.Now
	};

	private static readonly NumberRangeQuery NumberRangeQuery = new(Field)
	{
		QueryName = QueryName,
		Boost = 1.1f,
		Gte = 100,
		Lt = 1000
	};

	private static void VerifyQuery(DateRangeQuery dateRangeQuery)
	{
		dateRangeQuery.Should().NotBeNull();
		dateRangeQuery.QueryName.Should().Be(QueryName);
		dateRangeQuery.Field.Should().Be(Field);
		dateRangeQuery.Boost.Should().Be(1.1f);
		dateRangeQuery.Gte.ToString().Should().Be("now-1y/M");
		dateRangeQuery.Lt.ToString().Should().Be("now");
	}

	private static void VerifyQuery(NumberRangeQuery dateRangeQuery)
	{
		dateRangeQuery.Should().NotBeNull();
		dateRangeQuery.QueryName.Should().Be(QueryName);
		dateRangeQuery.Field.Should().Be(Field);
		dateRangeQuery.Boost.Should().Be(1.1f);
		dateRangeQuery.Gte.Should().Be(100);
		dateRangeQuery.Lt.Should().Be(1000);
	}

	[U]
	public async Task DateRangeQuery_SerializesCorrectly()
	{
		var query = DateRangeQueryWithoutFormat;

		var json = await SerializeAndVerifyJsonAsync(query);

		var dateRangeQuery = DeserializeJsonString<DateRangeQuery>(json);

		VerifyQuery(dateRangeQuery);

		var rangeQuery = DeserializeJsonString<RangeQuery>(json);

		dateRangeQuery = rangeQuery as DateRangeQuery;

		VerifyQuery(dateRangeQuery);
	}
	
	[U]
	public async Task DateRangeQuery_WithFormat_SerializesCorrectly()
	{
		const string dateFormat = @"dd/MM/yyyy||yyyy";

		var query = DateRangeQueryWithoutFormat;
		query.Format = dateFormat;

		var json = await SerializeAndVerifyJsonAsync(query);

		var dateRangeQuery = DeserializeJsonString<DateRangeQuery>(json);

		VerifyQuery(dateRangeQuery);
		dateRangeQuery.Format.Should().Be(dateFormat);

		var rangeQuery = DeserializeJsonString<RangeQuery>(json);

		dateRangeQuery = rangeQuery as DateRangeQuery;

		VerifyQuery(dateRangeQuery);
		dateRangeQuery.Format.Should().Be(dateFormat);
	}

	[U]
	public async Task NumberRangeQuery_SerializesCorrectly()
	{
		var query = NumberRangeQuery;

		var json = await SerializeAndVerifyJsonAsync(query);

		var numberRangeQuery = DeserializeJsonString<NumberRangeQuery>(json);

		VerifyQuery(numberRangeQuery);

		var rangeQuery = DeserializeJsonString<RangeQuery>(json);

		numberRangeQuery = rangeQuery as NumberRangeQuery;

		VerifyQuery(numberRangeQuery);
	}

	[U]
	public async Task DateRangeQuery_QueryContainer_SerializesCorrectly()
	{
		var query = Query.Range(DateRangeQueryWithoutFormat);

		var json = await SerializeAndVerifyJsonAsync(query);

		var deserializedQuery = DeserializeJsonString<Query>(json);

		deserializedQuery.TryGet<DateRangeQuery>(out var dateRangeQuery).Should().BeTrue();

		VerifyQuery(dateRangeQuery);
	}

	[U]
	public async Task Fluent_DateRangeQuery_QueryContainer_SerializesCorrectly()
	{
		var query = new QueryDescriptor<Project>(q => q
			.Range(r => r
				.DateRange(d => d
					.Field(Field)
					.QueryName(QueryName)
					.Boost(1.1f)
					.Gte(DateMath.Now.Subtract("1y").RoundTo(DateMathTimeUnit.Month))
					.Lt(DateMath.Now))));

		var json = await SerializeAndVerifyJsonAsync(query);

		var deserializedQuery = DeserializeJsonString<Query>(json);

		deserializedQuery.TryGet<DateRangeQuery>(out var dateRangeQuery).Should().BeTrue();

		VerifyQuery(dateRangeQuery);
	}

	[U]
	public async Task NumberRangeQuery_QueryContainer_SerializesCorrectly()
	{
		var query = Query.Range(NumberRangeQuery);

		var json = await SerializeAndVerifyJsonAsync(query);

		var deserializedQuery = DeserializeJsonString<Query>(json);

		deserializedQuery.TryGet<NumberRangeQuery>(out var numberRangeQuery).Should().BeTrue();

		VerifyQuery(numberRangeQuery);
	}

	[U]
	public async Task Fluent_NumberRangeQuery_QueryContainer_SerializesCorrectly()
	{
		var query = new QueryDescriptor<Project>(q => q
			.Range(r => r
				.NumberRange(d => d
					.Field(Field)
					.QueryName(QueryName)
					.Boost(1.1f)
					.Gte(100)
					.Lt(1000))));

		var json = await SerializeAndVerifyJsonAsync(query);

		var deserializedQuery = DeserializeJsonString<Query>(json);

		deserializedQuery.TryGet<NumberRangeQuery>(out var numberRangeQuery).Should().BeTrue();

		VerifyQuery(numberRangeQuery);
	}
}

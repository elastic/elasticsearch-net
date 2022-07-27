// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class SearchSerializationTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize_SearchRequest_WithAggregations()
	{
		var searchRequest = new SearchRequest { Aggregations = new TermsAggregation("my-terms-agg") { Field = Infer.Field<Project>(f => f.Name) } };

		var roundTripped = await RoundTripAndVerifyJsonAsync(searchRequest);

		roundTripped.Aggregations.Should().NotBeNull();
		roundTripped.Aggregations["my-terms-agg"].Should().NotBeNull();
	}

	[U]
	public void CanDeserialize_SearchRequest_UsingAggs()
	{
		var json = @"{""aggs"":{""my-terms-agg"":{""terms"":{""field"":""name""}}}}";

		var searchRequest = DeserializeJsonString<SearchRequest>(json);

		searchRequest.Aggregations.Should().NotBeNull();
		searchRequest.Aggregations["my-terms-agg"].Should().NotBeNull();
	}

	[U]
	public void CanDeserialize_SearchRequest_UsingAggregations()
	{
		var json = @"{""aggregations"":{""my-terms-agg"":{""terms"":{""field"":""name""}}}}";

		var searchRequest = DeserializeJsonString<SearchRequest>(json);

		searchRequest.Aggregations.Should().NotBeNull();
		searchRequest.Aggregations["my-terms-agg"].Should().NotBeNull();
	}
}

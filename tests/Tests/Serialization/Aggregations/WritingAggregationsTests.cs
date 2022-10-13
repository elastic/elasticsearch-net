// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class WritingAggregationsTests : SerializerTestBase
{
	[U]
	public async Task CanSerializeAggregationsWrittenInVariousWays_WhichIncludeMultipleSubAggregations()
	{
		// TODO - Test other cases from https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/writing-aggregations.html#writing-aggregations

		// ** First test that the descriptor produces the expected JSON.

		var descriptor = new AggregationContainerDescriptor(aggs => aggs
			.Children<CommitActivity>("name_of_child_agg", child => child
				.Type("commits")
				.Aggregations(childAggs => childAggs
					.Avg("average_per_child", avg => avg.Field(p => p.ConfidenceFactor))
					.Max("max_per_child", max => max.Field(p => p.ConfidenceFactor))
					.Min("min_per_child", min => min.Field(p => p.ConfidenceFactor))
		)));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		// ** Then test that the object initializer dictionary approach produces the expected (and same) JSON.

		var aggs = new AggregationDictionary
		{
			{
				new ChildrenAggregation("name_of_child_agg")
				{
					Type = "commits",
					Aggregations = new AggregationDictionary
					{
						{new AverageAggregation("average_per_child", "confidenceFactor")},
						{new MaxAggregation("max_per_child", "confidenceFactor")},
						{new MinAggregation("min_per_child", "confidenceFactor")},
					}
				}
			}
		};

		var objectInitializerJson = await SerializeAndGetJsonStringAsync(aggs);

		json.Should().Be(objectInitializerJson);

		// ** Next test the terser object initializer syntax produces the expected (and same) JSON.

		AggregationDictionary terseAggs = new ChildrenAggregation("name_of_child_agg") // NOTE: Must assign to AggregationDictionary.
		{
			Type = "commits",
			Aggregations =
				new AverageAggregation("average_per_child", Infer.Field<CommitActivity>(p => p.ConfidenceFactor))
				&& new MaxAggregation("max_per_child", Infer.Field<CommitActivity>(p => p.ConfidenceFactor))
				&& new MinAggregation("min_per_child", Infer.Field<CommitActivity>(p => p.ConfidenceFactor))
		};

		var terseAggsJson = await SerializeAndGetJsonStringAsync(terseAggs);

		json.Should().Be(terseAggsJson);

		// ** Test we can deserialise the JSON as an AggregationDictionary

		// TODO
		// var result = DeserializeJsonString<AggregationDictionary>(json);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Serialization;

public class ExtendedStatsAggregateDeserializationTests : SerializerTestBase
{
	[U]
	public void CanDeserializeAggregate()
	{
		var json = @"{""aggregations"":{""extended_stats#my-agg-name"":{""count"":2,""min"":50,""max"":100,""avg"":75,""sum"":150,""sum_of_squares"":12500,
""variance"":625,""variance_population"":625,""variance_sampling"":1250,""std_deviation"":25,""std_deviation_population"":25,""std_deviation_sampling"":35.35533905932738,
""std_deviation_bounds"":{""upper"":125,""lower"":25,""upper_population"":125,""lower_population"":25,""upper_sampling"":145.71067811865476,""lower_sampling"":4.289321881345245}}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var agg = search.Aggregations.ExtendedStats("my-agg-name");
		agg.Count.Should().Be(2);
		agg.Min.Should().Be(50);
		agg.Max.Should().Be(100);
		agg.Avg.Should().Be(75);
		agg.Sum.Should().Be(150);
		agg.SumOfSquares.Should().Be(12500);
		agg.Variance.Should().Be(625);
		agg.VariancePopulation.Should().Be(625);
		agg.VarianceSampling.Should().Be(1250);
		agg.StdDeviation.Should().Be(25);
		// MISSING std_deviation_population
		// MISSING std_deviation_sampling
		// MISSING std_deviation_bounds
	}
}

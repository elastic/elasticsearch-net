// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
namespace Tests.Serialization;

public class BucketSubAggregationSerializationTests : SerializerTestBase
{
	[U]
	public void DeserializeSubAggsForDateHistorgram()
	{
		const string json = @"{""took"":3,""timed_out"":false,""_shards"":{""total"":1,""successful"":1,""skipped"":0,""failed"":0},
""hits"":{""total"":{""value"":1259,""relation"":""eq""},""max_score"":null,""hits"":[]},""aggregations"":{""date_histogram#by-month"":{""buckets"":[{
""key_as_string"":""2018-02-01T00:00:00.000Z"",""key"":1517443200000,""doc_count"":5,""sum#trade-volumes"":{""value"":255233256},""sum#trade-volumes-two"":{""value"":255233256}}]}}}";

		var searchResponse = DeserializeJsonString<SearchResponse<object>>(json);

		var dateHistogramAgg = searchResponse.Aggregations.GetDateHistogram("by-month");

		dateHistogramAgg.Should().NotBeNull();

		var bucketsCollection = dateHistogramAgg.Buckets.Item2;
		bucketsCollection.Should().HaveCount(1);

		var dateBucket = bucketsCollection.First();

		dateBucket.Key.Should().Be(1517443200000);
		dateBucket.KeyAsString.Should().Be("2018-02-01T00:00:00.000Z");

		var firstSum = dateBucket.GetSum("trade-volumes");

		firstSum.Should().NotBeNull();
		firstSum.Value.Should().Be(255233256);

		dateBucket.GetSum("trade-volumes-two").Should().NotBeNull();
	}
}

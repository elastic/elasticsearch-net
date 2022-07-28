// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Tests.Serialization;

public class TermsAggregateDeserializationTests : SerializerTestBase
{
	[U]
	public void CanDeserialize_BasicStringTermsAggregate()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,
""buckets"":[{""key"":""electronic"",""doc_count"":5,""doc_count_error"":2},{""key"":""rock"",""doc_count"":3}]}}}";

		var stream = WrapInStream(json);
	
		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.GetStringTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);
		var bucketCollection = termsAgg.Buckets.Item2;
		bucketCollection.Should().HaveCount(2);

		var firstBucket = bucketCollection.First();
		firstBucket.Key.Should().Be("electronic");
		firstBucket.DocCount.Should().Be(5);
		firstBucket.DocCountError.Should().Be(2);

		var secondBucket = bucketCollection.Last();
		secondBucket.Key.Should().Be("rock");
		secondBucket.DocCount.Should().Be(3);
		secondBucket.DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicLongTermsAggregate()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,
""buckets"":[{""key"":10,""key_as_string"":""10"",""doc_count"":5,""doc_count_error"":2},{""key"":15,""key_as_string"":""15"",""doc_count"":3}]}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.GetLongTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);

		var bucketCollection = termsAgg.Buckets.Item2;
		bucketCollection.Should().HaveCount(2);

		var firstBucket = bucketCollection.First();
		firstBucket.Key.Should().Be(10);
		firstBucket.KeyAsString.Should().Be("10");
		firstBucket.DocCount.Should().Be(5);
		firstBucket.DocCountError.Should().Be(2);

		var secondBucket = bucketCollection.Last();
		secondBucket.Key.Should().Be(15);
		secondBucket.KeyAsString.Should().Be("15");
		secondBucket.DocCount.Should().Be(3);
		secondBucket.DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicDoubleTermsAggregate()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,
""buckets"":[{""key"":10.5,""key_as_string"":""10.5"",""doc_count"":5,""doc_count_error"":2},{""key"":15.5,""key_as_string"":""15.5"",""doc_count"":3}]}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.GetDoubleTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);

		var bucketCollection = termsAgg.Buckets.Item2;
		bucketCollection.Should().HaveCount(2);

		var firstBucket = bucketCollection.First();
		firstBucket.Key.Should().Be(10.5);
		firstBucket.KeyAsString.Should().Be("10.5");
		firstBucket.DocCount.Should().Be(5);
		firstBucket.DocCountError.Should().Be(2);

		var secondBucket = bucketCollection.Last();
		secondBucket.Key.Should().Be(15.5);
		secondBucket.KeyAsString.Should().Be("15.5");
		secondBucket.DocCount.Should().Be(3);
		secondBucket.DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_StringBased_MultiTermsAggregate()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,
""buckets"":[{""key"":[""key-1"",""key-2""],""key_as_string"":""key-1|key-2"",""doc_count"":5,""doc_count_error_upper_bound"":2},
{""key"":[""key-3"",""key-4""],""key_as_string"":""key-3|key-4"",""doc_count"":3}]}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.GetMultiTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);

		var bucketCollection = termsAgg.Buckets.Item2;

		var firstBucket = bucketCollection.First();
		firstBucket.Key.Should().HaveCount(2);
		firstBucket.Key.First().Should().BeOfType<string>().Subject.Should().Be("key-1");
		firstBucket.Key.Last().Should().BeOfType<string>().Subject.Should().Be("key-2");
		firstBucket.KeyAsString.Should().Be("key-1|key-2");
		firstBucket.DocCount.Should().Be(5);
		firstBucket.DocCountErrorUpperBound.Should().Be(2);

		var secondBucket = bucketCollection.Last();
		secondBucket.Key.Should().HaveCount(2);
		secondBucket.Key.First().Should().BeOfType<string>().Subject.Should().Be("key-3");
		secondBucket.Key.Last().Should().BeOfType<string>().Subject.Should().Be("key-4");
		secondBucket.KeyAsString.Should().Be("key-3|key-4");
		secondBucket.DocCount.Should().Be(3);
		secondBucket.DocCountErrorUpperBound.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicDoubleTerms_AndAccessAsTermsAggregate()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,
""buckets"":[{""key"":10.5,""key_as_string"":""10.5"",""doc_count"":5,""doc_count_error"":2},{""key"":15.5,""key_as_string"":""15.5"",""doc_count"":3}]}}}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.GetDoubleTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);

		var bucketCollection = termsAgg.Buckets.Item2;

		bucketCollection.Should().HaveCount(2);

		var firstBucket = bucketCollection.First();
		firstBucket.Key.Should().Be(10.5);
		firstBucket.KeyAsString.Should().Be("10.5");
		firstBucket.DocCount.Should().Be(5);
		firstBucket.DocCountError.Should().Be(2);

		var secondBucket = bucketCollection.Last();
		secondBucket.Key.Should().Be(15.5);
		secondBucket.KeyAsString.Should().Be("15.5");
		secondBucket.DocCount.Should().Be(3);
		secondBucket.DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicEmptyTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[]}}}");

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var isEmpty = search.Aggregations.IsEmptyTerms("my-agg-name");

		isEmpty.Should().BeTrue();

		if (isEmpty)
		{
			var agg = search.Aggregations.EmptyTerms("my-agg-name");
			agg.DocCountErrorUpperBound.Should().Be(10);
			agg.SumOtherDocCount.Should().Be(200);
			agg.Buckets.Item2.Should().HaveCount(0);
		}
	}

	[U]
	public void CanDeserialize_TermsAggregate_WithSubAggregation()
	{
		var json = @"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":0,""sum_other_doc_count"":0,""buckets"":[{""key"":""foo"",""doc_count"":5,""avg#my-sub-agg-name"":{""value"":75.0}}]}}}";

		var response = DeserializeJsonString<SearchResponse<object>>(json);

		var termsAgg = response.Aggregations.GetTerms("my-agg-name");
		var avgAgg = termsAgg.Buckets.Item2.Single().GetAverage("my-sub-agg-name");
		avgAgg.Value.Should().Be(75.0);
	}
}

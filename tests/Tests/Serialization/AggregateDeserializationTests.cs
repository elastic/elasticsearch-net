// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Tests.Serialization;

public class AggregateDeserializationTests : SourceSerializerTestBase
{
	[U]
	public void CanDeserialize_BasicStringTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[{""key"":""electronic"",""doc_count"":5},{""key"":""rock"",""doc_count"":3}]}}}");
	
		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.StringTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);
		termsAgg.Buckets.Should().HaveCount(2);
		termsAgg.Buckets.First().Key.Should().Be("electronic");
		termsAgg.Buckets.First().DocCount.Should().Be(5);
		termsAgg.Buckets.First().DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicLongTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[{""key"":10,""doc_count"":5},{""key"":15,""doc_count"":3}]}}}");

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.LongTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);
		termsAgg.Buckets.Should().HaveCount(2);
		termsAgg.Buckets.First().Key.Should().Be(10);
		termsAgg.Buckets.First().DocCount.Should().Be(5);
		termsAgg.Buckets.First().DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicDoubleTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[{""key"":10.5,""doc_count"":5},{""key"":15.5,""doc_count"":3}]}}}");

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.DoubleTerms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);
		termsAgg.Buckets.Should().HaveCount(2);
		termsAgg.Buckets.First().Key.Should().Be(10.5);
		termsAgg.Buckets.First().DocCount.Should().Be(5);
		termsAgg.Buckets.First().DocCountError.Should().BeNull();
	}

	[U]
	public void CanDeserialize_BasicDoubleTerms_AsTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[{""key"":10.5,""doc_count"":5},{""key"":15.5,""doc_count"":3}]}}}");

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		var termsAgg = search.Aggregations.Terms("my-agg-name");
		termsAgg.DocCountErrorUpperBound.Should().Be(10);
		termsAgg.SumOtherDocCount.Should().Be(200);
		termsAgg.Buckets.Should().HaveCount(2);
		termsAgg.Buckets.First().Key.Should().Be(10.5);
		termsAgg.Buckets.First().DocCount.Should().Be(5);
		termsAgg.Buckets.First().DocCountError.Should().BeNull();
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
			agg.Buckets.Should().HaveCount(0);
		}
	}

	[U]
	public void CanDeserialize_TryGetStringTermsAggregate()
	{
		var stream = WrapInStream(@"{""aggregations"":{""terms#my-agg-name"":{""doc_count_error_upper_bound"":10,""sum_other_doc_count"":200,""buckets"":[]}}}");

		var search = _requestResponseSerializer.Deserialize<BasicSearchResponse>(stream);

		search.Aggregations.Should().HaveCount(1);

		search.Aggregations.TryGetStringTerms("my-agg-name", out var stringTermsAggregate).Should().BeFalse();
		stringTermsAggregate.Should().BeNull();
	}

	// TODO - Tests for multi-terms

	private class BasicSearchResponse
	{
		public AggregateDictionary Aggregations { get; set; }
	}
}

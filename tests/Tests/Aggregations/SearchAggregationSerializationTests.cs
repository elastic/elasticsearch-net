// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Aggregations
{
	public class SearchAggregationSerializationTests
	{
		private const string ExpectedAggsJson = @"{""aggs"":{""test-agg"":{""terms"":{""field"":""my-field""}}}}";
		private const string AggregationsJson = @"{""aggregations"":{""test-agg"":{""terms"":{""field"":""my-field""}}}}";

		[U] public void SerializesAsExpected()
		{
			var client = new ElasticClient();

			var request = new SearchRequest
			{
				Aggregations = new TermsAggregation("test-agg")
				{
					Field = "my-field"
				}
			};

			var stream = new MemoryStream();
			client.RequestResponseSerializer.Serialize(request, stream);

			stream.Position = 0;
			var reader = new StreamReader(stream);
			var json = reader.ReadToEnd();

			json.Should().Be(ExpectedAggsJson);
		}

		[U]
		public void DeserializesAggsAsExpected()
		{
			var client = new ElasticClient();
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(ExpectedAggsJson));
			var request = client.RequestResponseSerializer.Deserialize<SearchRequest>(stream);
			request.Aggregations.Should().HaveCount(1);
		}

		[U]
		public void DeserializesAggregationsAsExpected()
		{
			var client = new ElasticClient();
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(AggregationsJson));
			var request = client.RequestResponseSerializer.Deserialize<SearchRequest>(stream);
			request.Aggregations.Should().HaveCount(1);
		}
	}

	public class AggregationContainer_AggregationSerializationTests
	{
		private const string ExpectedAggsJson = @"{""aggs"":{""sub-agg"":{""terms"":{""field"":""my-field""}}},""terms"":{""field"":""my-field""}}";
		private const string AggregationsJson = @"{""aggregations"":{""sub-agg"":{""terms"":{""field"":""my-field""}}},""terms"":{""field"":""my-field""}}";

		[U]
		public void SerializesAsExpected()
		{
			var client = new ElasticClient();

			AggregationContainer aggsDictionary = new TermsAggregation("test-agg")
			{
				Field = "my-field",
				Aggregations = new TermsAggregation("sub-agg")
				{
					Field = "my-field"
				}
			};

			var stream = new MemoryStream();
			client.RequestResponseSerializer.Serialize(aggsDictionary, stream);

			stream.Position = 0;
			var reader = new StreamReader(stream);
			var json = reader.ReadToEnd();

			json.Should().Be(ExpectedAggsJson);
		}

		[U]
		public void DeserializesAggsAsExpected()
		{
			var client = new ElasticClient();
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(ExpectedAggsJson));
			var request = client.RequestResponseSerializer.Deserialize<AggregationContainer>(stream);
			request.Aggregations.Should().HaveCount(1);
		}

		[U]
		public void DeserializesAggregationsAsExpected()
		{
			var client = new ElasticClient();
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(AggregationsJson));
			var request = client.RequestResponseSerializer.Deserialize<AggregationContainer>(stream);
			request.Aggregations.Should().HaveCount(1);
		}
	}
}

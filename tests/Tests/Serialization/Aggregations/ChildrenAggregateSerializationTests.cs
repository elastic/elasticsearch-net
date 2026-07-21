// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.Experimental;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class ChildrenAggregateSerializationTests : SerializerTestBase
{
	[U]
	public void DeserialisesCorrectly()
	{
		var json = @"{""aggregations"":{""sterms#top-tags"":{""doc_count_error_upper_bound"":0,""sum_other_doc_count"":0,""buckets"":[{""key"":""file-transfer"",""doc_count"":1,""children#to-answers"":{""doc_count"":2,""sterms#top-names"":{""doc_count_error_upper_bound"":0,""sum_other_doc_count"":0,""buckets"":[{""key"":""Sam"",""doc_count"":1},{""key"":""Troll"",""doc_count"":1}]}}}]}}}";

		var searchResponse = DeserializeJsonString<SearchResponse<object>>(json);

		var topTagsStringTermsAggregate = searchResponse.Aggregations.GetStringTerms("top-tags");
		var firstTopTagsStringTermsBucket = topTagsStringTermsAggregate.Buckets.Single();
		var childrenAggregate = firstTopTagsStringTermsBucket.GetChildren("to-answers");
		var topNamesAggregate = childrenAggregate.GetTerms("top-names");
		var firstTopNameBucket = topNamesAggregate.Buckets.First();
		firstTopNameBucket.Key.Should().Be("Sam");

		// This tests the experiental GetTerms extension method as an easier way to access the same terms.
		var topTagsTermsAggregate = searchResponse.Aggregations.GetTerms("top-tags");
		var firstTopTagsBucket = topTagsTermsAggregate.Buckets.Single();
		childrenAggregate = firstTopTagsBucket.GetChildren("to-answers");
		topNamesAggregate = childrenAggregate.GetTerms("top-names");
		firstTopNameBucket = topNamesAggregate.Buckets.First();
		firstTopNameBucket.Key.Should().Be("Sam");
	}
}

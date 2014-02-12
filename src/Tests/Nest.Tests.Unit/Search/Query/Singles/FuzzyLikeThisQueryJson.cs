using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class FuzzyLikeThisQueryJson
	{
		[Test]
		public void TestFuzzyLikeThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyLikeThis(fz => fz
						.OnFields(f => f.Name)
						.LikeText("elasticsearcc")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ flt: { 
				fields : [""name"" ],
				like_text : ""elasticsearcc"" 
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestFuzzyLikeThisAllQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.FuzzyLikeThis(fz => fz
						.OnFields(f => f.Name)
						.LikeText("elasticsearcc")
						.PrefixLength(3)
						.MaxQueryTerms(25)
						.MinimumSimilarity(3)
						.IgnoreTermFrequency(true)
						.Boost(1.1)
						.Analyzer("my_analyzer")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ flt: { 
				fields : [""name"" ],
				like_text : ""elasticsearcc"",
				ignore_tf: true,
				max_query_terms: 25,
				min_similarity: 3.0,
				prefix_length: 3,
				boost: 1.1,
				analyzer: ""my_analyzer""
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

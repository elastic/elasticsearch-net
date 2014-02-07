using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class MoreLikeThisQueryJson
	{
		[Test]
		public void TestMoreLikeThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MoreLikeThis(fz => fz
						.OnFields(f => f.Name)
						.LikeText("elasticsearcc")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ mlt: { 
				fields : [""name"" ],
				like_text : ""elasticsearcc"" 
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestMoreLikeThisAllQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MoreLikeThis(fz => fz
						.OnFields(f => f.Name)
						.LikeText("elasticsearcc")
						.MaxQueryTerms(25)
						.MinDocumentFrequency(1)
						.MaxDocumentFrequency(2)
						.MinTermFrequency(1)
						.MaxDocumentFrequency(2)
						.MinWordLength(1)
						.MaxWordLength(2)
						.StopWords(new[] { "thou", "shall" })
						.BoostTerms(1.4)
						.TermMatchPercentage(12)
						.Boost(1.1)
						.Analyzer("my_analyzer")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ mlt: { 
				fields : [""name"" ],
				like_text : ""elasticsearcc"",
				percent_terms_to_match: 12.0,
				stop_words: [
					""thou"",
					""shall""
				],
				min_term_freq: 1,
				max_query_terms: 25,
				min_doc_freq: 1,
				max_doc_freq: 2,
				min_word_len: 1,
				max_word_len: 2,
				boost_terms: 1.4,
				boost: 1.1,
				analyzer: ""my_analyzer""
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

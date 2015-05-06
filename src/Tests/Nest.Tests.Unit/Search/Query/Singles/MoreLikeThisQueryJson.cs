using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class MoreLikeThisQueryJson
	{
		public class MoreLikeThisTestDoc
		{
			public string Name { get; set; }
			public string Text { get; set; }
		}

		[Test]
		public void TestMoreLikeThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.MoreLikeThis(fz => fz
						.Name("named_query")
						.OnFields(f => f.Name)
						.LikeText("elasticsearcc")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ mlt: { 
				_name: ""named_query"",
				fields : [""name"" ],
				like_text : ""elasticsearcc"" 
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void MoreLikeThisWithIds()
		{
			var s = new MoreLikeThisQueryDescriptor<ElasticsearchProject>()
				.Ids(new long[] { 1, 2, 3, 4 })
				.OnFields(p => p.Name);
			var json = TestElasticClient.Serialize(s);

			var expected = @"{
				fields: [ ""name""],
				ids: [ ""1"", ""2"", ""3"", ""4""],
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void MoreLikeThisWithDocuments()
		{
			var s = new MoreLikeThisQueryDescriptor<ElasticsearchProject>()
				.OnFields(p => p.Name)
				.Documents(d => d
					.Get(1, g => g.Fields(p => p.Product.Name).Routing("routing_value"))
					.Get<Person>("some-string-id", g => g.Routing("routing_value").Type("people").Index("different_index"))
					.Get<MoreLikeThisTestDoc>(g => g
						.Document(new MoreLikeThisTestDoc { Name = "elasticsearch", Text = "foo" })
						.PerFieldAnalyzer(pfa => pfa
							.Add(p => p.Name, "keyword")
						)
					)
					.Document<MoreLikeThisTestDoc>(new MoreLikeThisTestDoc { Name = "nest" })
					.Document<MoreLikeThisTestDoc>(new MoreLikeThisTestDoc { Name = "foo" }, "myindex", "mytype")
				);
			var json = TestElasticClient.Serialize(s);

			var expected = @"{
				fields: [ ""name""],
				docs: [
				{
				  _index: ""nest_test_data"",
				  _type: ""elasticsearchprojects"",
				  _id: ""1"",
				  fields: [
				    ""product.name""
				  ],
				  _routing: ""routing_value""
				},
				{
				  _index: ""different_index"",
				  _type: ""people"",
				  _id: ""some-string-id"",
				  _routing: ""routing_value""
				},
				{
				  _index: ""nest_test_data"",
                  _type: ""morelikethistestdoc"",
                  doc: {
                    name: ""elasticsearch"",
                    text: ""foo""
                  },
                  per_field_analyzer: {
                    name: ""keyword""
                  }
				},
				{
                  _index: ""nest_test_data"",
                  _type: ""morelikethistestdoc"",
                  doc: {
                    name: ""nest""
                  }
				},
				{
				  _index: ""myindex"",
                  _type: ""mytype"",
                  doc: {
                    name: ""foo""
                  }
				}]
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void MoreLikeThisWithDocumentsExplicit()
		{
			var s = new MoreLikeThisQueryDescriptor<ElasticsearchProject>()
				.OnFields(p => p.Name)
				.DocumentsExplicit(d => d
					.Get(1, g => g.Fields(p => p.Product.Name).Routing("routing_value"))
					.Get<Person>("some-string-id", g => g.Routing("routing_value").Type("people").Index("different_index"))
				);
			var json = TestElasticClient.Serialize(s);

			//NEST should default to not sending index but when specified explicitly it should not force it null
			var expected = @"{
				fields: [ ""name""],
				docs: [
				{
				  _type: ""elasticsearchprojects"",
				  _id: ""1"",
				  fields: [
				    ""product.name""
				  ],
				  _routing: ""routing_value""
				},
				{
				  _index: ""different_index"",
				  _type: ""people"",
				  _id: ""some-string-id"",
				  _routing: ""routing_value""
				}]
			}";
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
						.MinimumShouldMatch("30%")
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
                minimum_should_match: ""30%"",
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

using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class MatchQueryJson
	{
		[Test]
		public void MatchQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.Match(t=>t
						.Name("named_query")
						.OnField(f=>f.Name)
						.Query("this is a test")
						.MinimumShouldMatch("2<80%")
						.Rewrite(RewriteMultiTerm.ConstantScoreDefault)
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					match: {
						name : { 
							_name: ""named_query"",
							query : ""this is a test"",
							rewrite: ""constant_score_default"",
							minimum_should_match: ""2<80%""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		
		[Test]
		public void MatchPhraseQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.MatchPhrase(t=>t
						.Name("named_query")
						.OnField(f=>f.Name)
						.Lenient()
						.Query("this is a test")
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					match: {
						name : { 
							_name: ""named_query"",
							type: ""phrase"",
							query : ""this is a test"",
							lenient: true
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void MatchQuerySomeOptions()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Match(t => t
						.OnField(f => f.Name)
						.Query("this is a test")
						
						.Fuzziness(1.0)
						.Analyzer("my_analyzer")
						.CutoffFrequency(0.3)
						.Rewrite(RewriteMultiTerm.ConstantScoreFilter)
						.PrefixLength(2)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					match: {
						name : { 
							query : ""this is a test"",
							analyzer : ""my_analyzer"",
							rewrite: ""constant_score_filter"",
							fuzziness: 1.0,
							cutoff_frequency: 0.3,
							prefix_length: 2
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

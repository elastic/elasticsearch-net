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
						.OnField(f=>f.Name)
						.Query("this is a test")
						.Rewrite(RewriteMultiTerm.constant_score_default)
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					match: {
						name : { 
							query : ""this is a test"",
							rewrite: ""constant_score_default""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
			s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.Match(t=>t
						.OnField(f=>f.Name)
						.Query("this is a test")
						.Rewrite(RewriteMultiTerm.constant_score_default)
					)
			);
				
			json = TestElasticClient.Serialize(s);
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
						.Rewrite(RewriteMultiTerm.constant_score_filter)
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

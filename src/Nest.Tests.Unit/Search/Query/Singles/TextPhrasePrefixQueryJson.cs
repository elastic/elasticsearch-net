using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TextPhrasePrefixQueryJson
	{
		[Test]
		public void TextPhrasePrefixQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.TextPhrasePrefix(t=>t
						.OnField(f=>f.Name)
						.Query("this is a test")
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""phrase_prefix"",
							query : ""this is a test""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TextPhrasePrefixQuerySomeOptions()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TextPhrasePrefix(t => t
						.OnField(f => f.Name)
						.Query("this is a test")
						.Fuzziness(1.0)
						.Analyzer("my_analyzer")
						.PrefixLength(2)
						.Operator(Operator.and)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""phrase_prefix"",
							query : ""this is a test"",
							analyzer : ""my_analyzer"",
							fuzziness: 1.0,
							prefix_length: 2,
							operator: ""and""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

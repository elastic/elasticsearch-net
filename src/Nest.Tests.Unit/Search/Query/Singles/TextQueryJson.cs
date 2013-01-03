using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TextQueryJson
	{
		[Test]
		public void TextQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.Text(t=>t
						.OnField(f=>f.Name)
						.QueryString("this is a test")
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							query : ""this is a test""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TextQuerySomeOptions()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Text(t => t
						.OnField(f => f.Name)
						.QueryString("this is a test")
						.Fuzziness(1.0)
						.Analyzer("my_analyzer")
						.PrefixLength(2)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							query : ""this is a test"",
							analyzer : ""my_analyzer"",
							fuzziness: 1.0,
							prefix_length: 2
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

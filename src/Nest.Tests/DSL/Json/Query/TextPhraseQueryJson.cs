using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.FilterTests
{
	[TestFixture]
	public class TextPhraseQueryJson
	{
		[Test]
		public void TextPhraseQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.TextPhrase(t=>t
						.OnField(f=>f.Name)
						.QueryString("this is a test")
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""text_phrase"", 
							query : ""this is a test""
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void TextPhraseQuerySomeOptions()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TextPhrase(t => t
						.OnField(f => f.Name)
						.QueryString("this is a test")
						.Fuzziness(1.0)
						.Analyzer("my_analyzer")
						.PrefixLength(2)
					)
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""text_phrase"", 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.QueryTests
{
	[TestFixture]
	public class TextPhrasePrefixQueryJson
	{
		[Test]
		public void TextPhrasePrefixQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.TextPhrasePrefix(t=>t
						.OnField(f=>f.Name)
						.QueryString("this is a test")
					)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""text_phrase_prefix"", 
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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TextPhrasePrefix(t => t
						.OnField(f => f.Name)
						.QueryString("this is a test")
						.Fuzziness(1.0)
						.Analyzer("my_analyzer")
						.PrefixLength(2)
						.Operator(Operator.and)
					)
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					text: {
						name : { 
							type: ""text_phrase_prefix"", 
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

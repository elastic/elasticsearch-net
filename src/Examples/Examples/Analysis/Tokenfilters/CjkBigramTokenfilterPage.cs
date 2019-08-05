using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class CjkBigramTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line18()
		{
			// tag::4a40ccf6b1a0090da8d8033b435b5b7d[]
			var response0 = new SearchResponse<object>();
			// end::4a40ccf6b1a0090da8d8033b435b5b7d[]

			response0.MatchesExample(@"PUT /cjk_bigram_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""han_bigrams"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""han_bigrams_filter""]
			                }
			            },
			            ""filter"" : {
			                ""han_bigrams_filter"" : {
			                    ""type"" : ""cjk_bigram"",
			                    ""ignored_scripts"": [
			                        ""hiragana"",
			                        ""katakana"",
			                        ""hangul""
			                    ],
			                    ""output_unigrams"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
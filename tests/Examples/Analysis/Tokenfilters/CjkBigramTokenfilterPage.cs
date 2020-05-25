// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class CjkBigramTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/cjk-bigram-tokenfilter.asciidoc:22")]
		public void Line22()
		{
			// tag::b8c03bbd917d0cf5474a3e46ebdd7aad[]
			var response0 = new SearchResponse<object>();
			// end::b8c03bbd917d0cf5474a3e46ebdd7aad[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""cjk_bigram""],
			  ""text"" : ""東京都は、日本の首都であり""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/cjk-bigram-tokenfilter.asciidoc:126")]
		public void Line126()
		{
			// tag::7230edf3a8cdb5e4091fad668b4049dc[]
			var response0 = new SearchResponse<object>();
			// end::7230edf3a8cdb5e4091fad668b4049dc[]

			response0.MatchesExample(@"PUT /cjk_bigram_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_cjk_bigram"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""cjk_bigram""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/cjk-bigram-tokenfilter.asciidoc:176")]
		public void Line176()
		{
			// tag::6b328ac5a63ac7f26b011a6905083934[]
			var response0 = new SearchResponse<object>();
			// end::6b328ac5a63ac7f26b011a6905083934[]

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
			                        ""hangul"",
			                        ""hiragana"",
			                        ""katakana""
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

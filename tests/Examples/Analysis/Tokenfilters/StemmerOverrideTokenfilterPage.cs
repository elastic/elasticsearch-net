// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class StemmerOverrideTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-override-tokenfilter.asciidoc:25")]
		public void Line25()
		{
			// tag::8995e7cf49c4d870aea334645b70ed13[]
			var response0 = new SearchResponse<object>();
			// end::8995e7cf49c4d870aea334645b70ed13[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""custom_stems"", ""porter_stem""]
			                }
			            },
			            ""filter"" : {
			                ""custom_stems"" : {
			                    ""type"" : ""stemmer_override"",
			                    ""rules_path"" : ""analysis/stemmer_override.txt""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/stemmer-override-tokenfilter.asciidoc:57")]
		public void Line57()
		{
			// tag::35e24a98b35cadd0b1b370ada79249e1[]
			var response0 = new SearchResponse<object>();
			// end::35e24a98b35cadd0b1b370ada79249e1[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""custom_stems"", ""porter_stem""]
			                }
			            },
			            ""filter"" : {
			                ""custom_stems"" : {
			                    ""type"" : ""stemmer_override"",
			                    ""rules"" : [
			                        ""running => run"",
			                        ""stemmer => stemmer""
			                    ]
			                }
			            }
			        }
			    }
			}");
		}
	}
}

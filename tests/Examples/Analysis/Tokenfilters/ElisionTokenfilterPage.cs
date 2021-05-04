// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class ElisionTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/elision-tokenfilter.asciidoc:34")]
		public void Line34()
		{
			// tag::446e8fc8ccfb13bb5ec64e32a5676d18[]
			var response0 = new SearchResponse<object>();
			// end::446e8fc8ccfb13bb5ec64e32a5676d18[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""elision""],
			  ""text"" : ""j’examine près du wharf""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/elision-tokenfilter.asciidoc:96")]
		public void Line96()
		{
			// tag::26d49d11bb37c3f4ef8179010e34b50e[]
			var response0 = new SearchResponse<object>();
			// end::26d49d11bb37c3f4ef8179010e34b50e[]

			response0.MatchesExample(@"PUT /elision_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""whitespace_elision"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""elision""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/elision-tokenfilter.asciidoc:165")]
		public void Line165()
		{
			// tag::fc575e08d0bc8f4cb03a54e5d57fff7b[]
			var response0 = new SearchResponse<object>();
			// end::fc575e08d0bc8f4cb03a54e5d57fff7b[]

			response0.MatchesExample(@"PUT /elision_case_sensitive_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""default"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""elision_case_sensitive""]
			                }
			            },
			            ""filter"" : {
			                ""elision_case_sensitive"" : {
			                    ""type"" : ""elision"",
			                    ""articles"" : [""l"", ""m"", ""t"", ""qu"", ""n"", ""s"", ""j""],
			                    ""articles_case"": true
			                }
			            }
			        }
			    }
			}");
		}
	}
}

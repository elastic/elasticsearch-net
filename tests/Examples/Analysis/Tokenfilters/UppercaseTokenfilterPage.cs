// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class UppercaseTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/uppercase-tokenfilter.asciidoc:30")]
		public void Line30()
		{
			// tag::9f7671119236423e0e40801ef6485af1[]
			var response0 = new SearchResponse<object>();
			// end::9f7671119236423e0e40801ef6485af1[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""uppercase""],
			  ""text"" : ""the Quick FoX JUMPs""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/uppercase-tokenfilter.asciidoc:92")]
		public void Line92()
		{
			// tag::9db72fe811ee61ee3f7baa45916d20e0[]
			var response0 = new SearchResponse<object>();
			// end::9db72fe811ee61ee3f7baa45916d20e0[]

			response0.MatchesExample(@"PUT uppercase_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""whitespace_uppercase"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""uppercase""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}

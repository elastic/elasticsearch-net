using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class UppercaseTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
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
		public void Line91()
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
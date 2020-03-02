using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class AsciifoldingTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
		{
			// tag::00d65f7b9daa1c6b18eedd8ace206bae[]
			var response0 = new SearchResponse<object>();
			// end::00d65f7b9daa1c6b18eedd8ace206bae[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""asciifolding""],
			  ""text"" : ""açaí à la carte""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line83()
		{
			// tag::a976bdf566730e35c5277740c1e3a7f2[]
			var response0 = new SearchResponse<object>();
			// end::a976bdf566730e35c5277740c1e3a7f2[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_asciifolding"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""asciifolding""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line118()
		{
			// tag::c5a0248213307f8e036a26e3294ad611[]
			var response0 = new SearchResponse<object>();
			// end::c5a0248213307f8e036a26e3294ad611[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_asciifolding"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""my_ascii_folding""]
			                }
			            },
			            ""filter"" : {
			                ""my_ascii_folding"" : {
			                    ""type"" : ""asciifolding"",
			                    ""preserve_original"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
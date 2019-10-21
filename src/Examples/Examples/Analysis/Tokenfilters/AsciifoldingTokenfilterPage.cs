using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class AsciifoldingTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::0d4cb64eca426ac03110fdfd01367ee9[]
			var response0 = new SearchResponse<object>();
			// end::0d4cb64eca426ac03110fdfd01367ee9[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""default"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""asciifolding""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line31()
		{
			// tag::f0609100be8e9eb4af6cbc75d0c40ebe[]
			var response0 = new SearchResponse<object>();
			// end::f0609100be8e9eb4af6cbc75d0c40ebe[]

			response0.MatchesExample(@"PUT /asciifold_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""default"" : {
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
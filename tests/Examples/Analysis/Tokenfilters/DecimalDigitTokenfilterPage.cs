using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class DecimalDigitTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/decimal-digit-tokenfilter.asciidoc:20")]
		public void Line20()
		{
			// tag::a21319c9eff1ac47d7fe7490f1ef2efa[]
			var response0 = new SearchResponse<object>();
			// end::a21319c9eff1ac47d7fe7490f1ef2efa[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""decimal_digit""],
			  ""text"" : ""१-one two-२ ३""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/decimal-digit-tokenfilter.asciidoc:75")]
		public void Line75()
		{
			// tag::121b8bc28620095dfa570a989bcdb04e[]
			var response0 = new SearchResponse<object>();
			// end::121b8bc28620095dfa570a989bcdb04e[]

			response0.MatchesExample(@"PUT /decimal_digit_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""whitespace_decimal_digit"" : {
			                    ""tokenizer"" : ""whitespace"",
			                    ""filter"" : [""decimal_digit""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}
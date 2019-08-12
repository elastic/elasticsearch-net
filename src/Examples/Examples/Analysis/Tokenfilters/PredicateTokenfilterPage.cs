using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class PredicateTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line19()
		{
			// tag::10338787b66a7f93270c3b88dd6197f8[]
			var response0 = new SearchResponse<object>();
			// end::10338787b66a7f93270c3b88dd6197f8[]

			response0.MatchesExample(@"PUT /condition_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""my_script_filter"" ]
			                }
			            },
			            ""filter"" : {
			                ""my_script_filter"" : {
			                    ""type"" : ""predicate_token_filter"",
			                    ""script"" : {
			                        ""source"" : ""token.getTerm().length() > 5""  \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::e20493a20d3992a97238b87c6930f08d[]
			var response0 = new SearchResponse<object>();
			// end::e20493a20d3992a97238b87c6930f08d[]

			response0.MatchesExample(@"POST /condition_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""What Flapdoodle""
			}");
		}
	}
}
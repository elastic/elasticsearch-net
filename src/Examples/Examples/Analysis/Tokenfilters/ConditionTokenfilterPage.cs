using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class ConditionTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line22()
		{
			// tag::59fd7082698a6b12d028105456016a66[]
			var response0 = new SearchResponse<object>();
			// end::59fd7082698a6b12d028105456016a66[]

			response0.MatchesExample(@"PUT /condition_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""my_condition"" ]
			                }
			            },
			            ""filter"" : {
			                ""my_condition"" : {
			                    ""type"" : ""condition"",
			                    ""filter"" : [ ""lowercase"" ],
			                    ""script"" : {
			                        ""source"" : ""token.getTerm().length() < 5""  \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line54()
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
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class MultiplexerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::c306212babadc14fa124b88fd8c43a6b[]
			var response0 = new SearchResponse<object>();
			// end::c306212babadc14fa124b88fd8c43a6b[]

			response0.MatchesExample(@"PUT /multiplexer_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""my_multiplexer"" ]
			                }
			            },
			            ""filter"" : {
			                ""my_multiplexer"" : {
			                    ""type"" : ""multiplexer"",
			                    ""filters"" : [ ""lowercase"", ""lowercase, porter_stem"" ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line61()
		{
			// tag::fa9a3ef94470f3d9bd6500b65bf993d1[]
			var response0 = new SearchResponse<object>();
			// end::fa9a3ef94470f3d9bd6500b65bf993d1[]

			response0.MatchesExample(@"POST /multiplexer_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""Going HOME""
			}");
		}
	}
}
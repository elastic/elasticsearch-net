using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class ReverseTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::e09d30195108bd6a1f6857394a6123ea[]
			var response0 = new SearchResponse<object>();
			// end::e09d30195108bd6a1f6857394a6123ea[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""reverse""],
			  ""text"" : ""quick fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line79()
		{
			// tag::aa5fbb68d3a8e0d0c894791cb6cf0b13[]
			var response0 = new SearchResponse<object>();
			// end::aa5fbb68d3a8e0d0c894791cb6cf0b13[]

			response0.MatchesExample(@"PUT reverse_example
			{
			  ""settings"" : {
			    ""analysis"" : {
			      ""analyzer"" : {
			        ""whitespace_reverse"" : {
			          ""tokenizer"" : ""whitespace"",
			          ""filter"" : [""reverse""]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
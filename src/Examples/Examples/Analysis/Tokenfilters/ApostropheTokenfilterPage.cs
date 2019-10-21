using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class ApostropheTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line22()
		{
			// tag::3343a4cf559060c422d86c786a95e535[]
			var response0 = new SearchResponse<object>();
			// end::3343a4cf559060c422d86c786a95e535[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""apostrophe""],
			  ""text"" : ""Istanbul'a veya Istanbul'dan""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::da19e4ecfabcbabdc894687106eaccdc[]
			var response0 = new SearchResponse<object>();
			// end::da19e4ecfabcbabdc894687106eaccdc[]

			response0.MatchesExample(@"PUT /apostrophe_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""standard_apostrophe"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""apostrophe""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MatchPhraseQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::83f95657beca9bf5d8264c80c7fb463f[]
			var response0 = new SearchResponse<object>();
			// end::83f95657beca9bf5d8264c80c7fb463f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase"" : {
			            ""message"" : ""this is a test""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line31()
		{
			// tag::72231b7debac60c95b9869a97dafda3a[]
			var response0 = new SearchResponse<object>();
			// end::72231b7debac60c95b9869a97dafda3a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase"" : {
			            ""message"" : {
			                ""query"" : ""this is a test"",
			                ""analyzer"" : ""my_analyzer""
			            }
			        }
			    }
			}");
		}
	}
}
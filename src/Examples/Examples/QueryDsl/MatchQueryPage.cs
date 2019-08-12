using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MatchQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::fa2fe60f570bd930d2891778c6efbfe6[]
			var response0 = new SearchResponse<object>();
			// end::fa2fe60f570bd930d2891778c6efbfe6[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : ""this is a test""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::6138d6919f3cbaaf61e1092f817d295c[]
			var response0 = new SearchResponse<object>();
			// end::6138d6919f3cbaaf61e1092f817d295c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""this is a test"",
			                ""operator"" : ""and""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line87()
		{
			// tag::5043b83a89091fa00edb341ddf7ba370[]
			var response0 = new SearchResponse<object>();
			// end::5043b83a89091fa00edb341ddf7ba370[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""this is a testt"",
			                ""fuzziness"": ""AUTO""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line110()
		{
			// tag::0ac9916f47a2483b89c1416684af322a[]
			var response0 = new SearchResponse<object>();
			// end::0ac9916f47a2483b89c1416684af322a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""to be or not to be"",
			                ""operator"" : ""and"",
			                ""zero_terms_query"": ""all""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line138()
		{
			// tag::7f56755fb6c42f7e6203339a6d0cb6e6[]
			var response0 = new SearchResponse<object>();
			// end::7f56755fb6c42f7e6203339a6d0cb6e6[]

			response0.MatchesExample(@"GET /_search
			{
			   ""query"": {
			       ""match"" : {
			           ""message"": {
			               ""query"" : ""ny city"",
			               ""auto_generate_synonyms_phrase_query"" : false
			           }
			       }
			   }
			}");
		}
	}
}
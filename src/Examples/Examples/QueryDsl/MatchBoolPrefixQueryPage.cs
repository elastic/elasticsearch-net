using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MatchBoolPrefixQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line13()
		{
			// tag::79c7e8a98c47fad3e96c654d34aa049a[]
			var response0 = new SearchResponse<object>();
			// end::79c7e8a98c47fad3e96c654d34aa049a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"" : ""quick brown f""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::effc6b4784aca12691de5d5782c0384b[]
			var response0 = new SearchResponse<object>();
			// end::effc6b4784aca12691de5d5782c0384b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""should"": [
			                { ""term"": { ""message"": ""quick"" }},
			                { ""term"": { ""message"": ""brown"" }},
			                { ""prefix"": { ""message"": ""f""}}
			            ]
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line61()
		{
			// tag::953aab6cbd12a4f034cf02bf34d62a72[]
			var response0 = new SearchResponse<object>();
			// end::953aab6cbd12a4f034cf02bf34d62a72[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"": {
			                ""query"": ""quick brown f"",
			                ""analyzer"": ""keyword""
			            }
			        }
			    }
			}");
		}
	}
}
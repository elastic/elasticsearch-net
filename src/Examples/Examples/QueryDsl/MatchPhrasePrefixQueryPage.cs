using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MatchPhrasePrefixQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::d071647d9248aaf6b4ecc277cd9f24b2[]
			var response0 = new SearchResponse<object>();
			// end::d071647d9248aaf6b4ecc277cd9f24b2[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase_prefix"" : {
			            ""message"" : ""quick brown f""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line30()
		{
			// tag::93fb5b3445636611e024783b06f9af93[]
			var response0 = new SearchResponse<object>();
			// end::93fb5b3445636611e024783b06f9af93[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase_prefix"" : {
			            ""message"" : {
			                ""query"" : ""quick brown f"",
			                ""max_expansions"" : 10
			            }
			        }
			    }
			}");
		}
	}
}
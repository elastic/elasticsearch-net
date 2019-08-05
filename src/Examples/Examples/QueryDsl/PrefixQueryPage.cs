using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class PrefixQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line16()
		{
			// tag::81514791349e0e79ac565160e42889c0[]
			var response0 = new SearchResponse<object>();
			// end::81514791349e0e79ac565160e42889c0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"": {
			            ""user"": {
			                ""value"": ""ki""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line54()
		{
			// tag::32ea547cefa2976c8c3c2eb45a2a4ff4[]
			var response0 = new SearchResponse<object>();
			// end::32ea547cefa2976c8c3c2eb45a2a4ff4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"" : { ""user"" : ""ki"" }
			    }
			}");
		}
	}
}
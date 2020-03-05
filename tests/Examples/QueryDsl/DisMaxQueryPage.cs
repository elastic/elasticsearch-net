using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class DisMaxQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::fcf5a593cfe8809d98a5239ad9c82038[]
			var response0 = new SearchResponse<object>();
			// end::fcf5a593cfe8809d98a5239ad9c82038[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""dis_max"" : {
			            ""queries"" : [
			                { ""term"" : { ""title"" : ""Quick pets"" }},
			                { ""term"" : { ""body"" : ""Quick pets"" }}
			            ],
			            ""tie_breaker"" : 0.7
			        }
			    }
			}");
		}
	}
}
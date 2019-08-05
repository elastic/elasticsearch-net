using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class CountPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::8f0511f8a5cb176ff2afdd4311799a33[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::8f0511f8a5cb176ff2afdd4311799a33[]

			response0.MatchesExample(@"PUT /twitter/_doc/1?refresh
			{
			    ""user"": ""kimchy""
			}");

			response1.MatchesExample(@"GET /twitter/_count?q=user:kimchy");

			response2.MatchesExample(@"GET /twitter/_count
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
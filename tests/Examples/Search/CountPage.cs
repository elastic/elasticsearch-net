using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class CountPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/count.asciidoc:7")]
		public void Line7()
		{
			// tag::1b542e3ea87a742f95641d64dcfb1bdb[]
			var response0 = new SearchResponse<object>();
			// end::1b542e3ea87a742f95641d64dcfb1bdb[]

			response0.MatchesExample(@"GET /twitter/_count?q=user:kimchy");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/count.asciidoc:92")]
		public void Line92()
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
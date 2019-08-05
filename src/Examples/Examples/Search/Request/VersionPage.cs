using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class VersionPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line7()
		{
			// tag::9535be36eac8a589bd6bf7b7228eefd7[]
			var response0 = new SearchResponse<object>();
			// end::9535be36eac8a589bd6bf7b7228eefd7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""version"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
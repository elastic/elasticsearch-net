using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class MinScorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::8e8ceac8fc99348f885f85ff714557fd[]
			var response0 = new SearchResponse<object>();
			// end::8e8ceac8fc99348f885f85ff714557fd[]

			response0.MatchesExample(@"GET /_search
			{
			    ""min_score"": 0.5,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
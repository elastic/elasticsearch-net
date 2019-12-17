using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class QueryStringSyntaxPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line282()
		{
			// tag::8ca595edc1e1f1ae6bc3ee05e0aa1e32[]
			var response0 = new SearchResponse<object>();
			// end::8ca595edc1e1f1ae6bc3ee05e0aa1e32[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""query"" : {
			    ""query_string"" : {
			      ""query"" : ""kimchy\\!"",
			      ""fields""  : [""user""]
			    }
			  }
			}");
		}
	}
}
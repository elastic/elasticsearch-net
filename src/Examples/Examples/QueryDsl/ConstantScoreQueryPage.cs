using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class ConstantScoreQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line12()
		{
			// tag::d59a084640acf2f5c51d3068d38b5fc0[]
			var response0 = new SearchResponse<object>();
			// end::d59a084640acf2f5c51d3068d38b5fc0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""term"" : { ""user"" : ""kimchy""}
			            },
			            ""boost"" : 1.2
			        }
			    }
			}");
		}
	}
}
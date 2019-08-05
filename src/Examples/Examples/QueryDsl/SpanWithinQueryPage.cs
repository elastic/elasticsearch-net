using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanWithinQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::9429e565d0b56289a10b81220660163c[]
			var response0 = new SearchResponse<object>();
			// end::9429e565d0b56289a10b81220660163c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_within"" : {
			            ""little"" : {
			                ""span_term"" : { ""field1"" : ""foo"" }
			            },
			            ""big"" : {
			                ""span_near"" : {
			                    ""clauses"" : [
			                        { ""span_term"" : { ""field1"" : ""bar"" } },
			                        { ""span_term"" : { ""field1"" : ""baz"" } }
			                    ],
			                    ""slop"" : 5,
			                    ""in_order"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanContainingQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::73094e82ce3850cbb6f9d071cc8a2d14[]
			var response0 = new SearchResponse<object>();
			// end::73094e82ce3850cbb6f9d071cc8a2d14[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_containing"" : {
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
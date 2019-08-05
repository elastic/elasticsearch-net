using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanNotQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line13()
		{
			// tag::4a3b37cdf27279800355ccdef0e13128[]
			var response0 = new SearchResponse<object>();
			// end::4a3b37cdf27279800355ccdef0e13128[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_not"" : {
			            ""include"" : {
			                ""span_term"" : { ""field1"" : ""hoya"" }
			            },
			            ""exclude"" : {
			                ""span_near"" : {
			                    ""clauses"" : [
			                        { ""span_term"" : { ""field1"" : ""la"" } },
			                        { ""span_term"" : { ""field1"" : ""hoya"" } }
			                    ],
			                    ""slop"" : 0,
			                    ""in_order"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanNearQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line13()
		{
			// tag::35ee06bbcc1291446187f1eeaf7eed90[]
			var response0 = new SearchResponse<object>();
			// end::35ee06bbcc1291446187f1eeaf7eed90[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_near"" : {
			            ""clauses"" : [
			                { ""span_term"" : { ""field"" : ""value1"" } },
			                { ""span_term"" : { ""field"" : ""value2"" } },
			                { ""span_term"" : { ""field"" : ""value3"" } }
			            ],
			            ""slop"" : 12,
			            ""in_order"" : false
			        }
			    }
			}");
		}
	}
}
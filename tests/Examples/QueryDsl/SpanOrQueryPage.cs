using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanOrQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-or-query.asciidoc:11")]
		public void Line11()
		{
			// tag::b8b1c96897001708b2cfad92ac36a21f[]
			var response0 = new SearchResponse<object>();
			// end::b8b1c96897001708b2cfad92ac36a21f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_or"" : {
			            ""clauses"" : [
			                { ""span_term"" : { ""field"" : ""value1"" } },
			                { ""span_term"" : { ""field"" : ""value2"" } },
			                { ""span_term"" : { ""field"" : ""value3"" } }
			            ]
			        }
			    }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class WildcardQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/wildcard-query.asciidoc:21")]
		public void Line21()
		{
			// tag::d31062ff8c015387889fed4ad86fd914[]
			var response0 = new SearchResponse<object>();
			// end::d31062ff8c015387889fed4ad86fd914[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""wildcard"": {
			            ""user"": {
			                ""value"": ""ki*y"",
			                ""boost"": 1.0,
			                ""rewrite"": ""constant_score""
			            }
			        }
			    }
			}");
		}
	}
}
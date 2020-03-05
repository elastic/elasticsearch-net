using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class ExplainPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/explain.asciidoc:7")]
		public void Line7()
		{
			// tag::e405e90fe3207157d3c0f9c76c6778e8[]
			var response0 = new SearchResponse<object>();
			// end::e405e90fe3207157d3c0f9c76c6778e8[]

			response0.MatchesExample(@"GET /_search
			{
			    ""explain"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
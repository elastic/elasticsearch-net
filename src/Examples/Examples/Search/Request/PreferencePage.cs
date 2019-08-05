using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class PreferencePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line59()
		{
			// tag::9405de6fd841c32ac510eb0a7eeed989[]
			var response0 = new SearchResponse<object>();
			// end::9405de6fd841c32ac510eb0a7eeed989[]

			response0.MatchesExample(@"GET /_search?preference=xyzabc123
			{
			    ""query"": {
			        ""match"": {
			            ""title"": ""elasticsearch""
			        }
			    }
			}");
		}
	}
}
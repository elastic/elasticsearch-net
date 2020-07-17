using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class ClearScrollApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/clear-scroll-api.asciidoc:25")]
		public void Line25()
		{
			// tag::b0d64d0a554549e5b2808002a0725493[]
			var response0 = new SearchResponse<object>();
			// end::b0d64d0a554549e5b2808002a0725493[]

			response0.MatchesExample(@"DELETE /_search/scroll
			{
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==""
			}");
		}
	}
}
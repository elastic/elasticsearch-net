using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class ScrollApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/scroll-api.asciidoc:25")]
		public void Line25()
		{
			// tag::9ee6b38bc04ee4e73339b986ee09c30a[]
			var response0 = new SearchResponse<object>();
			// end::9ee6b38bc04ee4e73339b986ee09c30a[]

			response0.MatchesExample(@"GET /_search/scroll
			{
			    ""scroll_id"" : ""DXF1ZXJ5QW5kRmV0Y2gBAAAAAAAAAD4WYm9laVYtZndUQlNsdDcwakFMNjU1QQ==""
			}");
		}
	}
}
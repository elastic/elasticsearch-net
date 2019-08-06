using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class NodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::db20adb70a8e8d0709d15ba0daf18d23[]
			var response0 = new SearchResponse<object>();
			// end::db20adb70a8e8d0709d15ba0daf18d23[]

			response0.MatchesExample(@"GET /_cat/nodes?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line54()
		{
			// tag::21d3e98d911642ab3bda2657f7a06f80[]
			var response0 = new SearchResponse<object>();
			// end::21d3e98d911642ab3bda2657f7a06f80[]

			response0.MatchesExample(@"GET /_cat/nodes?v&h=id,ip,port,v,m");
		}
	}
}
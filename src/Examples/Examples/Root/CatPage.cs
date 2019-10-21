using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class CatPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::45bde49f35ffae3f3dabc77a592241b4[]
			var response0 = new SearchResponse<object>();
			// end::45bde49f35ffae3f3dabc77a592241b4[]

			response0.MatchesExample(@"GET /_cat/master?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::179dabbc531ede7a1813d1a11ce5b5fd[]
			var response0 = new SearchResponse<object>();
			// end::179dabbc531ede7a1813d1a11ce5b5fd[]

			response0.MatchesExample(@"GET /_cat/master?help");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::d940059e16675a40e3d278073331eeed[]
			var response0 = new SearchResponse<object>();
			// end::d940059e16675a40e3d278073331eeed[]

			response0.MatchesExample(@"GET /_cat/nodes?h=ip,port,heapPercent,name");
		}

		[U(Skip = "Example not implemented")]
		public void Line192()
		{
			// tag::794fa23d07c42900b5e97fb9bf323941[]
			var response0 = new SearchResponse<object>();
			// end::794fa23d07c42900b5e97fb9bf323941[]

			response0.MatchesExample(@"GET _cat/templates?v&s=order:desc,index_patterns");
		}
	}
}
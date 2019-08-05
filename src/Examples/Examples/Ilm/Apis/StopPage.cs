using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class StopPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line75()
		{
			// tag::585a34ad79aee16678b37da785933ac8[]
			var response0 = new SearchResponse<object>();
			// end::585a34ad79aee16678b37da785933ac8[]

			response0.MatchesExample(@"POST _ilm/stop");
		}

		[U]
		[SkipExample]
		public void Line84()
		{
			// tag::bc5fcc40c29087a0df7b5405bb70de5c[]
			var response0 = new SearchResponse<object>();
			// end::bc5fcc40c29087a0df7b5405bb70de5c[]

			response0.MatchesExample(@"{
			  ""acknowledged"": true
			}");
		}
	}
}
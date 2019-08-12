using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class StopPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::6b1336ff477f91d4a0db0b06db546ff0[]
			var response0 = new SearchResponse<object>();
			// end::6b1336ff477f91d4a0db0b06db546ff0[]

			response0.MatchesExample(@"POST _watcher/_stop");
		}
	}
}
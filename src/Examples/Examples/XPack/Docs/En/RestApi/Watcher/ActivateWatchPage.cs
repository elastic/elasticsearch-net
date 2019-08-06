using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class ActivateWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U(Skip = "Example not implemented")]
		public void Line69()
		{
			// tag::3477a89d869b1f7f72d50c2ca86c4679[]
			var response0 = new SearchResponse<object>();
			// end::3477a89d869b1f7f72d50c2ca86c4679[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch/_activate");
		}
	}
}
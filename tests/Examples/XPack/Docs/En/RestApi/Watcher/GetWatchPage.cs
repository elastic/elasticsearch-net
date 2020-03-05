using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class GetWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/get-watch.asciidoc:49")]
		public void Line49()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}
	}
}
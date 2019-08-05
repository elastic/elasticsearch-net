using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class DeactivateWatchPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line35()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U]
		[SkipExample]
		public void Line68()
		{
			// tag::f63f6343e74bd5c844854272e746de14[]
			var response0 = new SearchResponse<object>();
			// end::f63f6343e74bd5c844854272e746de14[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch/_deactivate");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class DeleteWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line60()
		{
			// tag::2aa548b692fc2fe7b6f0d90eb8b2ae29[]
			var response0 = new SearchResponse<object>();
			// end::2aa548b692fc2fe7b6f0d90eb8b2ae29[]

			response0.MatchesExample(@"DELETE _watcher/watch/my_watch");
		}
	}
}
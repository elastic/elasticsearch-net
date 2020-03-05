using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class StartPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/start.asciidoc:43")]
		public void Line43()
		{
			// tag::97ea5ab17213cb1faaf6f3ea13607098[]
			var response0 = new SearchResponse<object>();
			// end::97ea5ab17213cb1faaf6f3ea13607098[]

			response0.MatchesExample(@"POST _watcher/_start");
		}
	}
}
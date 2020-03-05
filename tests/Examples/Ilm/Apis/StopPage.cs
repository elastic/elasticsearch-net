using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class StopPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/stop.asciidoc:80")]
		public void Line80()
		{
			// tag::585a34ad79aee16678b37da785933ac8[]
			var response0 = new SearchResponse<object>();
			// end::585a34ad79aee16678b37da785933ac8[]

			response0.MatchesExample(@"POST _ilm/stop");
		}
	}
}
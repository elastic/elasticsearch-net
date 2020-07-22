using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm
{
	public class StartStopPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:59")]
		public void Line59()
		{
			// tag::182df084f028479ecbe8d7648ddad892[]
			var response0 = new SearchResponse<object>();
			// end::182df084f028479ecbe8d7648ddad892[]

			response0.MatchesExample(@"GET _ilm/status");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:82")]
		public void Line82()
		{
			// tag::585a34ad79aee16678b37da785933ac8[]
			var response0 = new SearchResponse<object>();
			// end::585a34ad79aee16678b37da785933ac8[]

			response0.MatchesExample(@"POST _ilm/stop");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/start-stop.asciidoc:135")]
		public void Line135()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class NodeattrsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::e20e2e6f949ac660a77840a9263fadef[]
			var response0 = new SearchResponse<object>();
			// end::e20e2e6f949ac660a77840a9263fadef[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line53()
		{
			// tag::0c69c638073cc8518187b678dd33443c[]
			var response0 = new SearchResponse<object>();
			// end::0c69c638073cc8518187b678dd33443c[]

			response0.MatchesExample(@"GET /_cat/nodeattrs?v&h=name,pid,attr,value");
		}
	}
}
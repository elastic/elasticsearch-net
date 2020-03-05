using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class GetBasicStatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/get-basic-status.asciidoc:36")]
		public void Line36()
		{
			// tag::f92d2f5018a8843ffbb56ade15f84406[]
			var response0 = new SearchResponse<object>();
			// end::f92d2f5018a8843ffbb56ade15f84406[]

			response0.MatchesExample(@"GET /_license/basic_status");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class GetLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/get-lifecycle.asciidoc:76")]
		public void Line76()
		{
			// tag::2e7f4b9be999422a12abb680572b13c8[]
			var response0 = new SearchResponse<object>();
			// end::2e7f4b9be999422a12abb680572b13c8[]

			response0.MatchesExample(@"GET _ilm/policy/my_policy");
		}
	}
}
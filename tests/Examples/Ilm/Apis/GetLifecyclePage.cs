using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class GetLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line69()
		{
			// tag::2e7f4b9be999422a12abb680572b13c8[]
			var response0 = new SearchResponse<object>();
			// end::2e7f4b9be999422a12abb680572b13c8[]

			response0.MatchesExample(@"GET _ilm/policy/my_policy");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class ExplainPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line91()
		{
			// tag::0f6fa3a706a7c17858d3dbe329839ea6[]
			var response0 = new SearchResponse<object>();
			// end::0f6fa3a706a7c17858d3dbe329839ea6[]

			response0.MatchesExample(@"GET my_index/_ilm/explain");
		}
	}
}
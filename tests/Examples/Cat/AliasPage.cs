using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class AliasPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line73()
		{
			// tag::a003467caeafcb2a935522efb83080cb[]
			var response0 = new SearchResponse<object>();
			// end::a003467caeafcb2a935522efb83080cb[]

			response0.MatchesExample(@"GET /_cat/aliases?v");
		}
	}
}
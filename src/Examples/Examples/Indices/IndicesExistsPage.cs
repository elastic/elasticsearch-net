using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class IndicesExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::2609ef78d52856aece101d28fc1e0701[]
			var response0 = new SearchResponse<object>();
			// end::2609ef78d52856aece101d28fc1e0701[]

			response0.MatchesExample(@"HEAD twitter");
		}
	}
}
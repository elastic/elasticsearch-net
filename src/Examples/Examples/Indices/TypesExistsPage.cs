using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class TypesExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::7ee31b1237a714c49760a1cc499cbd87[]
			var response0 = new SearchResponse<object>();
			// end::7ee31b1237a714c49760a1cc499cbd87[]

			response0.MatchesExample(@"HEAD twitter/_mapping/tweet");
		}
	}
}
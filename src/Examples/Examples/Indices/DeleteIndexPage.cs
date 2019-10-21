using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class DeleteIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::98f14fddddea54a7d6149ab7b92e099d[]
			var response0 = new SearchResponse<object>();
			// end::98f14fddddea54a7d6149ab7b92e099d[]

			response0.MatchesExample(@"DELETE /twitter");
		}
	}
}
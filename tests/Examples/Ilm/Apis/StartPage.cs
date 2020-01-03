using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class StartPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line70()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}
	}
}
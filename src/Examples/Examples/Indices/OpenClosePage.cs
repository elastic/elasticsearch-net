using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class OpenClosePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line26()
		{
			// tag::3a6b9143f3de6258d44ff7e0eb38d953[]
			var response0 = new SearchResponse<object>();
			// end::3a6b9143f3de6258d44ff7e0eb38d953[]

			response0.MatchesExample(@"POST /my_index/_close");
		}

		[U(Skip = "Example not implemented")]
		public void Line51()
		{
			// tag::37e6177bf8803971d30a4252498c07a4[]
			var response0 = new SearchResponse<object>();
			// end::37e6177bf8803971d30a4252498c07a4[]

			response0.MatchesExample(@"POST /my_index/_open");
		}
	}
}
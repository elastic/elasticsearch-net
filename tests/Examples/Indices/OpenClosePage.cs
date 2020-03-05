using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class OpenClosePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::7f36828a03e8cb5a028d9a6efb056b88[]
			var response0 = new SearchResponse<object>();
			// end::7f36828a03e8cb5a028d9a6efb056b88[]

			response0.MatchesExample(@"POST /twitter/_open");
		}

		[U(Skip = "Example not implemented")]
		public void Line103()
		{
			// tag::37e6177bf8803971d30a4252498c07a4[]
			var response0 = new SearchResponse<object>();
			// end::37e6177bf8803971d30a4252498c07a4[]

			response0.MatchesExample(@"POST /my_index/_open");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class FlushPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::bf7b04e79b861d76d1922a588d57f817[]
			var response0 = new SearchResponse<object>();
			// end::bf7b04e79b861d76d1922a588d57f817[]

			response0.MatchesExample(@"POST /twitter/_flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line116()
		{
			// tag::cefde3553fdbd516813e73a603c72c24[]
			var response0 = new SearchResponse<object>();
			// end::cefde3553fdbd516813e73a603c72c24[]

			response0.MatchesExample(@"POST /kimchy/_flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line126()
		{
			// tag::66db9f5108a3936115f1fb64c844934a[]
			var response0 = new SearchResponse<object>();
			// end::66db9f5108a3936115f1fb64c844934a[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line136()
		{
			// tag::f27c28ddbf4c266b5f42d14da837b8de[]
			var response0 = new SearchResponse<object>();
			// end::f27c28ddbf4c266b5f42d14da837b8de[]

			response0.MatchesExample(@"POST /_flush");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.IndexModules.Allocation
{
	public class PrioritizationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line17()
		{
			// tag::8703f3b1b3895543abc36e2a7a0013d3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::8703f3b1b3895543abc36e2a7a0013d3[]

			response0.MatchesExample(@"PUT index_1");

			response1.MatchesExample(@"PUT index_2");

			response2.MatchesExample(@"PUT index_3
			{
			  ""settings"": {
			    ""index.priority"": 10
			  }
			}");

			response3.MatchesExample(@"PUT index_4
			{
			  ""settings"": {
			    ""index.priority"": 5
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::a425fcab60f603504becee7d001f0a4b[]
			var response0 = new SearchResponse<object>();
			// end::a425fcab60f603504becee7d001f0a4b[]

			response0.MatchesExample(@"PUT index_4/_settings
			{
			  ""index.priority"": 1
			}");
		}
	}
}
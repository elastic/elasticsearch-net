using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class TasksPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line79()
		{
			// tag::611a684aab4aa256dd9eb873f8b1e450[]
			var response0 = new SearchResponse<object>();
			// end::611a684aab4aa256dd9eb873f8b1e450[]

			response0.MatchesExample(@"GET _cat/tasks/oTUltX4IQMOUUVeiohTt8A:124?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line99()
		{
			// tag::f3422381d36398fcb2612692b11b1e96[]
			var response0 = new SearchResponse<object>();
			// end::f3422381d36398fcb2612692b11b1e96[]

			response0.MatchesExample(@"GET _cat/tasks?v");
		}
	}
}
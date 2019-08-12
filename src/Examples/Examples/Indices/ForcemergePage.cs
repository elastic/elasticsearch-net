using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class ForcemergePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::ca16c1f060ca653ea8fbca445359f78f[]
			var response0 = new SearchResponse<object>();
			// end::ca16c1f060ca653ea8fbca445359f78f[]

			response0.MatchesExample(@"POST /twitter/_forcemerge");
		}

		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::64d97cda667be166f3df49e87e713560[]
			var response0 = new SearchResponse<object>();
			// end::64d97cda667be166f3df49e87e713560[]

			response0.MatchesExample(@"POST /logs-000001/_forcemerge?max_num_segments=1");
		}

		[U(Skip = "Example not implemented")]
		public void Line68()
		{
			// tag::e5ee4be6e45c99c270b2c3fdf1a061ab[]
			var response0 = new SearchResponse<object>();
			// end::e5ee4be6e45c99c270b2c3fdf1a061ab[]

			response0.MatchesExample(@"POST /kimchy/_forcemerge?only_expunge_deletes=false&max_num_segments=100&flush=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::9e6b6b784ba8931563dd04a5922098ba[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9e6b6b784ba8931563dd04a5922098ba[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_forcemerge");

			response1.MatchesExample(@"POST /_forcemerge");
		}
	}
}
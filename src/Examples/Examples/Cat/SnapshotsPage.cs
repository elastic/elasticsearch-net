using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class SnapshotsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::706fc4b9e4df1f6ee3fe34194492c20e[]
			var response0 = new SearchResponse<object>();
			// end::706fc4b9e4df1f6ee3fe34194492c20e[]

			response0.MatchesExample(@"GET /_cat/snapshots/repo1?v&s=id");
		}

		[U(Skip = "Example not implemented")]
		public void Line41()
		{
			// tag::18bd891c5a3d7dfd4dee6a9a9baae825[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::18bd891c5a3d7dfd4dee6a9a9baae825[]

			response0.MatchesExample(@"GET /_cat/snapshots/_all");

			response1.MatchesExample(@"GET /_cat/snapshots/repo1,repo2");

			response2.MatchesExample(@"GET /_cat/snapshots/repo*");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::9bdd3c0d47e60c8cfafc8109f9369922[]
			var response0 = new SearchResponse<object>();
			// end::9bdd3c0d47e60c8cfafc8109f9369922[]

			response0.MatchesExample(@"GET /twitter/_search?q=tag:wow");
		}

		[U(Skip = "Example not implemented")]
		public void Line340()
		{
			// tag::be49260e1b3496c4feac38c56ebb0669[]
			var response0 = new SearchResponse<object>();
			// end::be49260e1b3496c4feac38c56ebb0669[]

			response0.MatchesExample(@"GET /twitter/_search?q=user:kimchy");
		}

		[U(Skip = "Example not implemented")]
		public void Line386()
		{
			// tag::f5569945024b9d664828693705c27c1a[]
			var response0 = new SearchResponse<object>();
			// end::f5569945024b9d664828693705c27c1a[]

			response0.MatchesExample(@"GET /kimchy,elasticsearch/_search?q=user:kimchy");
		}

		[U(Skip = "Example not implemented")]
		public void Line398()
		{
			// tag::168bfdde773570cfc6dd3ab3574e413b[]
			var response0 = new SearchResponse<object>();
			// end::168bfdde773570cfc6dd3ab3574e413b[]

			response0.MatchesExample(@"GET /_search?q=user:kimchy");
		}

		[U(Skip = "Example not implemented")]
		public void Line407()
		{
			// tag::8022e6a690344035b6472a43a9d122e0[]
			var response0 = new SearchResponse<object>();
			// end::8022e6a690344035b6472a43a9d122e0[]

			response0.MatchesExample(@"GET /_all/_search?q=user:kimchy");
		}

		[U(Skip = "Example not implemented")]
		public void Line413()
		{
			// tag::43682666e1abcb14770c99f02eb26a0d[]
			var response0 = new SearchResponse<object>();
			// end::43682666e1abcb14770c99f02eb26a0d[]

			response0.MatchesExample(@"GET /*/_search?q=user:kimchy");
		}
	}
}
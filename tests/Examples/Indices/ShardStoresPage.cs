using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class ShardStoresPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::45a023986499436a9153eef87788ab82[]
			var response0 = new SearchResponse<object>();
			// end::45a023986499436a9153eef87788ab82[]

			response0.MatchesExample(@"GET /twitter/_shard_stores");
		}

		[U(Skip = "Example not implemented")]
		public void Line101()
		{
			// tag::cd93919e13f656ad2e6629f45c579b93[]
			var response0 = new SearchResponse<object>();
			// end::cd93919e13f656ad2e6629f45c579b93[]

			response0.MatchesExample(@"GET /test/_shard_stores");
		}

		[U(Skip = "Example not implemented")]
		public void Line111()
		{
			// tag::af970eb8b93cdea52209e1256eba9d8c[]
			var response0 = new SearchResponse<object>();
			// end::af970eb8b93cdea52209e1256eba9d8c[]

			response0.MatchesExample(@"GET /test1,test2/_shard_stores");
		}

		[U(Skip = "Example not implemented")]
		public void Line121()
		{
			// tag::00b3b6d76a368ae71277ea24af318693[]
			var response0 = new SearchResponse<object>();
			// end::00b3b6d76a368ae71277ea24af318693[]

			response0.MatchesExample(@"GET /_shard_stores");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::3545261682af72f4bee57f2bac0a9590[]
			var response0 = new SearchResponse<object>();
			// end::3545261682af72f4bee57f2bac0a9590[]

			response0.MatchesExample(@"GET /_shard_stores?status=green");
		}
	}
}
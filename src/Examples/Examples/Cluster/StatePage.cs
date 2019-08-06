using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class StatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line22()
		{
			// tag::66642ac5f672c35e087b5e2d7c02bd06[]
			var response0 = new SearchResponse<object>();
			// end::66642ac5f672c35e087b5e2d7c02bd06[]

			response0.MatchesExample(@"GET /_cluster/state");
		}

		[U(Skip = "Example not implemented")]
		public void Line50()
		{
			// tag::8c9017146434ad2ff4531dc8cde64c57[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::8c9017146434ad2ff4531dc8cde64c57[]

			response0.MatchesExample(@"GET /_cluster/state/{metrics}");

			response1.MatchesExample(@"GET /_cluster/state/{metrics}/{indices}");
		}

		[U(Skip = "Example not implemented")]
		public void Line87()
		{
			// tag::b66be1daf6c220eb66d94e708b2fae39[]
			var response0 = new SearchResponse<object>();
			// end::b66be1daf6c220eb66d94e708b2fae39[]

			response0.MatchesExample(@"GET /_cluster/state/metadata,routing_table/foo,bar");
		}

		[U(Skip = "Example not implemented")]
		public void Line95()
		{
			// tag::0fa220ee3fb267020382f74aa70eb1e9[]
			var response0 = new SearchResponse<object>();
			// end::0fa220ee3fb267020382f74aa70eb1e9[]

			response0.MatchesExample(@"GET /_cluster/state/_all/foo,bar");
		}

		[U(Skip = "Example not implemented")]
		public void Line103()
		{
			// tag::a3cfd350c73a104b99a998c6be931408[]
			var response0 = new SearchResponse<object>();
			// end::a3cfd350c73a104b99a998c6be931408[]

			response0.MatchesExample(@"GET /_cluster/state/blocks");
		}
	}
}
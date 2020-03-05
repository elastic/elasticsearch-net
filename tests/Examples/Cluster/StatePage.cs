using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class StatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/state.asciidoc:133")]
		public void Line133()
		{
			// tag::b66be1daf6c220eb66d94e708b2fae39[]
			var response0 = new SearchResponse<object>();
			// end::b66be1daf6c220eb66d94e708b2fae39[]

			response0.MatchesExample(@"GET /_cluster/state/metadata,routing_table/foo,bar");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/state.asciidoc:140")]
		public void Line140()
		{
			// tag::0fa220ee3fb267020382f74aa70eb1e9[]
			var response0 = new SearchResponse<object>();
			// end::0fa220ee3fb267020382f74aa70eb1e9[]

			response0.MatchesExample(@"GET /_cluster/state/_all/foo,bar");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/state.asciidoc:147")]
		public void Line147()
		{
			// tag::a3cfd350c73a104b99a998c6be931408[]
			var response0 = new SearchResponse<object>();
			// end::a3cfd350c73a104b99a998c6be931408[]

			response0.MatchesExample(@"GET /_cluster/state/blocks");
		}
	}
}
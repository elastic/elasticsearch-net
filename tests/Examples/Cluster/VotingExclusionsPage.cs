using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class VotingExclusionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/voting-exclusions.asciidoc:81")]
		public void Line81()
		{
			// tag::f6ead39c5505045543b9225deca7367d[]
			var response0 = new SearchResponse<object>();
			// end::f6ead39c5505045543b9225deca7367d[]

			response0.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=nodeName1,nodeName2");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/voting-exclusions.asciidoc:88")]
		public void Line88()
		{
			// tag::25cb9e1da00dfd971065ce182467434d[]
			var response0 = new SearchResponse<object>();
			// end::25cb9e1da00dfd971065ce182467434d[]

			response0.MatchesExample(@"DELETE /_cluster/voting_config_exclusions");
		}
	}
}
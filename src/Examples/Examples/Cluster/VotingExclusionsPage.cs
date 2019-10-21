using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class VotingExclusionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line73()
		{
			// tag::59681840e544bb5b3bd858c194972f23[]
			var response0 = new SearchResponse<object>();
			// end::59681840e544bb5b3bd858c194972f23[]

			response0.MatchesExample(@"POST /_cluster/voting_config_exclusions/nodeId1");
		}

		[U(Skip = "Example not implemented")]
		public void Line82()
		{
			// tag::25cb9e1da00dfd971065ce182467434d[]
			var response0 = new SearchResponse<object>();
			// end::25cb9e1da00dfd971065ce182467434d[]

			response0.MatchesExample(@"DELETE /_cluster/voting_config_exclusions");
		}
	}
}
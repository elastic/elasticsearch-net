using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules.Discovery
{
	public class VotingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line31()
		{
			// tag::1605be45a5711d1929d6ad2d1ae0f797[]
			var response0 = new SearchResponse<object>();
			// end::1605be45a5711d1929d6ad2d1ae0f797[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.cluster_coordination.last_committed_config");
		}
	}
}
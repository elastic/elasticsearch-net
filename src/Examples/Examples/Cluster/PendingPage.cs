using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class PendingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::aa814309ad5f1630886ba75255b444f5[]
			var response0 = new SearchResponse<object>();
			// end::aa814309ad5f1630886ba75255b444f5[]

			response0.MatchesExample(@"GET /_cluster/pending_tasks");
		}
	}
}
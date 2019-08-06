using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class RemoteInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::cc0cca5556ec6224c7134c233734beed[]
			var response0 = new SearchResponse<object>();
			// end::cc0cca5556ec6224c7134c233734beed[]

			response0.MatchesExample(@"GET /_remote/info");
		}
	}
}
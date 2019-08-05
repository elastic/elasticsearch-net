using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Migration.Migrate80
{
	public class SnapshotsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line20()
		{
			// tag::6458a2377155ecbdd2d3ebd0e1529201[]
			var response0 = new SearchResponse<object>();
			// end::6458a2377155ecbdd2d3ebd0e1529201[]

			response0.MatchesExample(@"GET _snapshot/repo1/snap1");
		}
	}
}
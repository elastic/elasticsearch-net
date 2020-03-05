using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Migration.Migrate80
{
	public class SnapshotsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("migration/migrate_8_0/snapshots.asciidoc:20")]
		public void Line20()
		{
			// tag::6458a2377155ecbdd2d3ebd0e1529201[]
			var response0 = new SearchResponse<object>();
			// end::6458a2377155ecbdd2d3ebd0e1529201[]

			response0.MatchesExample(@"GET _snapshot/repo1/snap1");
		}
	}
}
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class GetSnapshotApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-api.asciidoc:28")]
		public void Line28()
		{
			// tag::a811b82ba4632bdd9065829085188bc9[]
			var response0 = new SearchResponse<object>();
			// end::a811b82ba4632bdd9065829085188bc9[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/my_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-api.asciidoc:194")]
		public void Line194()
		{
			// tag::bfb8a15cd05b43094ffbce8078bad3e1[]
			var response0 = new SearchResponse<object>();
			// end::bfb8a15cd05b43094ffbce8078bad3e1[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/snapshot_2");
		}
	}
}
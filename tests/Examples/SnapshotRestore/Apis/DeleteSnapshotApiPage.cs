using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class DeleteSnapshotApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-snapshot-api.asciidoc:30")]
		public void Line30()
		{
			// tag::f04e1284d09ceb4443d67b2ef9c7f476[]
			var response0 = new SearchResponse<object>();
			// end::f04e1284d09ceb4443d67b2ef9c7f476[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository/my_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-snapshot-api.asciidoc:69")]
		public void Line69()
		{
			// tag::6dd4c02fe3d6b800648a04d3e2d29fc1[]
			var response0 = new SearchResponse<object>();
			// end::6dd4c02fe3d6b800648a04d3e2d29fc1[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository/snapshot_2,snapshot_3");
		}
	}
}
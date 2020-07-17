using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class GetSnapshotStatusApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-status-api.asciidoc:75")]
		public void Line75()
		{
			// tag::fac241cb0060e68abe899062676fc782[]
			var response0 = new SearchResponse<object>();
			// end::fac241cb0060e68abe899062676fc782[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/my_snapshot/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-snapshot-status-api.asciidoc:309")]
		public void Line309()
		{
			// tag::a72ba4b73cfc2562c4a4f212b46fb646[]
			var response0 = new SearchResponse<object>();
			// end::a72ba4b73cfc2562c4a4f212b46fb646[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/snapshot_2/_status");
		}
	}
}
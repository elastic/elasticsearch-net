using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class TakeSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:27")]
		public void Line27()
		{
			// tag::2ab78817eacb5030a447e7fac6b91591[]
			var response0 = new SearchResponse<object>();
			// end::2ab78817eacb5030a447e7fac6b91591[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_1?wait_for_completion=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:40")]
		public void Line40()
		{
			// tag::4a0353692bb14c5fccdc97903af0aa13[]
			var response0 = new SearchResponse<object>();
			// end::4a0353692bb14c5fccdc97903af0aa13[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_2?wait_for_completion=true
			{
			  ""indices"": ""index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": false,
			  ""metadata"": {
			    ""taken_by"": ""kimchy"",
			    ""taken_because"": ""backup before upgrading""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:72")]
		public void Line72()
		{
			// tag::7eb0303e39243fbb9bf51a99270cd022[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::7eb0303e39243fbb9bf51a99270cd022[]

			response0.MatchesExample(@"# PUT /_snapshot/my_backup/<snapshot-{now/d}>");

			response1.MatchesExample(@"PUT /_snapshot/my_backup/%3Csnapshot-%7Bnow%2Fd%7D%3E");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:102")]
		public void Line102()
		{
			// tag::020c56e520ff6556ebfaf98efaef56aa[]
			var response0 = new SearchResponse<object>();
			// end::020c56e520ff6556ebfaf98efaef56aa[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:139")]
		public void Line139()
		{
			// tag::0b77ebfb06c63ccbad857b39bb4ff851[]
			var response0 = new SearchResponse<object>();
			// end::0b77ebfb06c63ccbad857b39bb4ff851[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_*,some_other_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:147")]
		public void Line147()
		{
			// tag::fb224f0ae2a03567b6d9b165e7dd24b6[]
			var response0 = new SearchResponse<object>();
			// end::fb224f0ae2a03567b6d9b165e7dd24b6[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:167")]
		public void Line167()
		{
			// tag::677fdf84ac97bb107207b6966143144b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::677fdf84ac97bb107207b6966143144b[]

			response0.MatchesExample(@"GET /_snapshot/_all");

			response1.MatchesExample(@"GET /_snapshot/my_backup,my_fs_backup");

			response2.MatchesExample(@"GET /_snapshot/my*/snap*");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:177")]
		public void Line177()
		{
			// tag::155c438e215890cdcb4879eaaadf4046[]
			var response0 = new SearchResponse<object>();
			// end::155c438e215890cdcb4879eaaadf4046[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_current");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:185")]
		public void Line185()
		{
			// tag::0784fbe88299be4f02eaa86368e93203[]
			var response0 = new SearchResponse<object>();
			// end::0784fbe88299be4f02eaa86368e93203[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_2");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:199")]
		public void Line199()
		{
			// tag::2b8d2065be3002b0be26598d6ad803a6[]
			var response0 = new SearchResponse<object>();
			// end::2b8d2065be3002b0be26598d6ad803a6[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup");
		}
	}
}
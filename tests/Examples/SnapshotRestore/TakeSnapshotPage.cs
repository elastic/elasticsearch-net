// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class TakeSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:23")]
		public void Line23()
		{
			// tag::2ab78817eacb5030a447e7fac6b91591[]
			var response0 = new SearchResponse<object>();
			// end::2ab78817eacb5030a447e7fac6b91591[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_1?wait_for_completion=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:36")]
		public void Line36()
		{
			// tag::6a4f30129fb3f02296ab98fd8e180f07[]
			var response0 = new SearchResponse<object>();
			// end::6a4f30129fb3f02296ab98fd8e180f07[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_2?wait_for_completion=true
			{
			  ""indices"": ""data_stream_1,index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": false,
			  ""metadata"": {
			    ""taken_by"": ""kimchy"",
			    ""taken_because"": ""backup before upgrading""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:88")]
		public void Line88()
		{
			// tag::7eb0303e39243fbb9bf51a99270cd022[]
			var response0 = new SearchResponse<object>();
			// end::7eb0303e39243fbb9bf51a99270cd022[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/%3Csnapshot-%7Bnow%2Fd%7D%3E");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:118")]
		public void Line118()
		{
			// tag::020c56e520ff6556ebfaf98efaef56aa[]
			var response0 = new SearchResponse<object>();
			// end::020c56e520ff6556ebfaf98efaef56aa[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:149")]
		public void Line149()
		{
			// tag::0b77ebfb06c63ccbad857b39bb4ff851[]
			var response0 = new SearchResponse<object>();
			// end::0b77ebfb06c63ccbad857b39bb4ff851[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_*,some_other_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:157")]
		public void Line157()
		{
			// tag::fb224f0ae2a03567b6d9b165e7dd24b6[]
			var response0 = new SearchResponse<object>();
			// end::fb224f0ae2a03567b6d9b165e7dd24b6[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:177")]
		public void Line177()
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
		[Description("snapshot-restore/take-snapshot.asciidoc:187")]
		public void Line187()
		{
			// tag::155c438e215890cdcb4879eaaadf4046[]
			var response0 = new SearchResponse<object>();
			// end::155c438e215890cdcb4879eaaadf4046[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_current");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:195")]
		public void Line195()
		{
			// tag::0784fbe88299be4f02eaa86368e93203[]
			var response0 = new SearchResponse<object>();
			// end::0784fbe88299be4f02eaa86368e93203[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_2");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/take-snapshot.asciidoc:209")]
		public void Line209()
		{
			// tag::88151217a2f66fc25ccdc26805e75be1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::88151217a2f66fc25ccdc26805e75be1[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/my_backup,my_fs_backup");

			response1.MatchesExample(@"DELETE /_snapshot/my_backup/snap*");
		}
	}
}

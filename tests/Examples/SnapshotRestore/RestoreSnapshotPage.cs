// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class RestoreSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:28")]
		public void Line28()
		{
			// tag::853ca73db9b596cc4ddda66b3ec8faa2[]
			var response0 = new SearchResponse<object>();
			// end::853ca73db9b596cc4ddda66b3ec8faa2[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:45")]
		public void Line45()
		{
			// tag::47dcf95e3d398b9bdcb0a483f705bb4b[]
			var response0 = new SearchResponse<object>();
			// end::47dcf95e3d398b9bdcb0a483f705bb4b[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore
			{
			  ""indices"": ""index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": true,
			  ""rename_pattern"": ""index_(.+)"",
			  ""rename_replacement"": ""restored_index_$1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:83")]
		public void Line83()
		{
			// tag::922df55507c66533fcadc850eecabcff[]
			var response0 = new SearchResponse<object>();
			// end::922df55507c66533fcadc850eecabcff[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore
			{
			  ""indices"": ""index_1"",
			  ""ignore_unavailable"": true,
			  ""index_settings"": {
			    ""index.number_of_replicas"": 0
			  },
			  ""ignore_index_settings"": [
			    ""index.refresh_interval""
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:128")]
		public void Line128()
		{
			// tag::1ae301364751c376b3d26581a36d8975[]
			var response0 = new SearchResponse<object>();
			// end::1ae301364751c376b3d26581a36d8975[]

			response0.MatchesExample(@"GET /_snapshot/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:137")]
		public void Line137()
		{
			// tag::db1913b97109b86cfc5efc7cfcd65d93[]
			var response0 = new SearchResponse<object>();
			// end::db1913b97109b86cfc5efc7cfcd65d93[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:146")]
		public void Line146()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:202")]
		public void Line202()
		{
			// tag::2432f86346177533cabdabbd4eb41b30[]
			var response0 = new SearchResponse<object>();
			// end::2432f86346177533cabdabbd4eb41b30[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1,snapshot_2/_status");
		}
	}
}
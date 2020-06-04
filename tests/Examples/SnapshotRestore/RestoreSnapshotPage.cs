// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
			// tag::cc42a6a42101025923c206b4249fea9e[]
			var response0 = new SearchResponse<object>();
			// end::cc42a6a42101025923c206b4249fea9e[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore
			{
			  ""indices"": ""index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": false,              <1>
			  ""rename_pattern"": ""index_(.+)"",
			  ""rename_replacement"": ""restored_index_$1"",
			  ""include_aliases"": false
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:88")]
		public void Line88()
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
		[Description("snapshot-restore/restore-snapshot.asciidoc:133")]
		public void Line133()
		{
			// tag::1ae301364751c376b3d26581a36d8975[]
			var response0 = new SearchResponse<object>();
			// end::1ae301364751c376b3d26581a36d8975[]

			response0.MatchesExample(@"GET /_snapshot/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:142")]
		public void Line142()
		{
			// tag::db1913b97109b86cfc5efc7cfcd65d93[]
			var response0 = new SearchResponse<object>();
			// end::db1913b97109b86cfc5efc7cfcd65d93[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:151")]
		public void Line151()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:207")]
		public void Line207()
		{
			// tag::2432f86346177533cabdabbd4eb41b30[]
			var response0 = new SearchResponse<object>();
			// end::2432f86346177533cabdabbd4eb41b30[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1,snapshot_2/_status");
		}
	}
}

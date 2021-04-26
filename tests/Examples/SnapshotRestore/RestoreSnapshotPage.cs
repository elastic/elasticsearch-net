/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class RestoreSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:24")]
		public void Line24()
		{
			// tag::853ca73db9b596cc4ddda66b3ec8faa2[]
			var response0 = new SearchResponse<object>();
			// end::853ca73db9b596cc4ddda66b3ec8faa2[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:74")]
		public void Line74()
		{
			// tag::d43ecb0a1c938eeafb1a5dd8ecd4fdda[]
			var response0 = new SearchResponse<object>();
			// end::d43ecb0a1c938eeafb1a5dd8ecd4fdda[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore
			{
			  ""indices"": ""data_stream_1,index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": false,              <1>
			  ""rename_pattern"": ""index_(.+)"",
			  ""rename_replacement"": ""restored_index_$1"",
			  ""include_aliases"": false
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:129")]
		public void Line129()
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
		[Description("snapshot-restore/restore-snapshot.asciidoc:189")]
		public void Line189()
		{
			// tag::1ae301364751c376b3d26581a36d8975[]
			var response0 = new SearchResponse<object>();
			// end::1ae301364751c376b3d26581a36d8975[]

			response0.MatchesExample(@"GET /_snapshot/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:198")]
		public void Line198()
		{
			// tag::db1913b97109b86cfc5efc7cfcd65d93[]
			var response0 = new SearchResponse<object>();
			// end::db1913b97109b86cfc5efc7cfcd65d93[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:207")]
		public void Line207()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/restore-snapshot.asciidoc:263")]
		public void Line263()
		{
			// tag::2432f86346177533cabdabbd4eb41b30[]
			var response0 = new SearchResponse<object>();
			// end::2432f86346177533cabdabbd4eb41b30[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1,snapshot_2/_status");
		}
	}
}

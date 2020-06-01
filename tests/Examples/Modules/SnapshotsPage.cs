// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules
{
	public class SnapshotsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line98()
		{
			// tag::92b3749a473cf2e7ff4055316662a4fe[]
			var response0 = new SearchResponse<object>();
			// end::92b3749a473cf2e7ff4055316662a4fe[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_backup_location""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line112()
		{
			// tag::ff930e6409b6a923ef1c9e7fc99f24cc[]
			var response0 = new SearchResponse<object>();
			// end::ff930e6409b6a923ef1c9e7fc99f24cc[]

			response0.MatchesExample(@"GET /_snapshot/my_backup");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::b9e4f7a80d21c85f88f578219df8e192[]
			var response0 = new SearchResponse<object>();
			// end::b9e4f7a80d21c85f88f578219df8e192[]

			response0.MatchesExample(@"GET /_snapshot/repo*,*backup*");
		}

		[U(Skip = "Example not implemented")]
		public void Line145()
		{
			// tag::0d754b0d8d13c6d39ea353978dfe5992[]
			var response0 = new SearchResponse<object>();
			// end::0d754b0d8d13c6d39ea353978dfe5992[]

			response0.MatchesExample(@"GET /_snapshot");
		}

		[U(Skip = "Example not implemented")]
		public void Line152()
		{
			// tag::37432cda12eb63ce59d186b55233c6e1[]
			var response0 = new SearchResponse<object>();
			// end::37432cda12eb63ce59d186b55233c6e1[]

			response0.MatchesExample(@"GET /_snapshot/_all");
		}

		[U(Skip = "Example not implemented")]
		public void Line184()
		{
			// tag::44b410249d477c640c127bfc7320e365[]
			var response0 = new SearchResponse<object>();
			// end::44b410249d477c640c127bfc7320e365[]

			response0.MatchesExample(@"PUT /_snapshot/my_fs_backup
			{
			    ""type"": ""fs"",
			    ""settings"": {
			        ""location"": ""/mount/backups/my_fs_backup_location"",
			        ""compress"": true
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line200()
		{
			// tag::8988215f3a4fc4b7a7ef4a9c5be3391e[]
			var response0 = new SearchResponse<object>();
			// end::8988215f3a4fc4b7a7ef4a9c5be3391e[]

			response0.MatchesExample(@"PUT /_snapshot/my_fs_backup
			{
			    ""type"": ""fs"",
			    ""settings"": {
			        ""location"": ""my_fs_backup_location"",
			        ""compress"": true
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line277()
		{
			// tag::98ee9bfa32b64ca22e4338544b36c370[]
			var response0 = new SearchResponse<object>();
			// end::98ee9bfa32b64ca22e4338544b36c370[]

			response0.MatchesExample(@"PUT _snapshot/my_src_only_repository
			{
			  ""type"": ""source"",
			  ""settings"": {
			    ""delegate_type"": ""fs"",
			    ""location"": ""my_backup_location""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line306()
		{
			// tag::f1a7cf532da3a8f9a52a401a90e3a998[]
			var response0 = new SearchResponse<object>();
			// end::f1a7cf532da3a8f9a52a401a90e3a998[]

			response0.MatchesExample(@"PUT /_snapshot/my_unverified_backup?verify=false
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_unverified_backup_location""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line320()
		{
			// tag::337cd2c3f9e11665f00786705037f86c[]
			var response0 = new SearchResponse<object>();
			// end::337cd2c3f9e11665f00786705037f86c[]

			response0.MatchesExample(@"POST /_snapshot/my_unverified_backup/_verify");
		}

		[U(Skip = "Example not implemented")]
		public void Line336()
		{
			// tag::6aca241c0361d26f134712821e2d09a9[]
			var response0 = new SearchResponse<object>();
			// end::6aca241c0361d26f134712821e2d09a9[]

			response0.MatchesExample(@"POST /_snapshot/my_repository/_cleanup");
		}

		[U(Skip = "Example not implemented")]
		public void Line370()
		{
			// tag::2ab78817eacb5030a447e7fac6b91591[]
			var response0 = new SearchResponse<object>();
			// end::2ab78817eacb5030a447e7fac6b91591[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_1?wait_for_completion=true");
		}

		[U(Skip = "Example not implemented")]
		public void Line384()
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
		public void Line446()
		{
			// tag::020c56e520ff6556ebfaf98efaef56aa[]
			var response0 = new SearchResponse<object>();
			// end::020c56e520ff6556ebfaf98efaef56aa[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1");
		}

		[U(Skip = "Example not implemented")]
		public void Line483()
		{
			// tag::0b77ebfb06c63ccbad857b39bb4ff851[]
			var response0 = new SearchResponse<object>();
			// end::0b77ebfb06c63ccbad857b39bb4ff851[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_*,some_other_snapshot");
		}

		[U(Skip = "Example not implemented")]
		public void Line491()
		{
			// tag::fb224f0ae2a03567b6d9b165e7dd24b6[]
			var response0 = new SearchResponse<object>();
			// end::fb224f0ae2a03567b6d9b165e7dd24b6[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_all");
		}

		[U(Skip = "Example not implemented")]
		public void Line511()
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
		public void Line521()
		{
			// tag::155c438e215890cdcb4879eaaadf4046[]
			var response0 = new SearchResponse<object>();
			// end::155c438e215890cdcb4879eaaadf4046[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_current");
		}

		[U(Skip = "Example not implemented")]
		public void Line529()
		{
			// tag::0784fbe88299be4f02eaa86368e93203[]
			var response0 = new SearchResponse<object>();
			// end::0784fbe88299be4f02eaa86368e93203[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_2");
		}

		[U(Skip = "Example not implemented")]
		public void Line543()
		{
			// tag::2b8d2065be3002b0be26598d6ad803a6[]
			var response0 = new SearchResponse<object>();
			// end::2b8d2065be3002b0be26598d6ad803a6[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup");
		}

		[U(Skip = "Example not implemented")]
		public void Line558()
		{
			// tag::853ca73db9b596cc4ddda66b3ec8faa2[]
			var response0 = new SearchResponse<object>();
			// end::853ca73db9b596cc4ddda66b3ec8faa2[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore");
		}

		[U(Skip = "Example not implemented")]
		public void Line576()
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
		public void Line614()
		{
			// tag::79ecb7594b3e55df3e28149beff222f6[]
			var response0 = new SearchResponse<object>();
			// end::79ecb7594b3e55df3e28149beff222f6[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore
			{
			  ""indices"": ""index_1"",
			  ""index_settings"": {
			    ""index.number_of_replicas"": 0
			  },
			  ""ignore_index_settings"": [
			    ""index.refresh_interval""
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line658()
		{
			// tag::1ae301364751c376b3d26581a36d8975[]
			var response0 = new SearchResponse<object>();
			// end::1ae301364751c376b3d26581a36d8975[]

			response0.MatchesExample(@"GET /_snapshot/_status");
		}

		[U(Skip = "Example not implemented")]
		public void Line667()
		{
			// tag::db1913b97109b86cfc5efc7cfcd65d93[]
			var response0 = new SearchResponse<object>();
			// end::db1913b97109b86cfc5efc7cfcd65d93[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_status");
		}

		[U(Skip = "Example not implemented")]
		public void Line676()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U(Skip = "Example not implemented")]
		public void Line731()
		{
			// tag::2432f86346177533cabdabbd4eb41b30[]
			var response0 = new SearchResponse<object>();
			// end::2432f86346177533cabdabbd4eb41b30[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1,snapshot_2/_status");
		}

		[U(Skip = "Example not implemented")]
		public void Line788()
		{
			// tag::86c723fc6212d34166661e7dac223491[]
			var response0 = new SearchResponse<object>();
			// end::86c723fc6212d34166661e7dac223491[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_1");
		}
	}
}

using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules
{
	public class SnapshotsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line92()
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

		[U]
		[SkipExample]
		public void Line107()
		{
			// tag::ff930e6409b6a923ef1c9e7fc99f24cc[]
			var response0 = new SearchResponse<object>();
			// end::ff930e6409b6a923ef1c9e7fc99f24cc[]

			response0.MatchesExample(@"GET /_snapshot/my_backup");
		}

		[U]
		[SkipExample]
		public void Line134()
		{
			// tag::b9e4f7a80d21c85f88f578219df8e192[]
			var response0 = new SearchResponse<object>();
			// end::b9e4f7a80d21c85f88f578219df8e192[]

			response0.MatchesExample(@"GET /_snapshot/repo*,*backup*");
		}

		[U]
		[SkipExample]
		public void Line143()
		{
			// tag::0d754b0d8d13c6d39ea353978dfe5992[]
			var response0 = new SearchResponse<object>();
			// end::0d754b0d8d13c6d39ea353978dfe5992[]

			response0.MatchesExample(@"GET /_snapshot");
		}

		[U]
		[SkipExample]
		public void Line151()
		{
			// tag::37432cda12eb63ce59d186b55233c6e1[]
			var response0 = new SearchResponse<object>();
			// end::37432cda12eb63ce59d186b55233c6e1[]

			response0.MatchesExample(@"GET /_snapshot/_all");
		}

		[U]
		[SkipExample]
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

		[U]
		[SkipExample]
		public void Line201()
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

		[U]
		[SkipExample]
		public void Line279()
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

		[U]
		[SkipExample]
		public void Line309()
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

		[U]
		[SkipExample]
		public void Line324()
		{
			// tag::337cd2c3f9e11665f00786705037f86c[]
			var response0 = new SearchResponse<object>();
			// end::337cd2c3f9e11665f00786705037f86c[]

			response0.MatchesExample(@"POST /_snapshot/my_unverified_backup/_verify");
		}

		[U]
		[SkipExample]
		public void Line341()
		{
			// tag::2ab78817eacb5030a447e7fac6b91591[]
			var response0 = new SearchResponse<object>();
			// end::2ab78817eacb5030a447e7fac6b91591[]

			response0.MatchesExample(@"PUT /_snapshot/my_backup/snapshot_1?wait_for_completion=true");
		}

		[U]
		[SkipExample]
		public void Line356()
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

		[U]
		[SkipExample]
		public void Line388()
		{
			// tag::7eb0303e39243fbb9bf51a99270cd022[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::7eb0303e39243fbb9bf51a99270cd022[]

			response0.MatchesExample(@"# PUT /_snapshot/my_backup/<snapshot-{now/d}>");

			response1.MatchesExample(@"PUT /_snapshot/my_backup/%3Csnapshot-%7Bnow%2Fd%7D%3E");
		}

		[U]
		[SkipExample]
		public void Line419()
		{
			// tag::020c56e520ff6556ebfaf98efaef56aa[]
			var response0 = new SearchResponse<object>();
			// end::020c56e520ff6556ebfaf98efaef56aa[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1");
		}

		[U]
		[SkipExample]
		public void Line457()
		{
			// tag::0b77ebfb06c63ccbad857b39bb4ff851[]
			var response0 = new SearchResponse<object>();
			// end::0b77ebfb06c63ccbad857b39bb4ff851[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_*,some_other_snapshot");
		}

		[U]
		[SkipExample]
		public void Line466()
		{
			// tag::fb224f0ae2a03567b6d9b165e7dd24b6[]
			var response0 = new SearchResponse<object>();
			// end::fb224f0ae2a03567b6d9b165e7dd24b6[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_all");
		}

		[U]
		[SkipExample]
		public void Line486()
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

		[U]
		[SkipExample]
		public void Line497()
		{
			// tag::155c438e215890cdcb4879eaaadf4046[]
			var response0 = new SearchResponse<object>();
			// end::155c438e215890cdcb4879eaaadf4046[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_current");
		}

		[U]
		[SkipExample]
		public void Line506()
		{
			// tag::0784fbe88299be4f02eaa86368e93203[]
			var response0 = new SearchResponse<object>();
			// end::0784fbe88299be4f02eaa86368e93203[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_2");
		}

		[U]
		[SkipExample]
		public void Line521()
		{
			// tag::2b8d2065be3002b0be26598d6ad803a6[]
			var response0 = new SearchResponse<object>();
			// end::2b8d2065be3002b0be26598d6ad803a6[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup");
		}

		[U]
		[SkipExample]
		public void Line537()
		{
			// tag::853ca73db9b596cc4ddda66b3ec8faa2[]
			var response0 = new SearchResponse<object>();
			// end::853ca73db9b596cc4ddda66b3ec8faa2[]

			response0.MatchesExample(@"POST /_snapshot/my_backup/snapshot_1/_restore");
		}

		[U]
		[SkipExample]
		public void Line556()
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

		[U]
		[SkipExample]
		public void Line595()
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

		[U]
		[SkipExample]
		public void Line640()
		{
			// tag::1ae301364751c376b3d26581a36d8975[]
			var response0 = new SearchResponse<object>();
			// end::1ae301364751c376b3d26581a36d8975[]

			response0.MatchesExample(@"GET /_snapshot/_status");
		}

		[U]
		[SkipExample]
		public void Line650()
		{
			// tag::db1913b97109b86cfc5efc7cfcd65d93[]
			var response0 = new SearchResponse<object>();
			// end::db1913b97109b86cfc5efc7cfcd65d93[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/_status");
		}

		[U]
		[SkipExample]
		public void Line660()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U]
		[SkipExample]
		public void Line717()
		{
			// tag::2432f86346177533cabdabbd4eb41b30[]
			var response0 = new SearchResponse<object>();
			// end::2432f86346177533cabdabbd4eb41b30[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1,snapshot_2/_status");
		}

		[U]
		[SkipExample]
		public void Line777()
		{
			// tag::86c723fc6212d34166661e7dac223491[]
			var response0 = new SearchResponse<object>();
			// end::86c723fc6212d34166661e7dac223491[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_1");
		}
	}
}
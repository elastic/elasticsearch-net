// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class RegisterRepositoryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:24")]
		public void Line24()
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
		[Description("snapshot-restore/register-repository.asciidoc:38")]
		public void Line38()
		{
			// tag::ff930e6409b6a923ef1c9e7fc99f24cc[]
			var response0 = new SearchResponse<object>();
			// end::ff930e6409b6a923ef1c9e7fc99f24cc[]

			response0.MatchesExample(@"GET /_snapshot/my_backup");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:63")]
		public void Line63()
		{
			// tag::b9e4f7a80d21c85f88f578219df8e192[]
			var response0 = new SearchResponse<object>();
			// end::b9e4f7a80d21c85f88f578219df8e192[]

			response0.MatchesExample(@"GET /_snapshot/repo*,*backup*");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:71")]
		public void Line71()
		{
			// tag::0d754b0d8d13c6d39ea353978dfe5992[]
			var response0 = new SearchResponse<object>();
			// end::0d754b0d8d13c6d39ea353978dfe5992[]

			response0.MatchesExample(@"GET /_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:78")]
		public void Line78()
		{
			// tag::37432cda12eb63ce59d186b55233c6e1[]
			var response0 = new SearchResponse<object>();
			// end::37432cda12eb63ce59d186b55233c6e1[]

			response0.MatchesExample(@"GET /_snapshot/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:111")]
		public void Line111()
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
		[Description("snapshot-restore/register-repository.asciidoc:127")]
		public void Line127()
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
		[Description("snapshot-restore/register-repository.asciidoc:205")]
		public void Line205()
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
		[Description("snapshot-restore/register-repository.asciidoc:236")]
		public void Line236()
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
		[Description("snapshot-restore/register-repository.asciidoc:250")]
		public void Line250()
		{
			// tag::337cd2c3f9e11665f00786705037f86c[]
			var response0 = new SearchResponse<object>();
			// end::337cd2c3f9e11665f00786705037f86c[]

			response0.MatchesExample(@"POST /_snapshot/my_unverified_backup/_verify");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/register-repository.asciidoc:267")]
		public void Line267()
		{
			// tag::6aca241c0361d26f134712821e2d09a9[]
			var response0 = new SearchResponse<object>();
			// end::6aca241c0361d26f134712821e2d09a9[]

			response0.MatchesExample(@"POST /_snapshot/my_repository/_cleanup");
		}
	}
}

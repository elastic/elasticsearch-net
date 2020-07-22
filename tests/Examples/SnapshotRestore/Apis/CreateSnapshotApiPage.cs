using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class CreateSnapshotApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/create-snapshot-api.asciidoc:25")]
		public void Line25()
		{
			// tag::1f3dd84ab11bae09d3f99b1b3536e239[]
			var response0 = new SearchResponse<object>();
			// end::1f3dd84ab11bae09d3f99b1b3536e239[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository/my_snapshot");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/create-snapshot-api.asciidoc:134")]
		public void Line134()
		{
			// tag::3f30310cc6d0adae6b0f61705624a695[]
			var response0 = new SearchResponse<object>();
			// end::3f30310cc6d0adae6b0f61705624a695[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository/snapshot_2?wait_for_completion=true
			{
			  ""indices"": ""index_1,index_2"",
			  ""ignore_unavailable"": true,
			  ""include_global_state"": false,
			  ""metadata"": {
			    ""taken_by"": ""user123"",
			    ""taken_because"": ""backup before upgrading""
			  }
			}");
		}
	}
}
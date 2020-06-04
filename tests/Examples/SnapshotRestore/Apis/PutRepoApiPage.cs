using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class PutRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/put-repo-api.asciidoc:10")]
		public void Line10()
		{
			// tag::89b72dd7f747f6297c2b089e8bc807be[]
			var response0 = new SearchResponse<object>();
			// end::89b72dd7f747f6297c2b089e8bc807be[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_backup_location""
			  }
			}");
		}
	}
}
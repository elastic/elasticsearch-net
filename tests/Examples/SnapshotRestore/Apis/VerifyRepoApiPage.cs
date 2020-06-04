using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class VerifyRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/verify-repo-api.asciidoc:24")]
		public void Line24()
		{
			// tag::3fe5e6c0d5ea4586aa04f989ae54b72e[]
			var response0 = new SearchResponse<object>();
			// end::3fe5e6c0d5ea4586aa04f989ae54b72e[]

			response0.MatchesExample(@"POST /_snapshot/my_repository/_verify");
		}
	}
}
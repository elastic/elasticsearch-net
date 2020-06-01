using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class CleanUpRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/clean-up-repo-api.asciidoc:25")]
		public void Line25()
		{
			// tag::6aca241c0361d26f134712821e2d09a9[]
			var response0 = new SearchResponse<object>();
			// end::6aca241c0361d26f134712821e2d09a9[]

			response0.MatchesExample(@"POST /_snapshot/my_repository/_cleanup");
		}
	}
}
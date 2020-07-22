using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmSearchableSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-searchable-snapshot.asciidoc:36")]
		public void Line36()
		{
			// tag::3f2e5132e35b9e8b3203a4a0541cf0d4[]
			var response0 = new SearchResponse<object>();
			// end::3f2e5132e35b9e8b3203a4a0541cf0d4[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""cold"": {
			        ""actions"": {
			          ""searchable_snapshot"" : {
			            ""snapshot_repository"" : ""backing_repo""
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
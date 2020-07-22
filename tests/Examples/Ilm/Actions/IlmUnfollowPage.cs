using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmUnfollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-unfollow.asciidoc:54")]
		public void Line54()
		{
			// tag::a5a58e8ad66afe831bc295500e3e8739[]
			var response0 = new SearchResponse<object>();
			// end::a5a58e8ad66afe831bc295500e3e8739[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""unfollow"" : {}
			        }
			      }
			    }
			  }
			}");
		}
	}
}
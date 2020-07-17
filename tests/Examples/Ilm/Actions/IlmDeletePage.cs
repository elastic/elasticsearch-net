using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmDeletePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-delete.asciidoc:25")]
		public void Line25()
		{
			// tag::053497b6960f80fd7b005b7c6d54358f[]
			var response0 = new SearchResponse<object>();
			// end::053497b6960f80fd7b005b7c6d54358f[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""delete"": {
			        ""actions"": {
			          ""delete"" : { }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
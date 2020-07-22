using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmFreezePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-freeze.asciidoc:23")]
		public void Line23()
		{
			// tag::0345fbd95c4516a89ac5ad261a16be8f[]
			var response0 = new SearchResponse<object>();
			// end::0345fbd95c4516a89ac5ad261a16be8f[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""cold"": {
			        ""actions"": {
			          ""freeze"" : { }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
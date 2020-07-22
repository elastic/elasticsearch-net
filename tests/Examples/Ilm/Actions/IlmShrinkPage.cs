using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ilm.Actions
{
	public class IlmShrinkPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/actions/ilm-shrink.asciidoc:49")]
		public void Line49()
		{
			// tag::f3b4ddce8ff21fc1a76a7c0d9c36650e[]
			var response0 = new SearchResponse<object>();
			// end::f3b4ddce8ff21fc1a76a7c0d9c36650e[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""shrink"" : {
			            ""number_of_shards"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}
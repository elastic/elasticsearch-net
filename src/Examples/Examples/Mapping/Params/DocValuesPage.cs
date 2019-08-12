using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class DocValuesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::4e75503583efc222045e0be4430a2863[]
			var response0 = new SearchResponse<object>();
			// end::4e75503583efc222045e0be4430a2863[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""status_code"": { \<1>
			        ""type"":       ""keyword""
			      },
			      ""session_id"": { \<2>
			        ""type"":       ""keyword"",
			        ""doc_values"": false
			      }
			    }
			  }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class AnalysisPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line41()
		{
			// tag::7ffee3c2a5581994fc0ea59dd106d39f[]
			var response0 = new SearchResponse<object>();
			// end::7ffee3c2a5581994fc0ea59dd106d39f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"":     ""text"",
			        ""analyzer"": ""standard""
			      }
			    }
			  }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class MetaPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::bb49cbbeef6afe2dae0db46c4a10df3b[]
			var response0 = new SearchResponse<object>();
			// end::bb49cbbeef6afe2dae0db46c4a10df3b[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""latency"": {
			        ""type"": ""long"",
			        ""meta"": {
			          ""unit"": ""ms""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
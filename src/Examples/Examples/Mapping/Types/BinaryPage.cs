using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class BinaryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::9296dd085f411739f5b0ec80eb9b9e27[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9296dd085f411739f5b0ec80eb9b9e27[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": {
			        ""type"": ""text""
			      },
			      ""blob"": {
			        ""type"": ""binary""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""name"": ""Some binary blob"",
			  ""blob"": ""U29tZSBiaW5hcnkgYmxvYg=="" \<1>
			}");
		}
	}
}
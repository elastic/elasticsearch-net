using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class NumericPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line22()
		{
			// tag::a71c438cc4df1cafe3109ccff475afdb[]
			var response0 = new SearchResponse<object>();
			// end::a71c438cc4df1cafe3109ccff475afdb[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_of_bytes"": {
			        ""type"": ""integer""
			      },
			      ""time_in_seconds"": {
			        ""type"": ""float""
			      },
			      ""price"": {
			        ""type"": ""scaled_float"",
			        ""scaling_factor"": 100
			      }
			    }
			  }
			}");
		}
	}
}
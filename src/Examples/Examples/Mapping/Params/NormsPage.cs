using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class NormsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line21()
		{
			// tag::f9d15004aba50331c595837c4546aeef[]
			var response0 = new SearchResponse<object>();
			// end::f9d15004aba50331c595837c4546aeef[]

			response0.MatchesExample(@"PUT my_index/_mapping
			{
			  ""properties"": {
			    ""title"": {
			      ""type"": ""text"",
			      ""norms"": false
			    }
			  }
			}");
		}
	}
}
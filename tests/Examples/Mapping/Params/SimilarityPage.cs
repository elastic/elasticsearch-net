using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class SimilarityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::e6e31dcdd1ca214c17e375c54069d513[]
			var response0 = new SearchResponse<object>();
			// end::e6e31dcdd1ca214c17e375c54069d513[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""default_field"": { \<1>
			        ""type"": ""text""
			      },
			      ""boolean_sim_field"": {
			        ""type"": ""text"",
			        ""similarity"": ""boolean"" \<2>
			      }
			    }
			  }
			}");
		}
	}
}
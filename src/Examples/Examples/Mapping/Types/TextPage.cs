using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class TextPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line22()
		{
			// tag::24ea1c6cdf10165228951e562b7ec0ef[]
			var response0 = new SearchResponse<object>();
			// end::24ea1c6cdf10165228951e562b7ec0ef[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"":  ""text""
			      }
			    }
			  }
			}");
		}
	}
}
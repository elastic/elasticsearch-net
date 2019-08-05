using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class IdFieldPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line12()
		{
			// tag::3abdbdc99e203e87332d387cfbdeafaa[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::3abdbdc99e203e87332d387cfbdeafaa[]

			response0.MatchesExample(@"# Example documents");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"": ""Document with ID 1""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2&refresh=true
			{
			  ""text"": ""Document with ID 2""
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""terms"": {
			      ""_id"": [ ""1"", ""2"" ] \<1>
			    }
			  }
			}");
		}
	}
}
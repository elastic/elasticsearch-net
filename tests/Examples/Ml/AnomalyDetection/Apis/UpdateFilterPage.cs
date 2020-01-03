using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateFilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line48()
		{
			// tag::4d21725453955582ff12b4a1104aa7b6[]
			var response0 = new SearchResponse<object>();
			// end::4d21725453955582ff12b4a1104aa7b6[]

			response0.MatchesExample(@"POST _ml/filters/safe_domains/_update
			{
			  ""description"": ""Updated list of domains"",
			  ""add_items"": [""*.myorg.com""],
			  ""remove_items"": [""wikipedia.org""]
			}");
		}
	}
}
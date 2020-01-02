using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line89()
		{
			// tag::d7b3862eb61595fc02d64d5f6ed60c88[]
			var response0 = new SearchResponse<object>();
			// end::d7b3862eb61595fc02d64d5f6ed60c88[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-total-requests");
		}
	}
}
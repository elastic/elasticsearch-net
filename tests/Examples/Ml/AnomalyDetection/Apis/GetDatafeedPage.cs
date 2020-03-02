using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::4fa9ee04188cbf0b38cfc28f6a56527d[]
			var response0 = new SearchResponse<object>();
			// end::4fa9ee04188cbf0b38cfc28f6a56527d[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-high_sum_total_sales");
		}
	}
}
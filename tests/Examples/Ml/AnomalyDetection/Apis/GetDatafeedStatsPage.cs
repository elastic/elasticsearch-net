using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetDatafeedStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-datafeed-stats.asciidoc:145")]
		public void Line145()
		{
			// tag::f44d287c6937785eb09b91353c1deb1e[]
			var response0 = new SearchResponse<object>();
			// end::f44d287c6937785eb09b91353c1deb1e[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-high_sum_total_sales/_stats");
		}
	}
}
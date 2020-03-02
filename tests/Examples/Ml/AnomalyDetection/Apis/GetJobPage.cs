using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line90()
		{
			// tag::86280dcb49aa89083be4b2644daf1b7c[]
			var response0 = new SearchResponse<object>();
			// end::86280dcb49aa89083be4b2644daf1b7c[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/high_sum_total_sales");
		}
	}
}
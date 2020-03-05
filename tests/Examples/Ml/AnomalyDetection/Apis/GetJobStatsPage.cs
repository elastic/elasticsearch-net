using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line328()
		{
			// tag::9298aaf8232a819e79b3bf8471245e98[]
			var response0 = new SearchResponse<object>();
			// end::9298aaf8232a819e79b3bf8471245e98[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/_stats");
		}
	}
}
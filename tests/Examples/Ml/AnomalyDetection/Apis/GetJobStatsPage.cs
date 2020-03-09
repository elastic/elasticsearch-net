using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-job-stats.asciidoc:328")]
		public void Line333()
		{
			// tag::9298aaf8232a819e79b3bf8471245e98[]
			var response0 = new SearchResponse<object>();
			// end::9298aaf8232a819e79b3bf8471245e98[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/_stats");
		}
	}
}
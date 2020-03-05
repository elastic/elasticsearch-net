using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class FlushJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/flush-job.asciidoc:71")]
		public void Line71()
		{
			// tag::02520ac7816b2c4cf8fb413fd16122f2[]
			var response0 = new SearchResponse<object>();
			// end::02520ac7816b2c4cf8fb413fd16122f2[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_flush
			{
			  ""calc_interim"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/flush-job.asciidoc:98")]
		public void Line98()
		{
			// tag::3033133e8675524fd8f969db0625b62e[]
			var response0 = new SearchResponse<object>();
			// end::3033133e8675524fd8f969db0625b62e[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_flush
			{
			  ""advance_time"": ""1514804400""
			}");
		}
	}
}
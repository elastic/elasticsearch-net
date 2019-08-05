using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class FlushJobPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line72()
		{
			// tag::6a9931992ce1b0c2c2c82635d32f32cd[]
			var response0 = new SearchResponse<object>();
			// end::6a9931992ce1b0c2c2c82635d32f32cd[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_flush
			{
			  ""calc_interim"": true
			}");
		}

		[U]
		[SkipExample]
		public void Line99()
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
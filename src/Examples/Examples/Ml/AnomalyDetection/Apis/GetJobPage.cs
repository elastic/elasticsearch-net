using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line86()
		{
			// tag::29278b6d24dd544da186019301dd4f40[]
			var response0 = new SearchResponse<object>();
			// end::29278b6d24dd544da186019301dd4f40[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/total-requests");
		}
	}
}
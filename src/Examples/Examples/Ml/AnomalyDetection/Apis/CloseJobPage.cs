using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class CloseJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line102()
		{
			// tag::cf6e928ae9efc2b9f59aa1ccb4605bee[]
			var response0 = new SearchResponse<object>();
			// end::cf6e928ae9efc2b9f59aa1ccb4605bee[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_close");
		}
	}
}
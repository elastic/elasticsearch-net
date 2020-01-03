using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::3ac8b5234e9d53859245cf8ab0094ca5[]
			var response0 = new SearchResponse<object>();
			// end::3ac8b5234e9d53859245cf8ab0094ca5[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests");
		}

		[U(Skip = "Example not implemented")]
		public void Line79()
		{
			// tag::ccec66fb20d5ede6c691e0890cfe402a[]
			var response0 = new SearchResponse<object>();
			// end::ccec66fb20d5ede6c691e0890cfe402a[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests?wait_for_completion=false");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class CloseJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line92()
		{
			// tag::75957a7d1b67e3d47899c5f18b32cb61[]
			var response0 = new SearchResponse<object>();
			// end::75957a7d1b67e3d47899c5f18b32cb61[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_close");
		}
	}
}
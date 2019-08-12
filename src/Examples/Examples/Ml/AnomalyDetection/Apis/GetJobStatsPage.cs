using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::e3d706e32f9bd1496072beb46e4c488e[]
			var response0 = new SearchResponse<object>();
			// end::e3d706e32f9bd1496072beb46e4c488e[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/farequote/_stats");
		}
	}
}
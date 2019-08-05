using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteForecastPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line72()
		{
			// tag::eb4e43b47867b54214a8630172dd0e21[]
			var response0 = new SearchResponse<object>();
			// end::eb4e43b47867b54214a8630172dd0e21[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests/_forecast/_all");
		}
	}
}
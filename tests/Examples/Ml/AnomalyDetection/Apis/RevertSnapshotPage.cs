using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class RevertSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::b173b1b5bab610668ab74d5b2ab03f78[]
			var response0 = new SearchResponse<object>();
			// end::b173b1b5bab610668ab74d5b2ab03f78[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/high_sum_total_sales/model_snapshots/1575402237/_revert
			{
			  ""delete_intervening_results"": true
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class RevertSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line63()
		{
			// tag::f28c58902bdefd9e2b36001e8d682d35[]
			var response0 = new SearchResponse<object>();
			// end::f28c58902bdefd9e2b36001e8d682d35[]

			response0.MatchesExample(@"POST
			_ml/anomaly_detectors/it_ops_new_kpi/model_snapshots/1491856080/_revert
			{
			  ""delete_intervening_results"": true
			}");
		}
	}
}
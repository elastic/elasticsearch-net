using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateSnapshotPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line54()
		{
			// tag::3b9c54604535d97e8368d47148aecc6f[]
			var response0 = new SearchResponse<object>();
			// end::3b9c54604535d97e8368d47148aecc6f[]

			response0.MatchesExample(@"POST
			_ml/anomaly_detectors/it_ops_new_logs/model_snapshots/1491852978/_update
			{
			  ""description"": ""Snapshot 1"",
			  ""retain"": true
			}");
		}
	}
}
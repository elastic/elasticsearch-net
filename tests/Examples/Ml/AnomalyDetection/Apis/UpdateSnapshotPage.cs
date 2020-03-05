using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-snapshot.asciidoc:52")]
		public void Line52()
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
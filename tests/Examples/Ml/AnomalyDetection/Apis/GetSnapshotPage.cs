using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-snapshot.asciidoc:193")]
		public void Line193()
		{
			// tag::c873f9cd093e26515148f052e28c7805[]
			var response0 = new SearchResponse<object>();
			// end::c873f9cd093e26515148f052e28c7805[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/high_sum_total_sales/model_snapshots
			{
			  ""start"": ""1575402236000""
			}");
		}
	}
}
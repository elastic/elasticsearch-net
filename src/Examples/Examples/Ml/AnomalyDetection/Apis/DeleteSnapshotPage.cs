using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteSnapshotPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line45()
		{
			// tag::1e08e054c761353f99211cd18e8ca47b[]
			var response0 = new SearchResponse<object>();
			// end::1e08e054c761353f99211cd18e8ca47b[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/farequote/model_snapshots/1491948163");
		}
	}
}
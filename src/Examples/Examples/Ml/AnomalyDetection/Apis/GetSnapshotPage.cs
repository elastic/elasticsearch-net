using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetSnapshotPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line70()
		{
			// tag::51393160fedca39ea733488044d06f8e[]
			var response0 = new SearchResponse<object>();
			// end::51393160fedca39ea733488044d06f8e[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/farequote/model_snapshots
			{
			  ""start"": ""1491852977000""
			}");
		}
	}
}
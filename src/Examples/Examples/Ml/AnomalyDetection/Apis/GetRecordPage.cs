using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetRecordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line76()
		{
			// tag::16337a7169486ffea5bfe185b6426b9c[]
			var response0 = new SearchResponse<object>();
			// end::16337a7169486ffea5bfe185b6426b9c[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/it-ops-kpi/results/records
			{
			  ""sort"": ""record_score"",
			  ""desc"": true,
			  ""start"": ""1454944100000""
			}");
		}
	}
}
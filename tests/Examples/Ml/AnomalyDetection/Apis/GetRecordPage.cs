using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetRecordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line202()
		{
			// tag::20e3b181114e00c943a27a9bbcf85f15[]
			var response0 = new SearchResponse<object>();
			// end::20e3b181114e00c943a27a9bbcf85f15[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/results/records
			{
			  ""sort"": ""record_score"",
			  ""desc"": true,
			  ""start"": ""1454944100000""
			}");
		}
	}
}
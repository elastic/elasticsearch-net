using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class StopDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line74()
		{
			// tag::fd60b4092c6552164862cec287359676[]
			var response0 = new SearchResponse<object>();
			// end::fd60b4092c6552164862cec287359676[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-low_request_rate/_stop
			{
			  ""timeout"": ""30s""
			}");
		}
	}
}
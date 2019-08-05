using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutDatafeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line112()
		{
			// tag::23067c5e8da958fa4d914f3b5c9bf607[]
			var response0 = new SearchResponse<object>();
			// end::23067c5e8da958fa4d914f3b5c9bf607[]

			response0.MatchesExample(@"PUT _ml/datafeeds/datafeed-total-requests
			{
			  ""job_id"": ""total-requests"",
			  ""indices"": [""server-metrics""]
			}");
		}
	}
}
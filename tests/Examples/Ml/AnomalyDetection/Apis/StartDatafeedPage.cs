using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class StartDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line99()
		{
			// tag::d7ae456f119246e95f2f4c37e7544b8c[]
			var response0 = new SearchResponse<object>();
			// end::d7ae456f119246e95f2f4c37e7544b8c[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-low_request_rate/_start
			{
			  ""start"": ""2019-04-07T18:22:16Z""
			}");
		}
	}
}
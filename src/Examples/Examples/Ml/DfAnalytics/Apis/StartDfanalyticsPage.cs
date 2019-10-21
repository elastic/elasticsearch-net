using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class StartDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::1a3a4b8a4bfee4ab84ddd13d8835f560[]
			var response0 = new SearchResponse<object>();
			// end::1a3a4b8a4bfee4ab84ddd13d8835f560[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/loganalytics/_start");
		}
	}
}
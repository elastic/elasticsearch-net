using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetDfanalyticsStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line85()
		{
			// tag::cfc52956b005d57111c49dfe1735634e[]
			var response0 = new SearchResponse<object>();
			// end::cfc52956b005d57111c49dfe1735634e[]

			response0.MatchesExample(@"GET _ml/data_frame/analytics/loganalytics/_stats");
		}
	}
}
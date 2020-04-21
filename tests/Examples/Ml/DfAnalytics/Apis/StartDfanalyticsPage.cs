using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class StartDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/start-dfanalytics.asciidoc:73")]
		public void Line73()
		{
			// tag::1a3a4b8a4bfee4ab84ddd13d8835f560[]
			var response0 = new SearchResponse<object>();
			// end::1a3a4b8a4bfee4ab84ddd13d8835f560[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/loganalytics/_start");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class StopDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line76()
		{
			// tag::db19cc7a26ca80106d86d688f4be67a8[]
			var response0 = new SearchResponse<object>();
			// end::db19cc7a26ca80106d86d688f4be67a8[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/loganalytics/_stop");
		}
	}
}
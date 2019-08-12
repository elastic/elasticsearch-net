using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DeleteDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::1c8b6768c4eefc76fcb38708152f561b[]
			var response0 = new SearchResponse<object>();
			// end::1c8b6768c4eefc76fcb38708152f561b[]

			response0.MatchesExample(@"DELETE _ml/data_frame/analytics/loganalytics");
		}
	}
}
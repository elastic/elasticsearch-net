using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class PutDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::80877b0ab3babd4f623becbe73c447fb[]
			var response0 = new SearchResponse<object>();
			// end::80877b0ab3babd4f623becbe73c447fb[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/loganalytics
			{
			  ""source"": {
			    ""index"": ""logdata""
			  },
			  ""dest"": {
			    ""index"": ""logdata_out""
			  },
			  ""analysis"": {
			    ""outlier_detection"": {
			    }
			  }
			}");
		}
	}
}
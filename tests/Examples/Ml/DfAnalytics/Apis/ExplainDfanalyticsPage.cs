using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class ExplainDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line88()
		{
			// tag::1e75b0e71294527b43015cc333f9d9f7[]
			var response0 = new SearchResponse<object>();
			// end::1e75b0e71294527b43015cc333f9d9f7[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/_explain
			{
			  ""data_frame_analytics_config"": {
			    ""source"": {
			      ""index"": ""houses_sold_last_10_yrs""
			    },
			    ""analysis"": {
			      ""regression"": {
			        ""dependent_variable"": ""price""
			      }
			    }
			  }
			}");
		}
	}
}
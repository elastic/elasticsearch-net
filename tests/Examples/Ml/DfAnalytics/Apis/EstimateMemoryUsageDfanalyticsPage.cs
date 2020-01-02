using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EstimateMemoryUsageDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::2749068ab33cac0ec113d479bc282368[]
			var response0 = new SearchResponse<object>();
			// end::2749068ab33cac0ec113d479bc282368[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/_estimate_memory_usage
			{
			  ""data_frame_analytics_config"": {
			    ""source"": {
			      ""index"": ""logdata""
			    },
			    ""analysis"": {
			      ""outlier_detection"": {}
			    }
			  }
			}");
		}
	}
}
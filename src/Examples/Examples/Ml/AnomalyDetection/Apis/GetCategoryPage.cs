using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCategoryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line70()
		{
			// tag::e8f1c9ee003d115ec8f55e57990df6e4[]
			var response0 = new SearchResponse<object>();
			// end::e8f1c9ee003d115ec8f55e57990df6e4[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/esxi_log/results/categories
			{
			  ""page"":{
			    ""size"": 1
			  }
			}");
		}
	}
}
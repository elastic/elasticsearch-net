using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCategoryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-category.asciidoc:110")]
		public void Line110()
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
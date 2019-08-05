using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EvaluateDfanalyticsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line72()
		{
			// tag::eae68412d998bc0f65b09711f007a4b7[]
			var response0 = new SearchResponse<object>();
			// end::eae68412d998bc0f65b09711f007a4b7[]

			response0.MatchesExample(@"POST _ml/data_frame/_evaluate
			{
			  ""index"": ""my_analytics_dest_index"",
			  ""evaluation"": {
			    ""binary_soft_classification"": {
			      ""actual_field"": ""is_outlier"",
			      ""predicted_probability_field"": ""ml.outlier_score""
			    }
			  }
			}");
		}
	}
}
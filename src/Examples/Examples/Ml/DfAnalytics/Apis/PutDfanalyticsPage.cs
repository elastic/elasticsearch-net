using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class PutDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line164()
		{
			// tag::ce3c391c2b1915cfc44a2917bca71d19[]
			var response0 = new SearchResponse<object>();
			// end::ce3c391c2b1915cfc44a2917bca71d19[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/loganalytics
			{
			  ""description"": ""Outlier detection on log data"",
			  ""source"": {
			    ""index"": ""logdata""
			  },
			  ""dest"": {
			    ""index"": ""logdata_out""
			  },
			  ""analysis"": {
			    ""outlier_detection"": {
			      ""compute_feature_influence"": true,
			      ""outlier_fraction"": 0.05,
			      ""standardization_enabled"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line227()
		{
			// tag::e8211247c280a3fbbbdd32850b743b7b[]
			var response0 = new SearchResponse<object>();
			// end::e8211247c280a3fbbbdd32850b743b7b[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/house_price_regression_analysis
			{
			  ""source"": {
			    ""index"": ""houses_sold_last_10_yrs""
			  },
			  ""dest"": {
			    ""index"": ""house_price_predictions""
			  },
			  ""analysis"":
			    {
			      ""regression"": {
			        ""dependent_variable"": ""price""
			      }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line284()
		{
			// tag::c04ad5bba1b5df4cfaf663052ab4f009[]
			var response0 = new SearchResponse<object>();
			// end::c04ad5bba1b5df4cfaf663052ab4f009[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/student_performance_mathematics_0.3
			{
			 ""source"": {
			   ""index"": ""student_performance_mathematics""
			 },
			 ""dest"": {
			   ""index"":""student_performance_mathematics_reg""
			 },
			 ""analysis"":
			   {
			     ""regression"": {
			       ""dependent_variable"": ""G3"",
			       ""training_percent"": 70  <1>
			     }
			   }
			}");
		}
	}
}
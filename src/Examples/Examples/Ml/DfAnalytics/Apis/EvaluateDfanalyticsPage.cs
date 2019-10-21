using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EvaluateDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line81()
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

		[U(Skip = "Example not implemented")]
		public void Line142()
		{
			// tag::e6e7586a81068773d18cca848346b69f[]
			var response0 = new SearchResponse<object>();
			// end::e6e7586a81068773d18cca848346b69f[]

			response0.MatchesExample(@"POST _ml/data_frame/_evaluate
			{
			  ""index"": ""house_price_predictions"", <1>
			  ""query"": {
			      ""bool"": {
			        ""filter"": [
			          { ""term"":  { ""ml.is_training"": false } } <2>
			        ]
			      }
			  },
			  ""evaluation"": {
			    ""regression"": {
			      ""actual_field"": ""price"", <3>
			      ""predicted_field"": ""ml.price_prediction"", <4>
			      ""metrics"": {
			        ""r_squared"": {},
			        ""mean_squared_error"": {}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line179()
		{
			// tag::862efc8d548a9202597c72c7e98a599d[]
			var response0 = new SearchResponse<object>();
			// end::862efc8d548a9202597c72c7e98a599d[]

			response0.MatchesExample(@"POST _ml/data_frame/_evaluate
			{
			  ""index"": ""student_performance_mathematics_reg"",
			  ""query"": {
			    ""term"": {
			      ""ml.is_training"": {
			        ""value"": true <1>
			      }
			    }
			  },
			  ""evaluation"": {
			    ""regression"": {
			      ""actual_field"": ""G3"", <2>
			      ""predicted_field"": ""ml.G3_prediction"", <3>
			      ""metrics"": {
			        ""r_squared"": {},
			        ""mean_squared_error"": {}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line218()
		{
			// tag::051b2682d386d49616b18a5db591afdf[]
			var response0 = new SearchResponse<object>();
			// end::051b2682d386d49616b18a5db591afdf[]

			response0.MatchesExample(@"POST _ml/data_frame/_evaluate
			{
			  ""index"": ""student_performance_mathematics_reg"",
			  ""query"": {
			    ""term"": {
			      ""ml.is_training"": {
			        ""value"": false <1>
			      }
			    }
			  },
			  ""evaluation"": {
			    ""regression"": {
			      ""actual_field"": ""G3"", <2>
			      ""predicted_field"": ""ml.G3_prediction"", <3>
			      ""metrics"": {
			        ""r_squared"": {},
			        ""mean_squared_error"": {}
			      }
			    }
			  }
			}");
		}
	}
}
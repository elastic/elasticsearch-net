// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EvaluateDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:197")]
		public void Line197()
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
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:259")]
		public void Line259()
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
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:296")]
		public void Line296()
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
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:335")]
		public void Line335()
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

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:374")]
		public void Line374()
		{
			// tag::388d3eda4f792d3fce044777739217e6[]
			var response0 = new SearchResponse<object>();
			// end::388d3eda4f792d3fce044777739217e6[]

			response0.MatchesExample(@"POST _ml/data_frame/_evaluate
			{
			   ""index"": ""animal_classification"",
			   ""evaluation"": {
			      ""classification"": { <1>
			         ""actual_field"": ""animal_class"", <2>
			         ""predicted_field"": ""ml.animal_class_prediction"", <3>
			         ""metrics"": {
			           ""multiclass_confusion_matrix"" : {} <4>
			         }
			      }
			   }
			}");
		}
	}
}
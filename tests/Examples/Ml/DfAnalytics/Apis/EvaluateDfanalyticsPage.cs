// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EvaluateDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:205")]
		public void Line205()
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
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:267")]
		public void Line267()
		{
			// tag::2ecdc5393ac2a240e8287b50dde9dbd9[]
			var response0 = new SearchResponse<object>();
			// end::2ecdc5393ac2a240e8287b50dde9dbd9[]

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
			        ""mse"": {}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:304")]
		public void Line304()
		{
			// tag::19c9b45dbd9d289997fc41a296fc28c5[]
			var response0 = new SearchResponse<object>();
			// end::19c9b45dbd9d289997fc41a296fc28c5[]

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
			        ""mse"": {}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:343")]
		public void Line343()
		{
			// tag::ca677dce1c1bf08fc1bee1de6ac54502[]
			var response0 = new SearchResponse<object>();
			// end::ca677dce1c1bf08fc1bee1de6ac54502[]

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
			        ""mse"": {}
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/evaluate-dfanalytics.asciidoc:382")]
		public void Line382()
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

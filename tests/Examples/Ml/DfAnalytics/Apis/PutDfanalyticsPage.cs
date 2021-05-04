// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class PutDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/put-dfanalytics.asciidoc:399")]
		public void Line399()
		{
			// tag::8c6f3bb8abae9ff1d21e776f16ad1c86[]
			var response0 = new SearchResponse<object>();
			// end::8c6f3bb8abae9ff1d21e776f16ad1c86[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/model-flight-delays-pre
			{
			  ""source"": {
			    ""index"": [
			      ""kibana_sample_data_flights"" <1>
			    ],
			    ""query"": { <2>
			      ""range"": {
			        ""DistanceKilometers"": {
			          ""gt"": 0
			        }
			      }
			    },
			    ""_source"": { <3>
			      ""includes"": [],
			      ""excludes"": [
			        ""FlightDelay"",
			        ""FlightDelayType""
			      ]
			    }
			  },
			  ""dest"": { <4>
			    ""index"": ""df-flight-delays"",
			    ""results_field"": ""ml-results""
			  },
			  ""analysis"": {
			  ""regression"": {
			    ""dependent_variable"": ""FlightDelayMin"",
			    ""training_percent"": 90
			    }
			  },
			  ""analyzed_fields"": { <5>
			    ""includes"": [],
			    ""excludes"": [
			      ""FlightNum""
			    ]
			  },
			  ""model_memory_limit"": ""100mb""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/put-dfanalytics.asciidoc:469")]
		public void Line469()
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
		[Description("ml/df-analytics/apis/put-dfanalytics.asciidoc:533")]
		public void Line533()
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
		[Description("ml/df-analytics/apis/put-dfanalytics.asciidoc:590")]
		public void Line590()
		{
			// tag::ae82eb17c23cb8e5761cb6240a5ed0a6[]
			var response0 = new SearchResponse<object>();
			// end::ae82eb17c23cb8e5761cb6240a5ed0a6[]

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
			       ""training_percent"": 70,  <1>
			       ""randomize_seed"": 19673948271  <2>
			     }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/put-dfanalytics.asciidoc:622")]
		public void Line622()
		{
			// tag::4fb0629146ca78b85e823edd405497bb[]
			var response0 = new SearchResponse<object>();
			// end::4fb0629146ca78b85e823edd405497bb[]

			response0.MatchesExample(@"PUT _ml/data_frame/analytics/loan_classification
			{
			  ""source"" : {
			    ""index"": ""loan-applicants""
			  },
			  ""dest"" : {
			    ""index"": ""loan-applicants-classified""
			  },
			  ""analysis"" : {
			    ""classification"": {
			      ""dependent_variable"": ""label"",
			      ""training_percent"": 75,
			      ""num_top_classes"": 2
			    }
			  }
			}");
		}
	}
}

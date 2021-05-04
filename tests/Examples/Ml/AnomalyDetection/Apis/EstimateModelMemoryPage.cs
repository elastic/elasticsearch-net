// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class EstimateModelMemoryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/estimate-model-memory.asciidoc:60")]
		public void Line60()
		{
			// tag::c4178795c108b4ed3daec4e69aa2acc6[]
			var response0 = new SearchResponse<object>();
			// end::c4178795c108b4ed3daec4e69aa2acc6[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/_estimate_model_memory
			{
			    ""analysis_config"": {
			        ""bucket_span"": ""5m"",
			        ""detectors"": [
			          {
			            ""function"": ""sum"",
			            ""field_name"": ""bytes"",
			            ""by_field_name"": ""status"",
			            ""partition_field_name"": ""app""
			          }
			        ],
			        ""influencers"": [ ""source_ip"", ""dest_ip"" ]
			    },
			    ""overall_cardinality"": {
			       ""status"": 10,
			       ""app"": 50
			    },
			    ""max_bucket_cardinality"": {
			       ""source_ip"": 300,
			       ""dest_ip"": 30
			    }
			}");
		}
	}
}

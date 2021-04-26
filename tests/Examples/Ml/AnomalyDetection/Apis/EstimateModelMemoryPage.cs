/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

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
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/put-job.asciidoc:298")]
		public void Line298()
		{
			// tag::9c11e238772d67dbc9d273776de9916c[]
			var response0 = new SearchResponse<object>();
			// end::9c11e238772d67dbc9d273776de9916c[]

			response0.MatchesExample(@"PUT _ml/anomaly_detectors/total-requests
			{
			  ""description"" : ""Total sum of requests"",
			  ""analysis_config"" : {
			    ""bucket_span"":""10m"",
			    ""detectors"": [
			      {
			        ""detector_description"": ""Sum of total"",
			        ""function"": ""sum"",
			        ""field_name"": ""total""
			      }
			    ]
			  },
			  ""data_description"" : {
			    ""time_field"":""timestamp"",
			    ""time_format"": ""epoch_ms""
			  }
			}");
		}
	}
}

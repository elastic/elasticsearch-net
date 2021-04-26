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
	public class FlushJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/flush-job.asciidoc:71")]
		public void Line71()
		{
			// tag::02520ac7816b2c4cf8fb413fd16122f2[]
			var response0 = new SearchResponse<object>();
			// end::02520ac7816b2c4cf8fb413fd16122f2[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_flush
			{
			  ""calc_interim"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/flush-job.asciidoc:98")]
		public void Line98()
		{
			// tag::7ebfb30b3ece855c1b783d9210939469[]
			var response0 = new SearchResponse<object>();
			// end::7ebfb30b3ece855c1b783d9210939469[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_flush
			{
			  ""advance_time"": ""1514804400000""
			}");
		}
	}
}

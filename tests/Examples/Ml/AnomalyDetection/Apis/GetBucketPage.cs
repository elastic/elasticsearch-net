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
	public class GetBucketPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-bucket.asciidoc:178")]
		public void Line178()
		{
			// tag::f96d4614f2fc294339fef325b794355f[]
			var response0 = new SearchResponse<object>();
			// end::f96d4614f2fc294339fef325b794355f[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/results/buckets
			{
			  ""anomaly_score"": 80,
			  ""start"": ""1454530200001""
			}");
		}
	}
}

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
	public class StartDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/start-datafeed.asciidoc:113")]
		public void Line113()
		{
			// tag::d7ae456f119246e95f2f4c37e7544b8c[]
			var response0 = new SearchResponse<object>();
			// end::d7ae456f119246e95f2f4c37e7544b8c[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-low_request_rate/_start
			{
			  ""start"": ""2019-04-07T18:22:16Z""
			}");
		}
	}
}

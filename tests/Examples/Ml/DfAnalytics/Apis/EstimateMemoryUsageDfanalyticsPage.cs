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

namespace Examples.Ml.DfAnalytics.Apis
{
	public class EstimateMemoryUsageDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::2749068ab33cac0ec113d479bc282368[]
			var response0 = new SearchResponse<object>();
			// end::2749068ab33cac0ec113d479bc282368[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/_estimate_memory_usage
			{
			  ""data_frame_analytics_config"": {
			    ""source"": {
			      ""index"": ""logdata""
			    },
			    ""analysis"": {
			      ""outlier_detection"": {}
			    }
			  }
			}");
		}
	}
}

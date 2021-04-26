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

namespace Examples.Ml.DfAnalytics.Apis
{
	public class UpdateDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/update-dfanalytics.asciidoc:100")]
		public void Line100()
		{
			// tag::d19956ce4b751d1909ec2d257c4ecaa4[]
			var response0 = new SearchResponse<object>();
			// end::d19956ce4b751d1909ec2d257c4ecaa4[]

			response0.MatchesExample(@"POST _ml/data_frame/analytics/model-flight-delays/_update
			{
			  ""model_memory_limit"": ""200mb""
			}");
		}
	}
}
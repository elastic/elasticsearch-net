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

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DeleteDfanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/delete-dfanalytics.asciidoc:59")]
		public void Line59()
		{
			// tag::1c8b6768c4eefc76fcb38708152f561b[]
			var response0 = new SearchResponse<object>();
			// end::1c8b6768c4eefc76fcb38708152f561b[]

			response0.MatchesExample(@"DELETE _ml/data_frame/analytics/loganalytics");
		}
	}
}

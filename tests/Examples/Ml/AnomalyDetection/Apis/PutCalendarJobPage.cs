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
	public class PutCalendarJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/put-calendar-job.asciidoc:38")]
		public void Line38()
		{
			// tag::1b2ab75d3c8064fac6ecc63104396c02[]
			var response0 = new SearchResponse<object>();
			// end::1b2ab75d3c8064fac6ecc63104396c02[]

			response0.MatchesExample(@"PUT _ml/calendars/planned-outages/jobs/total-requests");
		}
	}
}

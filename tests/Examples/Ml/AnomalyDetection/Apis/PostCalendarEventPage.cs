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
	public class PostCalendarEventPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/post-calendar-event.asciidoc:64")]
		public void Line64()
		{
			// tag::c067182d385f59ce5952fb9a716fbf05[]
			var response0 = new SearchResponse<object>();
			// end::c067182d385f59ce5952fb9a716fbf05[]

			response0.MatchesExample(@"POST _ml/calendars/planned-outages/events
			{
			  ""events"" : [
			    {""description"": ""event 1"", ""start_time"": 1513641600000, ""end_time"": 1513728000000},
			    {""description"": ""event 2"", ""start_time"": 1513814400000, ""end_time"": 1513900800000},
			    {""description"": ""event 3"", ""start_time"": 1514160000000, ""end_time"": 1514246400000}
			  ]
			}");
		}
	}
}

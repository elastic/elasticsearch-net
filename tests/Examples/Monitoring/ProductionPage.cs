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

namespace Examples.Monitoring
{
	public class ProductionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/production.asciidoc:50")]
		public void Line50()
		{
			// tag::a941fd568f2e20e13df909ab24506073[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a941fd568f2e20e13df909ab24506073[]

			response0.MatchesExample(@"GET _cluster/settings");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.collection.enabled"": false
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("monitoring/production.asciidoc:88")]
		public void Line88()
		{
			// tag::0b47b0bef81b9b5eecfb3775695bd6ad[]
			var response0 = new SearchResponse<object>();
			// end::0b47b0bef81b9b5eecfb3775695bd6ad[]

			response0.MatchesExample(@"POST /_security/user/remote_monitor
			{
			  ""password"" : ""changeme"",
			  ""roles"" : [ ""remote_monitoring_agent""],
			  ""full_name"" : ""Internal Agent For Remote Monitoring""
			}");
		}
	}
}

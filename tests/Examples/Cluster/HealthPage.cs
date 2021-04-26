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
using Elasticsearch.Net;

namespace Examples.Cluster
{
	public class HealthPage : ExampleBase
	{
		[U]
		[Description("cluster/health.asciidoc:36")]
		public void Line36()
		{
			// tag::04f5dd677c777bcb15d7d5fa63275fc8[]
			var healthResponse = client.Cluster.Health(selector: h => h
				.WaitForStatus(WaitForStatus.Yellow)
				.Timeout("50s")
			);
			// end::04f5dd677c777bcb15d7d5fa63275fc8[]

			healthResponse.MatchesExample(@"GET /_cluster/health?wait_for_status=yellow&timeout=50s");
		}

		[U]
		[Description("cluster/health.asciidoc:150")]
		public void Line150()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var healthResponse = client.Cluster.Health();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			healthResponse.MatchesExample(@"GET _cluster/health");
		}

		[U]
		[Description("cluster/health.asciidoc:186")]
		public void Line186()
		{
			// tag::c48264ec5d9b9679fddd72e5c44425b9[]
			var healthResponse = client.Cluster.Health("twitter", h => h
				.Level(Level.Shards)
			);
			// end::c48264ec5d9b9679fddd72e5c44425b9[]

			healthResponse.MatchesExample(@"GET /_cluster/health/twitter?level=shards");
		}
	}
}

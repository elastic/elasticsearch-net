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

namespace Examples.Cluster
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/stats.asciidoc:1100")]
		public void Line1100()
		{
			// tag::861f5f61409dc87f3671293b87839ff7[]
			var response0 = new SearchResponse<object>();
			// end::861f5f61409dc87f3671293b87839ff7[]

			response0.MatchesExample(@"GET /_cluster/stats?human&pretty");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/stats.asciidoc:1346")]
		public void Line1346()
		{
			// tag::71c629c44bf3c542a0daacbfc253c4b0[]
			var response0 = new SearchResponse<object>();
			// end::71c629c44bf3c542a0daacbfc253c4b0[]

			response0.MatchesExample(@"GET /_cluster/stats/nodes/node1,node*,master:false");
		}
	}
}

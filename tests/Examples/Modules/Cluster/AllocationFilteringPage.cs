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

namespace Examples.Modules.Cluster
{
	public class AllocationFilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/allocation_filtering.asciidoc:22")]
		public void Line22()
		{
			// tag::281ae12918af10b6377ec760eaa844ce[]
			var response0 = new SearchResponse<object>();
			// end::281ae12918af10b6377ec760eaa844ce[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"" : {
			    ""cluster.routing.allocation.exclude._ip"" : ""10.0.0.1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/allocation_filtering.asciidoc:64")]
		public void Line64()
		{
			// tag::07474768b8f9d532b524c15e512736f4[]
			var response0 = new SearchResponse<object>();
			// end::07474768b8f9d532b524c15e512736f4[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"": {
			    ""cluster.routing.allocation.exclude._ip"": ""192.168.2.*""
			  }
			}");
		}
	}
}

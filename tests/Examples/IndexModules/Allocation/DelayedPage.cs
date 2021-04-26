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

namespace Examples.IndexModules.Allocation
{
	public class DelayedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/delayed.asciidoc:40")]
		public void Line40()
		{
			// tag::17e6f3fac556f08a78f7a876e71acb89[]
			var response0 = new SearchResponse<object>();
			// end::17e6f3fac556f08a78f7a876e71acb89[]

			response0.MatchesExample(@"PUT _all/_settings
			{
			  ""settings"": {
			    ""index.unassigned.node_left.delayed_timeout"": ""5m""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/delayed.asciidoc:82")]
		public void Line82()
		{
			// tag::a38f29375eabd0103f8d7c00b17bb0ab[]
			var response0 = new SearchResponse<object>();
			// end::a38f29375eabd0103f8d7c00b17bb0ab[]

			response0.MatchesExample(@"GET _cluster/health \<1>");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/allocation/delayed.asciidoc:95")]
		public void Line95()
		{
			// tag::25d40d3049e57e2bb70c2c5b88bd7b87[]
			var response0 = new SearchResponse<object>();
			// end::25d40d3049e57e2bb70c2c5b88bd7b87[]

			response0.MatchesExample(@"PUT _all/_settings
			{
			  ""settings"": {
			    ""index.unassigned.node_left.delayed_timeout"": ""0""
			  }
			}");
		}
	}
}

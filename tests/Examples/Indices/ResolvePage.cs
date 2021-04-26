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

namespace Examples.Indices
{
	public class ResolvePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/resolve.asciidoc:50")]
		public void Line50()
		{
			// tag::b73ffaecb5532f9ab0136137e899c205[]
			var response0 = new SearchResponse<object>();
			// end::b73ffaecb5532f9ab0136137e899c205[]

			response0.MatchesExample(@"GET /_resolve/index/twitter*");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/resolve.asciidoc:83")]
		public void Line83()
		{
			// tag::bd57976bc93ca64b2d3e001df9f06c82[]
			var response0 = new SearchResponse<object>();
			// end::bd57976bc93ca64b2d3e001df9f06c82[]

			response0.MatchesExample(@"GET /_resolve/index/f*,remoteCluster1:bar*?expand_wildcards=all");
		}
	}
}
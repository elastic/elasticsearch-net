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

namespace Examples.Indices
{
	public class ForcemergePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/forcemerge.asciidoc:12")]
		public void Line12()
		{
			// tag::ca16c1f060ca653ea8fbca445359f78f[]
			var response0 = new SearchResponse<object>();
			// end::ca16c1f060ca653ea8fbca445359f78f[]

			response0.MatchesExample(@"POST /twitter/_forcemerge");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/forcemerge.asciidoc:151")]
		public void Line151()
		{
			// tag::6733f91e27b6d5907d7c58546bc45ca1[]
			var response0 = new SearchResponse<object>();
			// end::6733f91e27b6d5907d7c58546bc45ca1[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_forcemerge");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/forcemerge.asciidoc:161")]
		public void Line161()
		{
			// tag::480e531db799c4c909afd8e2a73a8d0b[]
			var response0 = new SearchResponse<object>();
			// end::480e531db799c4c909afd8e2a73a8d0b[]

			response0.MatchesExample(@"POST /_forcemerge");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/forcemerge.asciidoc:178")]
		public void Line178()
		{
			// tag::25ec4495a2f7bcbffb9cbdc6fb7eb07a[]
			var response0 = new SearchResponse<object>();
			// end::25ec4495a2f7bcbffb9cbdc6fb7eb07a[]

			response0.MatchesExample(@"POST /.ds-logs-000001/_forcemerge?max_num_segments=1");
		}
	}
}

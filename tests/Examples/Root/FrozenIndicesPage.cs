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

namespace Examples.Root
{
	public class FrozenIndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:66")]
		public void Line66()
		{
			// tag::f9018c483fb6b810d8a921668addfc71[]
			var response0 = new SearchResponse<object>();
			// end::f9018c483fb6b810d8a921668addfc71[]

			response0.MatchesExample(@"POST /twitter/_forcemerge?max_num_segments=1");
		}

		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:83")]
		public void Line83()
		{
			// tag::0652fc9f77639fce67a87dc2e33cef51[]
			var response0 = new SearchResponse<object>();
			// end::0652fc9f77639fce67a87dc2e33cef51[]

			response0.MatchesExample(@"GET /twitter/_search?q=user:kimchy&ignore_throttled=false");
		}

		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:97")]
		public void Line97()
		{
			// tag::9ff10591660890ba9d00eb14168c3b67[]
			var response0 = new SearchResponse<object>();
			// end::9ff10591660890ba9d00eb14168c3b67[]

			response0.MatchesExample(@"GET /_cat/indices/twitter?v&h=i,sth");
		}
	}
}

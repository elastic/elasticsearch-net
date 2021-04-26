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

namespace Examples.Search.Request
{
	public class SearchTypePage : ExampleBase
	{
		[U]
		[Description("search/request/search-type.asciidoc:54")]
		public void Line54()
		{
			// tag::de3c9fe00efc5647ad4b695524cbe8a0[]
			var searchResponse = client.Search<object>(s => s
				.Index("twitter")
				.SearchType(SearchType.QueryThenFetch)
			);
			// end::de3c9fe00efc5647ad4b695524cbe8a0[]

			searchResponse.MatchesExample(@"GET twitter/_search?search_type=query_then_fetch");
		}

		[U]
		[Description("search/request/search-type.asciidoc:72")]
		public void Line72()
		{
			// tag::be0d2fbf861842eef2c98d5e5bf6e406[]
			var searchResponse = client.Search<object>(s => s
				.Index("twitter")
				.SearchType(SearchType.DfsQueryThenFetch)
			);
			// end::be0d2fbf861842eef2c98d5e5bf6e406[]

			searchResponse.MatchesExample(@"GET twitter/_search?search_type=dfs_query_then_fetch");
		}
	}
}

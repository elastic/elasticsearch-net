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

namespace Examples.Search
{
	public class SearchShardsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/search-shards.asciidoc:7")]
		public void Line7()
		{
			// tag::49b137a1c0016face219bac3faf41996[]
			var response0 = new SearchResponse<object>();
			// end::49b137a1c0016face219bac3faf41996[]

			response0.MatchesExample(@"GET /twitter/_search_shards");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-shards.asciidoc:144")]
		public void Line144()
		{
			// tag::a44b7da0091ac75e5571475a4e99bb16[]
			var response0 = new SearchResponse<object>();
			// end::a44b7da0091ac75e5571475a4e99bb16[]

			response0.MatchesExample(@"GET /twitter/_search_shards?routing=foo,bar");
		}
	}
}

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

namespace Examples.IndexModules
{
	public class IndexSortingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/index-sorting.asciidoc:16")]
		public void Line16()
		{
			// tag::fea339c85b60ccefa6a163a70b86ca82[]
			var response0 = new SearchResponse<object>();
			// end::fea339c85b60ccefa6a163a70b86ca82[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : ""date"", \<1>
			            ""sort.order"" : ""desc"" \<2>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""date"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/index-sorting.asciidoc:41")]
		public void Line41()
		{
			// tag::a69f1a67cdc141e8dde5abb437c76959[]
			var response0 = new SearchResponse<object>();
			// end::a69f1a67cdc141e8dde5abb437c76959[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : [""username"", ""date""], \<1>
			            ""sort.order"" : [""asc"", ""desc""] \<2>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""username"": {
			                ""type"": ""keyword"",
			                ""doc_values"": true
			            },
			            ""date"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/index-sorting.asciidoc:114")]
		public void Line114()
		{
			// tag::e01a82a0a809a4770ddc84c2cfc1ec85[]
			var response0 = new SearchResponse<object>();
			// end::e01a82a0a809a4770ddc84c2cfc1ec85[]

			response0.MatchesExample(@"PUT events
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : ""timestamp"",
			            ""sort.order"" : ""desc"" \<1>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""timestamp"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/index-sorting.asciidoc:138")]
		public void Line138()
		{
			// tag::46a3694ee4a7bbd4973565e5886782bb[]
			var response0 = new SearchResponse<object>();
			// end::46a3694ee4a7bbd4973565e5886782bb[]

			response0.MatchesExample(@"GET /events/_search
			{
			    ""size"": 10,
			    ""sort"": [
			        { ""timestamp"": ""desc"" }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/index-sorting.asciidoc:159")]
		public void Line159()
		{
			// tag::2e8ba1e0b2a18dd276bbbe64f2b86338[]
			var response0 = new SearchResponse<object>();
			// end::2e8ba1e0b2a18dd276bbbe64f2b86338[]

			response0.MatchesExample(@"GET /events/_search
			{
			    ""size"": 10,
			    ""sort"": [ \<1>
			        { ""timestamp"": ""desc"" }
			    ],
			    ""track_total_hits"": false
			}");
		}
	}
}

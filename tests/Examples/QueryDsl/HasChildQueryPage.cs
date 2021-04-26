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

namespace Examples.QueryDsl
{
	public class HasChildQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-child-query.asciidoc:31")]
		public void Line31()
		{
			// tag::10239a59784c3069e0d9399d3f9a7008[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::10239a59784c3069e0d9399d3f9a7008[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""my-join-field"" : {
			                ""type"" : ""join"",
			                ""relations"": {
			                    ""parent"": ""child""
			                }
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-child-query.asciidoc:53")]
		public void Line53()
		{
			// tag::a204ff3396082b32175371c7ed8b9394[]
			var response0 = new SearchResponse<object>();
			// end::a204ff3396082b32175371c7ed8b9394[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_child"" : {
			            ""type"" : ""child"",
			            ""query"" : {
			                ""match_all"" : {}
			            },
			            ""max_children"": 10,
			            ""min_children"": 2,
			            ""score_mode"" : ""min""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-child-query.asciidoc:141")]
		public void Line141()
		{
			// tag::d7b459941dc32d790ade80a0f5712560[]
			var response0 = new SearchResponse<object>();
			// end::d7b459941dc32d790ade80a0f5712560[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_child"" : {
			            ""type"" : ""child"",
			            ""query"" : {
			                ""function_score"" : {
			                    ""script_score"": {
			                        ""script"": ""_score * doc['click_count'].value""
			                    }
			                }
			            },
			            ""score_mode"" : ""max""
			        }
			    }
			}");
		}
	}
}

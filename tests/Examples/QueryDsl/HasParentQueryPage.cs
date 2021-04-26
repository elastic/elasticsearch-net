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
	public class HasParentQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-parent-query.asciidoc:27")]
		public void Line27()
		{
			// tag::6515e74b150bbdae570e0fd3ef5ac2e5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::6515e74b150bbdae570e0fd3ef5ac2e5[]

			response0.MatchesExample(@"PUT /my-index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""my-join-field"" : {
			                ""type"" : ""join"",
			                ""relations"": {
			                    ""parent"": ""child""
			                }
			            },
			            ""tag"" : {
			                ""type"" : ""keyword""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-parent-query.asciidoc:52")]
		public void Line52()
		{
			// tag::e539bfb5c73771c73acdf22fe77dde04[]
			var response0 = new SearchResponse<object>();
			// end::e539bfb5c73771c73acdf22fe77dde04[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"": {
			        ""has_parent"" : {
			            ""parent_type"" : ""parent"",
			            ""query"" : {
			                ""term"" : {
			                    ""tag"" : {
			                        ""value"" : ""Elasticsearch""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/has-parent-query.asciidoc:122")]
		public void Line122()
		{
			// tag::85d88b08243afbef45d4dcea72c9a41c[]
			var response0 = new SearchResponse<object>();
			// end::85d88b08243afbef45d4dcea72c9a41c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_parent"" : {
			            ""parent_type"" : ""parent"",
			            ""score"" : true,
			            ""query"" : {
			                ""function_score"" : {
			                    ""script_score"": {
			                        ""script"": ""_score * doc['view_count'].value""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

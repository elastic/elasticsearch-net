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
	public class ParentIdQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/parent-id-query.asciidoc:24")]
		public void Line24()
		{
			// tag::0377c031f840e23dcf607a08e5549bac[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0377c031f840e23dcf607a08e5549bac[]

			response0.MatchesExample(@"PUT /my-index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""my-join-field"" : {
			                ""type"" : ""join"",
			                ""relations"": {
			                    ""my-parent"": ""my-child""
			                }
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/parent-id-query.asciidoc:47")]
		public void Line47()
		{
			// tag::6528a67cc20e5a422f11cbc0ffb6f673[]
			var response0 = new SearchResponse<object>();
			// end::6528a67cc20e5a422f11cbc0ffb6f673[]

			response0.MatchesExample(@"PUT /my-index/_doc/1?refresh
			{
			  ""text"": ""This is a parent document."",
			  ""my-join-field"": ""my-parent""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/parent-id-query.asciidoc:60")]
		public void Line60()
		{
			// tag::a4d0e6ff5bb904cbd686aecafa917aa2[]
			var response0 = new SearchResponse<object>();
			// end::a4d0e6ff5bb904cbd686aecafa917aa2[]

			response0.MatchesExample(@"PUT /my-index/_doc/2?routing=1&refresh
			{
			  ""text"": ""This is a child document."",
			  ""my_join_field"": {
			    ""name"": ""my-child"",
			    ""parent"": ""1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/parent-id-query.asciidoc:79")]
		public void Line79()
		{
			// tag::0dd8ee4a383f84f8454c262262630f41[]
			var response0 = new SearchResponse<object>();
			// end::0dd8ee4a383f84f8454c262262630f41[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			  ""query"": {
			      ""parent_id"": {
			          ""type"": ""my-child"",
			          ""id"": ""1""
			      }
			  }
			}");
		}
	}
}

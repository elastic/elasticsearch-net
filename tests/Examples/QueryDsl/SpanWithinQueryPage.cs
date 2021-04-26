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
	public class SpanWithinQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-within-query.asciidoc:11")]
		public void Line11()
		{
			// tag::9429e565d0b56289a10b81220660163c[]
			var response0 = new SearchResponse<object>();
			// end::9429e565d0b56289a10b81220660163c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_within"" : {
			            ""little"" : {
			                ""span_term"" : { ""field1"" : ""foo"" }
			            },
			            ""big"" : {
			                ""span_near"" : {
			                    ""clauses"" : [
			                        { ""span_term"" : { ""field1"" : ""bar"" } },
			                        { ""span_term"" : { ""field1"" : ""baz"" } }
			                    ],
			                    ""slop"" : 5,
			                    ""in_order"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}

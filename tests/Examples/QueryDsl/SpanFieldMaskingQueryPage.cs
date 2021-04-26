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
	public class SpanFieldMaskingQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-field-masking-query.asciidoc:16")]
		public void Line16()
		{
			// tag::b59861ad84352fee3e78bc869ccbe8b0[]
			var response0 = new SearchResponse<object>();
			// end::b59861ad84352fee3e78bc869ccbe8b0[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""span_near"": {
			      ""clauses"": [
			        {
			          ""span_term"": {
			            ""text"": ""quick brown""
			          }
			        },
			        {
			          ""field_masking_span"": {
			            ""query"": {
			              ""span_term"": {
			                ""text.stems"": ""fox""
			              }
			            },
			            ""field"": ""text""
			          }
			        }
			      ],
			      ""slop"": 5,
			      ""in_order"": false
			    }
			  }
			}");
		}
	}
}

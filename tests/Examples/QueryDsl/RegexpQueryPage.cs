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
	public class RegexpQueryPage : ExampleBase
	{
		[U(Skip = "Waiting on PR to implement Rewrite: https://github.com/elastic/elasticsearch-net/issues/4722")]
		[Description("query-dsl/regexp-query.asciidoc:23")]
		public void Line23()
		{
			// tag::618d5f3d35921d8cb7e9ccfbe9a4c3e3[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Regexp(r => r
						.Field("user")
						.Value("k.*y")
						.Flags("ALL")
						.MaximumDeterminizedStates(10_000)
					)
				)
			);
			// end::618d5f3d35921d8cb7e9ccfbe9a4c3e3[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""regexp"": {
			            ""user"": {
			                ""value"": ""k.*y"",
			                ""flags"" : ""ALL"",
			                ""max_determinized_states"": 10000,
			                ""rewrite"": ""constant_score""
			            }
			        }
			    }
			}");
		}
	}
}

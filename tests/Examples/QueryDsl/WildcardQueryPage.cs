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
	public class WildcardQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/wildcard-query.asciidoc:21")]
		public void Line21()
		{
			// tag::d31062ff8c015387889fed4ad86fd914[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Wildcard(w => w
						.Field("user")
						.Value("ki*y")
						.Boost(1)
						.Rewrite(MultiTermQueryRewrite.ConstantScore)
					)
				)
			);
			// end::d31062ff8c015387889fed4ad86fd914[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""wildcard"": {
			            ""user"": {
			                ""value"": ""ki*y"",
			                ""boost"": 1.0,
			                ""rewrite"": ""constant_score""
			            }
			        }
			    }
			}");
		}
	}
}

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
	public class PrefixQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/prefix-query.asciidoc:16")]
		public void Line16()
		{
			// tag::81514791349e0e79ac565160e42889c0[]
			var response0 = new SearchResponse<object>();
			// end::81514791349e0e79ac565160e42889c0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"": {
			            ""user"": {
			                ""value"": ""ki""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/prefix-query.asciidoc:53")]
		public void Line53()
		{
			// tag::32ea547cefa2976c8c3c2eb45a2a4ff4[]
			var response0 = new SearchResponse<object>();
			// end::32ea547cefa2976c8c3c2eb45a2a4ff4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"" : { ""user"" : ""ki"" }
			    }
			}");
		}
	}
}

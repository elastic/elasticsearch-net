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
	public class MatchBoolPrefixQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:13")]
		public void Line13()
		{
			// tag::79c7e8a98c47fad3e96c654d34aa049a[]
			var response0 = new SearchResponse<object>();
			// end::79c7e8a98c47fad3e96c654d34aa049a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"" : ""quick brown f""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:28")]
		public void Line28()
		{
			// tag::effc6b4784aca12691de5d5782c0384b[]
			var response0 = new SearchResponse<object>();
			// end::effc6b4784aca12691de5d5782c0384b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""should"": [
			                { ""term"": { ""message"": ""quick"" }},
			                { ""term"": { ""message"": ""brown"" }},
			                { ""prefix"": { ""message"": ""f""}}
			            ]
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:59")]
		public void Line59()
		{
			// tag::953aab6cbd12a4f034cf02bf34d62a72[]
			var response0 = new SearchResponse<object>();
			// end::953aab6cbd12a4f034cf02bf34d62a72[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"": {
			                ""query"": ""quick brown f"",
			                ""analyzer"": ""keyword""
			            }
			        }
			    }
			}");
		}
	}
}

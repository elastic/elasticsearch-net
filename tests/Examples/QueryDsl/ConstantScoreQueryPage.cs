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
	public class ConstantScoreQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/constant-score-query.asciidoc:12")]
		public void Line12()
		{
			// tag::d59a084640acf2f5c51d3068d38b5fc0[]
			var response0 = new SearchResponse<object>();
			// end::d59a084640acf2f5c51d3068d38b5fc0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""term"" : { ""user"" : ""kimchy""}
			            },
			            ""boost"" : 1.2
			        }
			    }
			}");
		}
	}
}

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
	public class RangeQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/range-query.asciidoc:16")]
		public void Line16()
		{
			// tag::a116949e446f34dc25ae57d4b703d0c1[]
			var response0 = new SearchResponse<object>();
			// end::a116949e446f34dc25ae57d4b703d0c1[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""range"" : {
			            ""age"" : {
			                ""gte"" : 10,
			                ""lte"" : 20,
			                ""boost"" : 2.0
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/range-query.asciidoc:157")]
		public void Line157()
		{
			// tag::67ceac4bf2d9ac7cc500390544cdcb41[]
			var response0 = new SearchResponse<object>();
			// end::67ceac4bf2d9ac7cc500390544cdcb41[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""range"" : {
			            ""timestamp"" : {
			                ""gte"" : ""now-1d/d"",
			                ""lt"" :  ""now/d""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/range-query.asciidoc:219")]
		public void Line219()
		{
			// tag::5c2f486c27bd5346e512265f93375d16[]
			var response0 = new SearchResponse<object>();
			// end::5c2f486c27bd5346e512265f93375d16[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""range"": {
			      ""timestamp"": {
			        ""time_zone"": ""+01:00"",        <1>
			        ""gte"": ""2020-01-01T00:00:00"", <2>
			        ""lte"": ""now""                  <3>
			      }
			    }
			  }
			}");
		}
	}
}

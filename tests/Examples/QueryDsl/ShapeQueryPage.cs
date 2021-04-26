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
	public class ShapeQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/shape-query.asciidoc:28")]
		public void Line28()
		{
			// tag::9208f0823deacf3e3a2cc2b5d78f1e33[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9208f0823deacf3e3a2cc2b5d78f1e33[]

			response0.MatchesExample(@"PUT /example
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /example/_doc/1?refresh=wait_for
			{
			    ""name"": ""Lucky Landing"",
			    ""geometry"": {
			        ""type"": ""point"",
			        ""coordinates"": [1355.400544, 5255.530286]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/shape-query.asciidoc:55")]
		public void Line55()
		{
			// tag::f7bbcd762afef5a562768a46fe192b56[]
			var response0 = new SearchResponse<object>();
			// end::f7bbcd762afef5a562768a46fe192b56[]

			response0.MatchesExample(@"GET /example/_search
			{
			    ""query"":{
			        ""shape"": {
			            ""geometry"": {
			                ""shape"": {
			                    ""type"": ""envelope"",
			                    ""coordinates"" : [[1355.0, 5355.0], [1400.0, 5200.0]]
			                },
			                ""relation"": ""within""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/shape-query.asciidoc:133")]
		public void Line133()
		{
			// tag::86388922e307dd94c0f5f93890f13832[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::86388922e307dd94c0f5f93890f13832[]

			response0.MatchesExample(@"PUT /shapes
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /shapes/_doc/footprint
			{
			    ""geometry"": {
			        ""type"": ""envelope"",
			        ""coordinates"" : [[1355.0, 5355.0], [1400.0, 5200.0]]
			    }
			}");

			response2.MatchesExample(@"GET /example/_search
			{
			    ""query"": {
			        ""shape"": {
			            ""geometry"": {
			                ""indexed_shape"": {
			                    ""index"": ""shapes"",
			                    ""id"": ""footprint"",
			                    ""path"": ""geometry""
			                }
			            }
			        }
			    }
			}");
		}
	}
}

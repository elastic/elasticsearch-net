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
	public class GeoPolygonQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-polygon-query.asciidoc:11")]
		public void Line11()
		{
			// tag::383c5a0771484086dcfd8d990830eeb7[]
			var response0 = new SearchResponse<object>();
			// end::383c5a0771484086dcfd8d990830eeb7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_polygon"" : {
			                    ""person.location"" : {
			                        ""points"" : [
			                            {""lat"" : 40, ""lon"" : -70},
			                            {""lat"" : 30, ""lon"" : -80},
			                            {""lat"" : 20, ""lon"" : -90}
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-polygon-query.asciidoc:60")]
		public void Line60()
		{
			// tag::ecf966a20c54eb4e60a2670f51a99bdc[]
			var response0 = new SearchResponse<object>();
			// end::ecf966a20c54eb4e60a2670f51a99bdc[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_polygon"" : {
			                    ""person.location"" : {
			                        ""points"" : [
			                            [-70, 40],
			                            [-80, 30],
			                            [-90, 20]
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-polygon-query.asciidoc:90")]
		public void Line90()
		{
			// tag::e532955a897ac1844e7c5727916bf32c[]
			var response0 = new SearchResponse<object>();
			// end::e532955a897ac1844e7c5727916bf32c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			               ""geo_polygon"" : {
			                    ""person.location"" : {
			                        ""points"" : [
			                            ""40, -70"",
			                            ""30, -80"",
			                            ""20, -90""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-polygon-query.asciidoc:118")]
		public void Line118()
		{
			// tag::5b809a128ee33be706e2097dde6e7719[]
			var response0 = new SearchResponse<object>();
			// end::5b809a128ee33be706e2097dde6e7719[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			               ""geo_polygon"" : {
			                    ""person.location"" : {
			                        ""points"" : [
			                            ""drn5x1g8cu2y"",
			                            ""30, -80"",
			                            ""20, -90""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

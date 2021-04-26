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
	public class GeoDistanceQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:12")]
		public void Line12()
		{
			// tag::b4ef55e48f137e8f67f82b42a047c8f6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b4ef55e48f137e8f67f82b42a047c8f6[]

			response0.MatchesExample(@"PUT /my_locations
			{
			    ""mappings"": {
			        ""properties"": {
			            ""pin"": {
			                ""properties"": {
			                    ""location"": {
			                        ""type"": ""geo_point""
			                    }
			                }
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /my_locations/_doc/1
			{
			    ""pin"" : {
			        ""location"" : {
			            ""lat"" : 40.12,
			            ""lon"" : -71.34
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:45")]
		public void Line45()
		{
			// tag::4639a1bbd12710d5f01f1aaadce09a3e[]
			var response0 = new SearchResponse<object>();
			// end::4639a1bbd12710d5f01f1aaadce09a3e[]

			response0.MatchesExample(@"GET /my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_distance"" : {
			                    ""distance"" : ""200km"",
			                    ""pin.location"" : {
			                        ""lat"" : 40,
			                        ""lon"" : -70
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:77")]
		public void Line77()
		{
			// tag::6fc37ccf570ff7e35b7b0bd4bacb8abd[]
			var response0 = new SearchResponse<object>();
			// end::6fc37ccf570ff7e35b7b0bd4bacb8abd[]

			response0.MatchesExample(@"GET /my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_distance"" : {
			                    ""distance"" : ""12km"",
			                    ""pin.location"" : {
			                        ""lat"" : 40,
			                        ""lon"" : -70
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:106")]
		public void Line106()
		{
			// tag::926fff8330fc3008f62b9de34f385a57[]
			var response0 = new SearchResponse<object>();
			// end::926fff8330fc3008f62b9de34f385a57[]

			response0.MatchesExample(@"GET /my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_distance"" : {
			                    ""distance"" : ""12km"",
			                    ""pin.location"" : [-70, 40]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:132")]
		public void Line132()
		{
			// tag::f878546633c6bcc30edcdcf520a20eba[]
			var response0 = new SearchResponse<object>();
			// end::f878546633c6bcc30edcdcf520a20eba[]

			response0.MatchesExample(@"GET /my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_distance"" : {
			                    ""distance"" : ""12km"",
			                    ""pin.location"" : ""40,-70""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-distance-query.asciidoc:155")]
		public void Line155()
		{
			// tag::48a40f20b752a8120cf020bda041adca[]
			var response0 = new SearchResponse<object>();
			// end::48a40f20b752a8120cf020bda041adca[]

			response0.MatchesExample(@"GET /my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_distance"" : {
			                    ""distance"" : ""12km"",
			                    ""pin.location"" : ""drm3btev3e86""
			                }
			            }
			        }
			    }
			}");
		}
	}
}

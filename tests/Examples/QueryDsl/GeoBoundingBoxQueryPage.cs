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
	public class GeoBoundingBoxQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:11")]
		public void Line11()
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
		[Description("query-dsl/geo-bounding-box-query.asciidoc:43")]
		public void Line43()
		{
			// tag::49abe3273ac51f14cd4b5f1aaa7f6833[]
			var response0 = new SearchResponse<object>();
			// end::49abe3273ac51f14cd4b5f1aaa7f6833[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : {
			                            ""lat"" : 40.73,
			                            ""lon"" : -74.1
			                        },
			                        ""bottom_right"" : {
			                            ""lat"" : 40.01,
			                            ""lon"" : -71.12
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:132")]
		public void Line132()
		{
			// tag::2cbaaab829728c46359d2f68b71c446e[]
			var response0 = new SearchResponse<object>();
			// end::2cbaaab829728c46359d2f68b71c446e[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : [-74.1, 40.73],
			                        ""bottom_right"" : [-71.12, 40.01]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:159")]
		public void Line159()
		{
			// tag::bbf04a7f7a8858e911d6a53fe88127b0[]
			var response0 = new SearchResponse<object>();
			// end::bbf04a7f7a8858e911d6a53fe88127b0[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : ""40.73, -74.1"",
			                        ""bottom_right"" : ""40.01, -71.12""
			                    }
			                }
			            }
			    }
			}
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:184")]
		public void Line184()
		{
			// tag::417dcb29f5547d4de9d75d8b6a7a53c8[]
			var response0 = new SearchResponse<object>();
			// end::417dcb29f5547d4de9d75d8b6a7a53c8[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""wkt"" : ""BBOX (-74.1, -71.12, 40.73, 40.01)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:208")]
		public void Line208()
		{
			// tag::d84695e3db2c92cd3faebf729e482bf0[]
			var response0 = new SearchResponse<object>();
			// end::d84695e3db2c92cd3faebf729e482bf0[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : ""dr5r9ydj2y73"",
			                        ""bottom_right"" : ""drj7teegpus6""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:242")]
		public void Line242()
		{
			// tag::32ffcae9e1d13df0b7295c349d9145ec[]
			var response0 = new SearchResponse<object>();
			// end::32ffcae9e1d13df0b7295c349d9145ec[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""geo_bounding_box"" : {
			            ""pin.location"" : {
			                ""top_left"" : ""dr"",
			                ""bottom_right"" : ""dr""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:271")]
		public void Line271()
		{
			// tag::370750b2f51bd097f4578e5b105babdf[]
			var response0 = new SearchResponse<object>();
			// end::370750b2f51bd097f4578e5b105babdf[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top"" : 40.73,
			                        ""left"" : -74.1,
			                        ""bottom"" : 40.01,
			                        ""right"" : -71.12
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-bounding-box-query.asciidoc:320")]
		public void Line320()
		{
			// tag::15eee00f09d2290e0f350d420029906e[]
			var response0 = new SearchResponse<object>();
			// end::15eee00f09d2290e0f350d420029906e[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : {
			                            ""lat"" : 40.73,
			                            ""lon"" : -74.1
			                        },
			                        ""bottom_right"" : {
			                            ""lat"" : 40.10,
			                            ""lon"" : -71.12
			                        }
			                    },
			                    ""type"" : ""indexed""
			                }
			            }
			        }
			    }
			}");
		}
	}
}

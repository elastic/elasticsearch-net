using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class GeoDistanceQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
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
		public void Line46()
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
		public void Line79()
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
		public void Line109()
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
		public void Line136()
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
		public void Line160()
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
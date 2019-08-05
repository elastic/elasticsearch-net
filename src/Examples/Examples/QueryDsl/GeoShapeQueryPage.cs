using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class GeoShapeQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::183be708fc91109008109b5ed44c8b08[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::183be708fc91109008109b5ed44c8b08[]

			response0.MatchesExample(@"PUT /example
			{
			    ""mappings"": {
			        ""properties"": {
			            ""location"": {
			                ""type"": ""geo_shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"POST /example/_doc?refresh
			{
			    ""name"": ""Wind & Wetter, Berlin, Germany"",
			    ""location"": {
			        ""type"": ""point"",
			        ""coordinates"": [13.400544, 52.530286]
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line57()
		{
			// tag::129975da094b6b93cc8fcc4042d47913[]
			var response0 = new SearchResponse<object>();
			// end::129975da094b6b93cc8fcc4042d47913[]

			response0.MatchesExample(@"GET /example/_search
			{
			    ""query"":{
			        ""bool"": {
			            ""must"": {
			                ""match_all"": {}
			            },
			            ""filter"": {
			                ""geo_shape"": {
			                    ""location"": {
			                        ""shape"": {
			                            ""type"": ""envelope"",
			                            ""coordinates"" : [[13.0, 53.0], [14.0, 52.0]]
			                        },
			                        ""relation"": ""within""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line102()
		{
			// tag::0e941a8309c3743972b8f5a8d9d9ada6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::0e941a8309c3743972b8f5a8d9d9ada6[]

			response0.MatchesExample(@"PUT /shapes
			{
			    ""mappings"": {
			        ""properties"": {
			            ""location"": {
			                ""type"": ""geo_shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /shapes/_doc/deu
			{
			    ""location"": {
			        ""type"": ""envelope"",
			        ""coordinates"" : [[13.0, 53.0], [14.0, 52.0]]
			    }
			}");

			response2.MatchesExample(@"GET /example/_search
			{
			    ""query"": {
			        ""bool"": {
			            ""filter"": {
			                ""geo_shape"": {
			                    ""location"": {
			                        ""indexed_shape"": {
			                            ""index"": ""shapes"",
			                            ""id"": ""deu"",
			                            ""path"": ""location""
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
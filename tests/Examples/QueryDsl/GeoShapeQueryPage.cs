// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class GeoShapeQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:31")]
		public void Line31()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:59")]
		public void Line59()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:87")]
		public void Line87()
		{
			// tag::33106dcb58ac2e2528e304836ccd6329[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::33106dcb58ac2e2528e304836ccd6329[]

			response0.MatchesExample(@"PUT /example_points
			{
			    ""mappings"": {
			        ""properties"": {
			            ""location"": {
			                ""type"": ""geo_point""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /example_points/_doc/1?refresh
			{
			    ""name"": ""Wind & Wetter, Berlin, Germany"",
			    ""location"": [13.400544, 52.530286]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:112")]
		public void Line112()
		{
			// tag::38073ce8915f9f21e1a3dfe159c4999c[]
			var response0 = new SearchResponse<object>();
			// end::38073ce8915f9f21e1a3dfe159c4999c[]

			response0.MatchesExample(@"GET /example_points/_search
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
			                        ""relation"": ""intersects""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:190")]
		public void Line190()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:288")]
		public void Line288()
		{
			// tag::5e3673bcbef5731746e400c4f3fe134d[]
			var response0 = new SearchResponse<object>();
			// end::5e3673bcbef5731746e400c4f3fe134d[]

			response0.MatchesExample(@"PUT /test/_doc/1
			{
			  ""location"": [
			    {
			      ""coordinates"": [46.25,20.14],
			      ""type"": ""point""
			    },
			    {
			      ""coordinates"": [47.49,19.04],
			      ""type"": ""point""
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/geo-shape-query.asciidoc:306")]
		public void Line306()
		{
			// tag::170c8a3fb81a4e93cd3034a3b5a43ac9[]
			var response0 = new SearchResponse<object>();
			// end::170c8a3fb81a4e93cd3034a3b5a43ac9[]

			response0.MatchesExample(@"PUT /test/_doc/1
			{
			  ""location"":
			    {
			      ""coordinates"": [[46.25,20.14],[47.49,19.04]],
			      ""type"": ""multipoint""
			    }
			}");
		}
	}
}

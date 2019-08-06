using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class GeohashgridAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
		{
			// tag::71af0fec59d37477c850d47730d3f286[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::71af0fec59d37477c850d47730d3f286[]

			response0.MatchesExample(@"PUT /museums
			{
			    ""mappings"": {
			          ""properties"": {
			              ""location"": {
			                  ""type"": ""geo_point""
			              }
			          }
			    }
			}");

			response1.MatchesExample(@"POST /museums/_bulk?refresh
			{""index"":{""_id"":1}}
			{""location"": ""52.374081,4.912350"", ""name"": ""NEMO Science Museum""}
			{""index"":{""_id"":2}}
			{""location"": ""52.369219,4.901618"", ""name"": ""Museum Het Rembrandthuis""}
			{""index"":{""_id"":3}}
			{""location"": ""52.371667,4.914722"", ""name"": ""Nederlands Scheepvaartmuseum""}
			{""index"":{""_id"":4}}
			{""location"": ""51.222900,4.405200"", ""name"": ""Letterenhuis""}
			{""index"":{""_id"":5}}
			{""location"": ""48.861111,2.336389"", ""name"": ""Musée du Louvre""}
			{""index"":{""_id"":6}}
			{""location"": ""48.860000,2.327000"", ""name"": ""Musée d'Orsay""}");

			response2.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggregations"" : {
			        ""large-grid"" : {
			            ""geohash_grid"" : {
			                ""field"" : ""location"",
			                ""precision"" : 3
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::9f0c6a8c6381bb0cb81a3070dd2bf2f2[]
			var response0 = new SearchResponse<object>();
			// end::9f0c6a8c6381bb0cb81a3070dd2bf2f2[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggregations"" : {
			        ""zoomed-in"" : {
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""location"" : {
			                        ""top_left"" : ""52.4, 4.9"",
			                        ""bottom_right"" : ""52.3, 5.0""
			                    }
			                }
			            },
			            ""aggregations"":{
			                ""zoom1"":{
			                    ""geohash_grid"" : {
			                        ""field"": ""location"",
			                        ""precision"": 8
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line126()
		{
			// tag::36f61e038014f92466cd83d7b007e16b[]
			var response0 = new SearchResponse<object>();
			// end::36f61e038014f92466cd83d7b007e16b[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggregations"" : {
			        ""zoomed-in"" : {
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""location"" : {
			                        ""top_left"" : ""u17"",
			                        ""bottom_right"" : ""u17""
			                    }
			                }
			            },
			            ""aggregations"":{
			                ""zoom1"":{
			                    ""geohash_grid"" : {
			                        ""field"": ""location"",
			                        ""precision"": 8
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
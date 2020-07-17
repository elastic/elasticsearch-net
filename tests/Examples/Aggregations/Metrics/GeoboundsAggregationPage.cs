// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class GeoboundsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/geobounds-aggregation.asciidoc:10")]
		public void Line10()
		{
			// tag::34cabdecfe9c2cb8dd929853882564eb[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::34cabdecfe9c2cb8dd929853882564eb[]

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
			    ""query"" : {
			        ""match"" : { ""name"" : ""musée"" }
			    },
			    ""aggs"" : {
			        ""viewport"" : {
			            ""geo_bounds"" : {
			                ""field"" : ""location"", \<1>
			                ""wrap_longitude"" : true \<2>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/geobounds-aggregation.asciidoc:90")]
		public void Line90()
		{
			// tag::930f51600604a9cc3eae43d7fbbc633a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::930f51600604a9cc3eae43d7fbbc633a[]

			response0.MatchesExample(@"PUT /places
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""geo_shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"POST /places/_bulk?refresh
			{""index"":{""_id"":1}}
			{""name"": ""NEMO Science Museum"", ""geometry"": ""POINT(4.912350 52.374081)"" }
			{""index"":{""_id"":2}}
			{""name"": ""Sportpark De Weeren"", ""geometry"": { ""type"": ""Polygon"", ""coordinates"": [ [ [ 4.965305328369141, 52.39347642069457 ], [ 4.966979026794433, 52.391721758934835 ], [ 4.969425201416015, 52.39238958618537 ], [ 4.967944622039794, 52.39420969150824 ], [ 4.965305328369141, 52.39347642069457 ] ] ] } }");

			response2.MatchesExample(@"POST /places/_search?size=0
			{
			    ""aggs"" : {
			        ""viewport"" : {
			            ""geo_bounds"" : {
			                ""field"" : ""geometry""
			            }
			        }
			    }
			}");
		}
	}
}

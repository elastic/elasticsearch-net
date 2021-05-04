// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class GeocentroidAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/geocentroid-aggregation.asciidoc:9")]
		public void Line9()
		{
			// tag::d0cf6057bc87042819a7ac961d1b2273[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::d0cf6057bc87042819a7ac961d1b2273[]

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
			{""location"": ""52.374081,4.912350"", ""city"": ""Amsterdam"", ""name"": ""NEMO Science Museum""}
			{""index"":{""_id"":2}}
			{""location"": ""52.369219,4.901618"", ""city"": ""Amsterdam"", ""name"": ""Museum Het Rembrandthuis""}
			{""index"":{""_id"":3}}
			{""location"": ""52.371667,4.914722"", ""city"": ""Amsterdam"", ""name"": ""Nederlands Scheepvaartmuseum""}
			{""index"":{""_id"":4}}
			{""location"": ""51.222900,4.405200"", ""city"": ""Antwerp"", ""name"": ""Letterenhuis""}
			{""index"":{""_id"":5}}
			{""location"": ""48.861111,2.336389"", ""city"": ""Paris"", ""name"": ""Musée du Louvre""}
			{""index"":{""_id"":6}}
			{""location"": ""48.860000,2.327000"", ""city"": ""Paris"", ""name"": ""Musée d'Orsay""}");

			response2.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""centroid"" : {
			            ""geo_centroid"" : {
			                ""field"" : ""location"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/geocentroid-aggregation.asciidoc:75")]
		public void Line75()
		{
			// tag::6dec421bf327ecaf189109d9aaa35919[]
			var response0 = new SearchResponse<object>();
			// end::6dec421bf327ecaf189109d9aaa35919[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""cities"" : {
			            ""terms"" : { ""field"" : ""city.keyword"" },
			            ""aggs"" : {
			                ""centroid"" : {
			                    ""geo_centroid"" : { ""field"" : ""location"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/geocentroid-aggregation.asciidoc:179")]
		public void Line179()
		{
			// tag::b65debaad33edbdd024fad3d72c626b6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::b65debaad33edbdd024fad3d72c626b6[]

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
			        ""centroid"" : {
			            ""geo_centroid"" : {
			                ""field"" : ""geometry""
			            }
			        }
			    }
			}");
		}
	}
}

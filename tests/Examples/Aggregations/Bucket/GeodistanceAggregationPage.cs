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

namespace Examples.Aggregations.Bucket
{
	public class GeodistanceAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geodistance-aggregation.asciidoc:7")]
		public void Line7()
		{
			// tag::9bf956f9d3f27bb7b4e5a03af84d5da5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::9bf956f9d3f27bb7b4e5a03af84d5da5[]

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
			    ""aggs"" : {
			        ""rings_around_amsterdam"" : {
			            ""geo_distance"" : {
			                ""field"" : ""location"",
			                ""origin"" : ""52.3760, 4.894"",
			                ""ranges"" : [
			                    { ""to"" : 100000 },
			                    { ""from"" : 100000, ""to"" : 300000 },
			                    { ""from"" : 300000 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geodistance-aggregation.asciidoc:93")]
		public void Line93()
		{
			// tag::c78b80d080a58090583228421ac1553d[]
			var response0 = new SearchResponse<object>();
			// end::c78b80d080a58090583228421ac1553d[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""rings"" : {
			            ""geo_distance"" : {
			                ""field"" : ""location"",
			                ""origin"" : ""52.3760, 4.894"",
			                ""unit"" : ""km"", \<1>
			                ""ranges"" : [
			                    { ""to"" : 100 },
			                    { ""from"" : 100, ""to"" : 300 },
			                    { ""from"" : 300 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geodistance-aggregation.asciidoc:119")]
		public void Line119()
		{
			// tag::a5736ad3638c238e3b15c9fdaa1f29f7[]
			var response0 = new SearchResponse<object>();
			// end::a5736ad3638c238e3b15c9fdaa1f29f7[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""rings"" : {
			            ""geo_distance"" : {
			                ""field"" : ""location"",
			                ""origin"" : ""52.3760, 4.894"",
			                ""unit"" : ""km"",
			                ""distance_type"" : ""plane"",
			                ""ranges"" : [
			                    { ""to"" : 100 },
			                    { ""from"" : 100, ""to"" : 300 },
			                    { ""from"" : 300 }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geodistance-aggregation.asciidoc:146")]
		public void Line146()
		{
			// tag::6b31f435607617d96b1dff3bf10c9d8c[]
			var response0 = new SearchResponse<object>();
			// end::6b31f435607617d96b1dff3bf10c9d8c[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""rings_around_amsterdam"" : {
			            ""geo_distance"" : {
			                ""field"" : ""location"",
			                ""origin"" : ""52.3760, 4.894"",
			                ""ranges"" : [
			                    { ""to"" : 100000 },
			                    { ""from"" : 100000, ""to"" : 300000 },
			                    { ""from"" : 300000 }
			                ],
			                ""keyed"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geodistance-aggregation.asciidoc:200")]
		public void Line200()
		{
			// tag::c5afc3d716fdf8c0eefa4732e8a4b3ee[]
			var response0 = new SearchResponse<object>();
			// end::c5afc3d716fdf8c0eefa4732e8a4b3ee[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggs"" : {
			        ""rings_around_amsterdam"" : {
			            ""geo_distance"" : {
			                ""field"" : ""location"",
			                ""origin"" : ""52.3760, 4.894"",
			                ""ranges"" : [
			                    { ""to"" : 100000, ""key"": ""first_ring"" },
			                    { ""from"" : 100000, ""to"" : 300000, ""key"": ""second_ring"" },
			                    { ""from"" : 300000, ""key"": ""third_ring"" }
			                ],
			                ""keyed"": true
			            }
			        }
			    }
			}");
		}
	}
}

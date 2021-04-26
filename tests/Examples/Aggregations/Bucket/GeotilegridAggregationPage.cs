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
	public class GeotilegridAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geotilegrid-aggregation.asciidoc:34")]
		public void Line34()
		{
			// tag::86f1e66bc101b3f22dc84d2aa172fd75[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::86f1e66bc101b3f22dc84d2aa172fd75[]

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
			            ""geotile_grid"" : {
			                ""field"" : ""location"",
			                ""precision"" : 8
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geotilegrid-aggregation.asciidoc:109")]
		public void Line109()
		{
			// tag::57705815ad6bd50d91e58153ae75d3ca[]
			var response0 = new SearchResponse<object>();
			// end::57705815ad6bd50d91e58153ae75d3ca[]

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
			                    ""geotile_grid"" : {
			                        ""field"": ""location"",
			                        ""precision"": 22
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/geotilegrid-aggregation.asciidoc:177")]
		public void Line177()
		{
			// tag::473bc08acc95689e256c7160fec07c0c[]
			var response0 = new SearchResponse<object>();
			// end::473bc08acc95689e256c7160fec07c0c[]

			response0.MatchesExample(@"POST /museums/_search?size=0
			{
			    ""aggregations"" : {
			        ""tiles-in-bounds"" : {
			            ""geotile_grid"" : {
			                ""field"" : ""location"",
			                ""precision"" : 22,
			                ""bounds"": {
			                  ""top_left"" : ""52.4, 4.9"",
			                  ""bottom_right"" : ""52.3, 5.0""
			                }
			            }
			        }
			    }
			}");
		}
	}
}

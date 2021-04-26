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

namespace Examples.Mapping.Types
{
	public class GeoPointPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/geo-point.asciidoc:20")]
		public void Line20()
		{
			// tag::f1b512400f2f7ca0b0f2e4bb45a8b2fe[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();
			// end::f1b512400f2f7ca0b0f2e4bb45a8b2fe[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""location"": {
			        ""type"": ""geo_point""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"": ""Geo-point as an object"",
			  ""location"": { \<1>
			    ""lat"": 41.12,
			    ""lon"": -71.34
			  }
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""text"": ""Geo-point as a string"",
			  ""location"": ""41.12,-71.34"" \<2>
			}");

			response3.MatchesExample(@"PUT my_index/_doc/3
			{
			  ""text"": ""Geo-point as a geohash"",
			  ""location"": ""drm3btev3e86"" \<3>
			}");

			response4.MatchesExample(@"PUT my_index/_doc/4
			{
			  ""text"": ""Geo-point as an array"",
			  ""location"": [ -71.34, 41.12 ] \<4>
			}");

			response5.MatchesExample(@"PUT my_index/_doc/5
			{
			  ""text"": ""Geo-point as a WKT POINT primitive"",
			  ""location"" : ""POINT (-71.34 41.12)"" \<5>
			}");

			response6.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""geo_bounding_box"": { \<6>
			      ""location"": {
			        ""top_left"": {
			          ""lat"": 42,
			          ""lon"": -72
			        },
			        ""bottom_right"": {
			          ""lat"": 40,
			          ""lon"": -74
			        }
			      }
			    }
			  }
			}");
		}
	}
}

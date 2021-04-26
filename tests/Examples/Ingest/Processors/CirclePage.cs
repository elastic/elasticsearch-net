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

namespace Examples.Ingest.Processors
{
	public class CirclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/circle.asciidoc:24")]
		public void Line24()
		{
			// tag::92223bd2873546d7efb557de81b9f75d[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::92223bd2873546d7efb557de81b9f75d[]

			response0.MatchesExample(@"PUT circles
			{
			  ""mappings"": {
			    ""properties"": {
			      ""circle"": {
			        ""type"": ""geo_shape""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT _ingest/pipeline/polygonize_circles
			{
			    ""description"": ""translate circle to polygon"",
			    ""processors"": [
			      {
			        ""circle"": {
			          ""field"": ""circle"",
			          ""error_distance"": 28.0,
			          ""shape_type"": ""geo_shape""
			        }
			      }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/circle.asciidoc:61")]
		public void Line61()
		{
			// tag::b0b1ae9582599f501f3b3ed8a42ea2af[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b0b1ae9582599f501f3b3ed8a42ea2af[]

			response0.MatchesExample(@"PUT circles/_doc/1?pipeline=polygonize_circles
			{
			  ""circle"": ""CIRCLE (30 10 40)""
			}");

			response1.MatchesExample(@"GET circles/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/circle.asciidoc:94")]
		public void Line94()
		{
			// tag::415b46bc2b7a7b4dcf9a73ac67ea20e9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::415b46bc2b7a7b4dcf9a73ac67ea20e9[]

			response0.MatchesExample(@"PUT circles/_doc/2?pipeline=polygonize_circles
			{
			  ""circle"": {
			    ""type"": ""circle"",
			    ""radius"": ""40m"",
			    ""coordinates"": [30, 10]
			  }
			}");

			response1.MatchesExample(@"GET circles/_doc/2");
		}
	}
}

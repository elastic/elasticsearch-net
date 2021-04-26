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

namespace Examples.Aggregations.Matrix
{
	public class StatsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/matrix/stats-aggregation.asciidoc:39")]
		public void Line39()
		{
			// tag::8ab89e635fcbc485d1728c13dfeeb1ae[]
			var response0 = new SearchResponse<object>();
			// end::8ab89e635fcbc485d1728c13dfeeb1ae[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"": {
			        ""statistics"": {
			            ""matrix_stats"": {
			                ""fields"": [""poverty"", ""income""]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/matrix/stats-aggregation.asciidoc:122")]
		public void Line122()
		{
			// tag::7ee2877f8f031b9a4e56a40b371421fb[]
			var response0 = new SearchResponse<object>();
			// end::7ee2877f8f031b9a4e56a40b371421fb[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"": {
			        ""matrixstats"": {
			            ""matrix_stats"": {
			                ""fields"": [""poverty"", ""income""],
			                ""missing"": {""income"" : 50000} \<1>
			            }
			        }
			    }
			}");
		}
	}
}

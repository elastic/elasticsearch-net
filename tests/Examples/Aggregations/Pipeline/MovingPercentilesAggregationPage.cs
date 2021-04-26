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
using System.ComponentModel;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class MovingPercentilesAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/moving-percentiles-aggregation.asciidoc:41")]
		public void Line41()
		{
			// tag::b3ccf004ec109e930aebebd653364f74[]
			var response0 = new SearchResponse<object>();
			// end::b3ccf004ec109e930aebebd653364f74[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{                <1>
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_percentile"":{        <2>
			                    ""percentiles"":{
			                      ""field"": ""price"",
			                      ""percents"": [ 1.0, 99.0 ]
			                     }
			                },
			                ""the_movperc"": {
			                    ""moving_percentiles"": {
			                        ""buckets_path"": ""the_percentile"", <3>
			                        ""window"": 10
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
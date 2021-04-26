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
	public class GlobalAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/global-aggregation.asciidoc:15")]
		public void Line15()
		{
			// tag::d209f2447584a37e7f1480912b40a52d[]
			var response0 = new SearchResponse<object>();
			// end::d209f2447584a37e7f1480912b40a52d[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""match"" : { ""type"" : ""t-shirt"" }
			    },
			    ""aggs"" : {
			        ""all_products"" : {
			            ""global"" : {}, \<1>
			            ""aggs"" : { \<2>
			                ""avg_price"" : { ""avg"" : { ""field"" : ""price"" } }
			            }
			        },
			        ""t_shirts"": { ""avg"" : { ""field"" : ""price"" } }
			    }
			}");
		}
	}
}

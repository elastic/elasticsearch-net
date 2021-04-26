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

namespace Examples.Aggregations.Pipeline
{
	public class BucketSelectorAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/bucket-selector-aggregation.asciidoc:48")]
		public void Line48()
		{
			// tag::7851d52ed462f0a1bdfd4f676e4a4363[]
			var response0 = new SearchResponse<object>();
			// end::7851d52ed462f0a1bdfd4f676e4a4363[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""sales_per_month"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            },
			            ""aggs"": {
			                ""total_sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                },
			                ""sales_bucket_filter"": {
			                    ""bucket_selector"": {
			                        ""buckets_path"": {
			                          ""totalSales"": ""total_sales""
			                        },
			                        ""script"": ""params.totalSales > 200""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

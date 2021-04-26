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
	public class PercentilesBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/percentiles-bucket-aggregation.asciidoc:38")]
		public void Line38()
		{
			// tag::cff65c0f9fbc53c26c60abe9fb7e4044[]
			var response0 = new SearchResponse<object>();
			// end::cff65c0f9fbc53c26c60abe9fb7e4044[]

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
			                ""sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                }
			            }
			        },
			        ""percentiles_monthly_sales"": {
			            ""percentiles_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"", \<1>
			                ""percents"": [ 25.0, 50.0, 75.0 ] \<2>
			            }
			        }
			    }
			}");
		}
	}
}

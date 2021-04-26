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
	public class AvgBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/avg-bucket-aggregation.asciidoc:37")]
		public void Line37()
		{
			// tag::b3e8697874ed65ed6cb62f2568bcc55e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b3e8697874ed65ed6cb62f2568bcc55e[]

			response0.MatchesExample(@"POST /_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""sales_per_month"": {
			      ""date_histogram"": {
			        ""field"": ""date"",
			        ""calendar_interval"": ""month""
			      },
			      ""aggs"": {
			        ""sales"": {
			          ""sum"": {
			            ""field"": ""price""
			          }
			        }
			      }
			    },
			    ""avg_monthly_sales"": {
			      ""avg_bucket"": {
			        ""buckets_path"": ""sales_per_month>sales"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"");
		}
	}
}

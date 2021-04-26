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
	public class AutodatehistogramAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:14")]
		public void Line14()
		{
			// tag::9f9123f67baff22429bca73f7cf48622[]
			var response0 = new SearchResponse<object>();
			// end::9f9123f67baff22429bca73f7cf48622[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""auto_date_histogram"" : {
			                ""field"" : ""date"",
			                ""buckets"" : 10
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:40")]
		public void Line40()
		{
			// tag::941466b290eaa9a2685bbe32c73e887a[]
			var response0 = new SearchResponse<object>();
			// end::941466b290eaa9a2685bbe32c73e887a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""auto_date_histogram"" : {
			                ""field"" : ""date"",
			                ""buckets"" : 5,
			                ""format"" : ""yyyy-MM-dd"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:121")]
		public void Line121()
		{
			// tag::06cd928a050b75f2bb207a548ac43d40[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::06cd928a050b75f2bb207a548ac43d40[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T00:30:00Z""
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T01:30:00Z""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/3?refresh
			{
			  ""date"": ""2015-10-01T02:30:00Z""
			}");

			response3.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""auto_date_histogram"": {
			        ""field"":     ""date"",
			        ""buckets"" : 3
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:187")]
		public void Line187()
		{
			// tag::e16449c0f4eadb394761e9c2aff50fe6[]
			var response0 = new SearchResponse<object>();
			// end::e16449c0f4eadb394761e9c2aff50fe6[]

			response0.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""auto_date_histogram"": {
			        ""field"":     ""date"",
			        ""buckets"" : 3,
			        ""time_zone"": ""-01:00""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:273")]
		public void Line273()
		{
			// tag::00abcf63bffec42e5d2c15011e989b37[]
			var response0 = new SearchResponse<object>();
			// end::00abcf63bffec42e5d2c15011e989b37[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""auto_date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""buckets"": 10,
			                 ""minimum_interval"": ""minute""
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/autodatehistogram-aggregation.asciidoc:296")]
		public void Line296()
		{
			// tag::89fe7b404791770a2075f2870fd65c3e[]
			var response0 = new SearchResponse<object>();
			// end::89fe7b404791770a2075f2870fd65c3e[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""auto_date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""buckets"": 10,
			                 ""missing"": ""2000/01/01"" \<1>
			             }
			         }
			    }
			}");
		}
	}
}

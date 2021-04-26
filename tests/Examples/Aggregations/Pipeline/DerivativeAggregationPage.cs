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
	public class DerivativeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/derivative-aggregation.asciidoc:38")]
		public void Line38()
		{
			// tag::469bc2e7b9e65b3b1e38a547f63bd2f9[]
			var response0 = new SearchResponse<object>();
			// end::469bc2e7b9e65b3b1e38a547f63bd2f9[]

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
			                },
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/derivative-aggregation.asciidoc:131")]
		public void Line131()
		{
			// tag::d683ed8c4a72f82200bbad0c3921e427[]
			var response0 = new SearchResponse<object>();
			// end::d683ed8c4a72f82200bbad0c3921e427[]

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
			                },
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales""
			                    }
			                },
			                ""sales_2nd_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales_deriv"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/derivative-aggregation.asciidoc:230")]
		public void Line230()
		{
			// tag::8553b0c396e9de7d841fcc6373e017e2[]
			var response0 = new SearchResponse<object>();
			// end::8553b0c396e9de7d841fcc6373e017e2[]

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
			                },
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales"",
			                        ""unit"": ""day"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

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

namespace Examples.Aggregations.Metrics
{
	public class MaxAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/max-aggregation.asciidoc:16")]
		public void Line16()
		{
			// tag::9498a707be49e14dad801db6b6824e34[]
			var response0 = new SearchResponse<object>();
			// end::9498a707be49e14dad801db6b6824e34[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""max_price"" : { ""max"" : { ""field"" : ""price"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/max-aggregation.asciidoc:51")]
		public void Line51()
		{
			// tag::736fc5448b66962ceef1e6d5948ef691[]
			var response0 = new SearchResponse<object>();
			// end::736fc5448b66962ceef1e6d5948ef691[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""max_price"" : {
			            ""max"" : {
			                ""script"" : {
			                    ""source"" : ""doc.price.value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/max-aggregation.asciidoc:71")]
		public void Line71()
		{
			// tag::b5e782e309a2a10db272414e8483d8dc[]
			var response0 = new SearchResponse<object>();
			// end::b5e782e309a2a10db272414e8483d8dc[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""max_price"" : {
			            ""max"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""price""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/max-aggregation.asciidoc:98")]
		public void Line98()
		{
			// tag::23fdba37454d6d7abf6bfbb4fd01692f[]
			var response0 = new SearchResponse<object>();
			// end::23fdba37454d6d7abf6bfbb4fd01692f[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""max_price_in_euros"" : {
			            ""max"" : {
			                ""field"" : ""price"",
			                ""script"" : {
			                    ""source"" : ""_value * params.conversion_rate"",
			                    ""params"" : {
			                        ""conversion_rate"" : 1.2
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/max-aggregation.asciidoc:125")]
		public void Line125()
		{
			// tag::41518c094db4a5b03cca3b21497f79cf[]
			var response0 = new SearchResponse<object>();
			// end::41518c094db4a5b03cca3b21497f79cf[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""grade_max"" : {
			            ""max"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}
	}
}

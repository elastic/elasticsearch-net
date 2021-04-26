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
	public class NestedAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/nested-aggregation.asciidoc:10")]
		public void Line10()
		{
			// tag::60917d97013c4516c621c6c24c29748f[]
			var response0 = new SearchResponse<object>();
			// end::60917d97013c4516c621c6c24c29748f[]

			response0.MatchesExample(@"PUT /products
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""resellers"" : { <1>
			                ""type"" : ""nested"",
			                ""properties"" : {
			                    ""reseller"" : { ""type"" : ""text"" },
			                    ""price"" : { ""type"" : ""double"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/nested-aggregation.asciidoc:31")]
		public void Line31()
		{
			// tag::a1849d3cb44fc24b58323ec97c5e9c5c[]
			var response0 = new SearchResponse<object>();
			// end::a1849d3cb44fc24b58323ec97c5e9c5c[]

			response0.MatchesExample(@"PUT /products/_doc/0
			{
			  ""name"": ""LED TV"", <1>
			  ""resellers"": [
			    {
			      ""reseller"": ""companyA"",
			      ""price"": 350
			    },
			    {
			      ""reseller"": ""companyB"",
			      ""price"": 500
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/nested-aggregation.asciidoc:55")]
		public void Line55()
		{
			// tag::be91aeed1af812064943dd5192425ab2[]
			var response0 = new SearchResponse<object>();
			// end::be91aeed1af812064943dd5192425ab2[]

			response0.MatchesExample(@"GET /products/_search
			{
			    ""query"" : {
			        ""match"" : { ""name"" : ""led tv"" }
			    },
			    ""aggs"" : {
			        ""resellers"" : {
			            ""nested"" : {
			                ""path"" : ""resellers""
			            },
			            ""aggs"" : {
			                ""min_price"" : { ""min"" : { ""field"" : ""resellers.price"" } }
			            }
			        }
			    }
			}");
		}
	}
}

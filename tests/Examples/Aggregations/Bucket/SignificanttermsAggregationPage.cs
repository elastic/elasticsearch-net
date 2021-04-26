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
	public class SignificanttermsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:67")]
		public void Line67()
		{
			// tag::290b845e59368e8aa8d1a56d7379afd0[]
			var response0 = new SearchResponse<object>();
			// end::290b845e59368e8aa8d1a56d7379afd0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""terms"" : {""force"" : [ ""British Transport Police"" ]}
			    },
			    ""aggregations"" : {
			        ""significant_crime_types"" : {
			            ""significant_terms"" : { ""field"" : ""crime_type"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:127")]
		public void Line127()
		{
			// tag::b2af9784f8530a363ac6e9f95b39677d[]
			var response0 = new SearchResponse<object>();
			// end::b2af9784f8530a363ac6e9f95b39677d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggregations"": {
			        ""forces"": {
			            ""terms"": {""field"": ""force""},
			            ""aggregations"": {
			                ""significant_crime_types"": {
			                    ""significant_terms"": {""field"": ""crime_type""}
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:204")]
		public void Line204()
		{
			// tag::0868d8ac2fb5351e633184f897ee6866[]
			var response0 = new SearchResponse<object>();
			// end::0868d8ac2fb5351e633184f897ee6866[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"": {
			        ""hotspots"": {
			            ""geohash_grid"": {
			                ""field"": ""location"",
			                ""precision"": 5
			            },
			            ""aggs"": {
			                ""significant_crime_types"": {
			                    ""significant_terms"": {""field"": ""crime_type""}
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:464")]
		public void Line464()
		{
			// tag::09d4a753140ee5a9ab9f4fc09047b588[]
			var response0 = new SearchResponse<object>();
			// end::09d4a753140ee5a9ab9f4fc09047b588[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			            ""significant_terms"" : {
			                ""field"" : ""tag"",
			                ""min_doc_count"": 10
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:507")]
		public void Line507()
		{
			// tag::3fdaac87eb741a79f747633b5065323a[]
			var response0 = new SearchResponse<object>();
			// end::3fdaac87eb741a79f747633b5065323a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"" : {
			            ""city"" : ""madrid""
			        }
			    },
			    ""aggs"" : {
			        ""tags"" : {
			            ""significant_terms"" : {
			                ""field"" : ""tag"",
			                ""background_filter"": {
			                	""term"" : { ""text"" : ""spain""}
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significantterms-aggregation.asciidoc:565")]
		public void Line565()
		{
			// tag::11a21cd0b9d31da7eda77c9384a29208[]
			var response0 = new SearchResponse<object>();
			// end::11a21cd0b9d31da7eda77c9384a29208[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			             ""significant_terms"" : {
			                 ""field"" : ""tags"",
			                 ""execution_hint"": ""map"" \<1>
			             }
			         }
			    }
			}");
		}
	}
}

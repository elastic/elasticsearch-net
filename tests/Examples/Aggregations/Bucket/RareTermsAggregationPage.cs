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
	public class RareTermsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/rare-terms-aggregation.asciidoc:89")]
		public void Line89()
		{
			// tag::91bbb85bc6add315fc9a044d8bcfec8a[]
			var response0 = new SearchResponse<object>();
			// end::91bbb85bc6add315fc9a044d8bcfec8a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""rare_terms"" : {
			                ""field"" : ""genre""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/rare-terms-aggregation.asciidoc:127")]
		public void Line127()
		{
			// tag::0fa84243cd275a885298602aa8b4415f[]
			var response0 = new SearchResponse<object>();
			// end::0fa84243cd275a885298602aa8b4415f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""rare_terms"" : {
			                ""field"" : ""genre"",
			                ""max_doc_count"": 2
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/rare-terms-aggregation.asciidoc:277")]
		public void Line277()
		{
			// tag::b7207f557d5481db52d5df1aa0dae982[]
			var response0 = new SearchResponse<object>();
			// end::b7207f557d5481db52d5df1aa0dae982[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""rare_terms"" : {
			                ""field"" : ""genre"",
			                ""include"" : ""swi*"",
			                ""exclude"" : ""electro*""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/rare-terms-aggregation.asciidoc:305")]
		public void Line305()
		{
			// tag::dea7ef16acd6d148a20876630a010522[]
			var response0 = new SearchResponse<object>();
			// end::dea7ef16acd6d148a20876630a010522[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			             ""rare_terms"" : {
			                 ""field"" : ""genre"",
			                 ""include"" : [""swing"", ""rock""],
			                 ""exclude"" : [""jazz""]
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/rare-terms-aggregation.asciidoc:328")]
		public void Line328()
		{
			// tag::2eea6157a9fbc54a8987e5c1a4f14bbe[]
			var response0 = new SearchResponse<object>();
			// end::2eea6157a9fbc54a8987e5c1a4f14bbe[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			             ""rare_terms"" : {
			                 ""field"" : ""genre"",
			                 ""missing"": ""N/A"" \<1>
			             }
			         }
			    }
			}");
		}
	}
}

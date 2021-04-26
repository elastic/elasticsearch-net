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

namespace Examples.QueryDsl
{
	public class TermsSetQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-set-query.asciidoc:49")]
		public void Line49()
		{
			// tag::f29bc8beaa219c21be3204e010f5a509[]
			var response0 = new SearchResponse<object>();
			// end::f29bc8beaa219c21be3204e010f5a509[]

			response0.MatchesExample(@"PUT /job-candidates
			{
			    ""mappings"": {
			        ""properties"": {
			            ""name"": {
			                ""type"": ""keyword""
			            },
			            ""programming_languages"": {
			                ""type"": ""keyword""
			            },
			            ""required_matches"": {
			                ""type"": ""long""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-set-query.asciidoc:85")]
		public void Line85()
		{
			// tag::6866beb749ef6dee19d2cb56edc0a9ab[]
			var response0 = new SearchResponse<object>();
			// end::6866beb749ef6dee19d2cb56edc0a9ab[]

			response0.MatchesExample(@"PUT /job-candidates/_doc/1?refresh
			{
			    ""name"": ""Jane Smith"",
			    ""programming_languages"": [""c++"", ""java""],
			    ""required_matches"": 2
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-set-query.asciidoc:107")]
		public void Line107()
		{
			// tag::f7bccd5a51a4000215767e9a6454327f[]
			var response0 = new SearchResponse<object>();
			// end::f7bccd5a51a4000215767e9a6454327f[]

			response0.MatchesExample(@"PUT /job-candidates/_doc/2?refresh
			{
			    ""name"": ""Jason Response"",
			    ""programming_languages"": [""java"", ""php""],
			    ""required_matches"": 2
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-set-query.asciidoc:136")]
		public void Line136()
		{
			// tag::c5040ac6dc2922f191113e7a5fd5a699[]
			var response0 = new SearchResponse<object>();
			// end::c5040ac6dc2922f191113e7a5fd5a699[]

			response0.MatchesExample(@"GET /job-candidates/_search
			{
			    ""query"": {
			        ""terms_set"": {
			            ""programming_languages"": {
			                ""terms"": [""c++"", ""java"", ""php""],
			                ""minimum_should_match_field"": ""required_matches""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-set-query.asciidoc:214")]
		public void Line214()
		{
			// tag::cf2e6e604c67175398f6c217b9e86127[]
			var response0 = new SearchResponse<object>();
			// end::cf2e6e604c67175398f6c217b9e86127[]

			response0.MatchesExample(@"GET /job-candidates/_search
			{
			    ""query"": {
			        ""terms_set"": {
			            ""programming_languages"": {
			                ""terms"": [""c++"", ""java"", ""php""],
			                ""minimum_should_match_script"": {
			                   ""source"": ""Math.min(params.num_terms, doc['required_matches'].value)""
			                },
			                ""boost"": 1.0
			            }
			        }
			    }
			}");
		}
	}
}

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
	public class MatchQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/match-query.asciidoc:18")]
		public void Line18()
		{
			// tag::e0d6e02b998bdea99c9c08dcc3630c5e[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("this is a test")
					)
				)
			);
			// end::e0d6e02b998bdea99c9c08dcc3630c5e[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""this is a test""
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-query.asciidoc:150")]
		public void Line150()
		{
			// tag::fa2fe60f570bd930d2891778c6efbfe6[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("this is a test")
					)
				)
			);
			// end::fa2fe60f570bd930d2891778c6efbfe6[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : ""this is a test""
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["query"]["match"]["message"].ToLongFormQuery();
				});
			});
		}

		[U]
		[Description("query-dsl/match-query.asciidoc:175")]
		public void Line175()
		{
			// tag::6138d6919f3cbaaf61e1092f817d295c[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("this is a test")
						.Operator(Operator.And)
					)
				)
			);
			// end::6138d6919f3cbaaf61e1092f817d295c[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""this is a test"",
			                ""operator"" : ""and""
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-query.asciidoc:219")]
		public void Line219()
		{
			// tag::5043b83a89091fa00edb341ddf7ba370[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("this is a testt")
						.Fuzziness(Fuzziness.Auto)
					)
				)
			);
			// end::5043b83a89091fa00edb341ddf7ba370[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""this is a testt"",
			                ""fuzziness"": ""AUTO""
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-query.asciidoc:241")]
		public void Line241()
		{
			// tag::0ac9916f47a2483b89c1416684af322a[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("to be or not to be")
						.Operator(Operator.And)
						.ZeroTermsQuery(ZeroTermsQuery.All)
					)
				)
			);
			// end::0ac9916f47a2483b89c1416684af322a[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match"" : {
			            ""message"" : {
			                ""query"" : ""to be or not to be"",
			                ""operator"" : ""and"",
			                ""zero_terms_query"": ""all""
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/match-query.asciidoc:268")]
		public void Line268()
		{
			// tag::7f56755fb6c42f7e6203339a6d0cb6e6[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("message")
						.Query("ny city")
						.AutoGenerateSynonymsPhraseQuery(false)
					)
				)
			);
			// end::7f56755fb6c42f7e6203339a6d0cb6e6[]

			searchResponse.MatchesExample(@"GET /_search
			{
			   ""query"": {
			       ""match"" : {
			           ""message"": {
			               ""query"" : ""ny city"",
			               ""auto_generate_synonyms_phrase_query"" : false
			           }
			       }
			   }
			}");
		}
	}
}

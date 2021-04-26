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

namespace Examples.Indices.Apis
{
	public class ReloadAnalyzersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/apis/reload-analyzers.asciidoc:14")]
		public void Line14()
		{
			// tag::b0015e63323171f38995b8e4aa2b52d5[]
			var response0 = new SearchResponse<object>();
			// end::b0015e63323171f38995b8e4aa2b52d5[]

			response0.MatchesExample(@"POST /twitter/_reload_search_analyzers");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/apis/reload-analyzers.asciidoc:102")]
		public void Line102()
		{
			// tag::db8cbfa2afece5d21b3ca69ffee8f5c0[]
			var response0 = new SearchResponse<object>();
			// end::db8cbfa2afece5d21b3ca69ffee8f5c0[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""my_synonyms"" : {
			                        ""tokenizer"" : ""whitespace"",
			                        ""filter"" : [""synonym""]
			                    }
			                },
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym_graph"",
			                        ""synonyms_path"" : ""analysis/synonym.txt"", <1>
			                        ""updateable"" : true <2>
			                    }
			                }
			            }
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""text"": {
			                ""type"": ""text"",
			                ""analyzer"" : ""standard"",
			                ""search_analyzer"": ""my_synonyms"" <3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/apis/reload-analyzers.asciidoc:146")]
		public void Line146()
		{
			// tag::7554da505cc27f6bd0d028b66e85f4a5[]
			var response0 = new SearchResponse<object>();
			// end::7554da505cc27f6bd0d028b66e85f4a5[]

			response0.MatchesExample(@"POST /my_index/_reload_search_analyzers");
		}
	}
}

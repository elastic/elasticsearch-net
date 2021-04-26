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

namespace Examples.Analysis.Tokenfilters
{
	public class SynonymGraphTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-graph-tokenfilter.asciidoc:26")]
		public void Line26()
		{
			// tag::2f071d36aa4aff5a2fafb3dadaa38b82[]
			var response0 = new SearchResponse<object>();
			// end::2f071d36aa4aff5a2fafb3dadaa38b82[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""search_synonyms"" : {
			                        ""tokenizer"" : ""whitespace"",
			                        ""filter"" : [""graph_synonyms""]
			                    }
			                },
			                ""filter"" : {
			                    ""graph_synonyms"" : {
			                        ""type"" : ""synonym_graph"",
			                        ""synonyms_path"" : ""analysis/synonym.txt""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-graph-tokenfilter.asciidoc:61")]
		public void Line61()
		{
			// tag::3d253e5a0029bc96cce484302319b772[]
			var response0 = new SearchResponse<object>();
			// end::3d253e5a0029bc96cce484302319b772[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""synonym"" : {
			                        ""tokenizer"" : ""standard"",
			                        ""filter"" : [""my_stop"", ""synonym_graph""]
			                    }
			                },
			                ""filter"" : {
			                    ""my_stop"": {
			                        ""type"" : ""stop"",
			                        ""stopwords"": [""bar""]
			                    },
			                    ""synonym_graph"" : {
			                        ""type"" : ""synonym_graph"",
			                        ""lenient"": true,
			                        ""synonyms"" : [""foo, bar => baz""]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-graph-tokenfilter.asciidoc:121")]
		public void Line121()
		{
			// tag::1a14fd905941ecbdbc943b05875afc6f[]
			var response0 = new SearchResponse<object>();
			// end::1a14fd905941ecbdbc943b05875afc6f[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym_graph"",
			                        ""synonyms"" : [
			                            ""lol, laughing out loud"",
			                            ""universe, cosmos""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-graph-tokenfilter.asciidoc:152")]
		public void Line152()
		{
			// tag::f0d7d6d5c878211704d4a5f1b2f6a247[]
			var response0 = new SearchResponse<object>();
			// end::f0d7d6d5c878211704d4a5f1b2f6a247[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym_graph"",
			                        ""format"" : ""wordnet"",
			                        ""synonyms"" : [
			                            ""s(100000001,1,'abstain',v,1,0)."",
			                            ""s(100000001,2,'refrain',v,1,0)."",
			                            ""s(100000001,3,'desist',v,1,0).""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

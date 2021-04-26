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

namespace Examples.Mapping.Types
{
	public class PercolatorPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:20")]
		public void Line20()
		{
			// tag::05c14dd0bda732cfa36f7fb88138d98e[]
			var response0 = new SearchResponse<object>();
			// end::05c14dd0bda732cfa36f7fb88138d98e[]

			response0.MatchesExample(@"PUT my_index
			{
			    ""mappings"": {
			        ""properties"": {
			            ""query"": {
			                ""type"": ""percolator""
			            },
			            ""field"": {
			                ""type"": ""text""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:40")]
		public void Line40()
		{
			// tag::dc1f917924b43416a9ec7f8c9505f885[]
			var response0 = new SearchResponse<object>();
			// end::dc1f917924b43416a9ec7f8c9505f885[]

			response0.MatchesExample(@"PUT my_index/_doc/match_value
			{
			    ""query"" : {
			        ""match"" : {
			            ""field"" : ""value""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:70")]
		public void Line70()
		{
			// tag::3eb4cdd4a799a117ac1ff5f02b18a512[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::3eb4cdd4a799a117ac1ff5f02b18a512[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""query"" : {
			        ""type"" : ""percolator""
			      },
			      ""body"" : {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST _aliases
			{
			  ""actions"": [
			    {
			      ""add"": {
			        ""index"": ""index"",
			        ""alias"": ""queries"" \<1>
			      }
			    }
			  ]
			}");

			response2.MatchesExample(@"PUT queries/_doc/1?refresh
			{
			  ""query"" : {
			    ""match"" : {
			      ""body"" : ""quick brown fox""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:115")]
		public void Line115()
		{
			// tag::f09817fd13ff3dce52eb79d0722409c3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::f09817fd13ff3dce52eb79d0722409c3[]

			response0.MatchesExample(@"PUT new_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""query"" : {
			        ""type"" : ""percolator""
			      },
			      ""body"" : {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST /_reindex?refresh
			{
			  ""source"": {
			    ""index"": ""index""
			  },
			  ""dest"": {
			    ""index"": ""new_index""
			  }
			}");

			response2.MatchesExample(@"POST _aliases
			{
			  ""actions"": [ \<1>
			    {
			      ""remove"": {
			        ""index"" : ""index"",
			        ""alias"": ""queries""
			      }
			    },
			    {
			      ""add"": {
			        ""index"": ""new_index"",
			        ""alias"": ""queries""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:165")]
		public void Line165()
		{
			// tag::60cab62af1540db2ad3b696b0ee1d7a8[]
			var response0 = new SearchResponse<object>();
			// end::60cab62af1540db2ad3b696b0ee1d7a8[]

			response0.MatchesExample(@"GET /queries/_search
			{
			  ""query"": {
			    ""percolate"" : {
			      ""field"" : ""query"",
			      ""document"" : {
			        ""body"" : ""fox jumps over the lazy dog""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:262")]
		public void Line262()
		{
			// tag::360c4f373e72ba861584ee85bd218124[]
			var response0 = new SearchResponse<object>();
			// end::360c4f373e72ba861584ee85bd218124[]

			response0.MatchesExample(@"PUT /test_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"" : {
			          ""tokenizer"": ""standard"",
			          ""filter"" : [""lowercase"", ""porter_stem""]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""query"" : {
			        ""type"": ""percolator""
			      },
			      ""body"" : {
			        ""type"": ""text"",
			        ""analyzer"": ""my_analyzer"" \<1>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:295")]
		public void Line295()
		{
			// tag::3e13c8a81f40a537eddc0b57633b45f8[]
			var response0 = new SearchResponse<object>();
			// end::3e13c8a81f40a537eddc0b57633b45f8[]

			response0.MatchesExample(@"POST /test_index/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""missing bicycles""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:331")]
		public void Line331()
		{
			// tag::7a2b9a7b2b6553a48bd4db60a939c0fc[]
			var response0 = new SearchResponse<object>();
			// end::7a2b9a7b2b6553a48bd4db60a939c0fc[]

			response0.MatchesExample(@"PUT /test_index/_doc/1?refresh
			{
			  ""query"" : {
			    ""match"" : {
			      ""body"" : {
			        ""query"" : ""miss bicycl"",
			        ""analyzer"" : ""whitespace"" \<1>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:355")]
		public void Line355()
		{
			// tag::45813d971bfa890ffa2f51f3f480cce5[]
			var response0 = new SearchResponse<object>();
			// end::45813d971bfa890ffa2f51f3f480cce5[]

			response0.MatchesExample(@"GET /test_index/_search
			{
			  ""query"": {
			    ""percolate"" : {
			      ""field"" : ""query"",
			      ""document"" : {
			        ""body"" : ""Bycicles are missing""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:427")]
		public void Line427()
		{
			// tag::a04a8d90f8245ff5f30a9983909faa1d[]
			var response0 = new SearchResponse<object>();
			// end::a04a8d90f8245ff5f30a9983909faa1d[]

			response0.MatchesExample(@"PUT my_queries1
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""wildcard_prefix"": { \<1>
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""wildcard_edge_ngram""
			          ]
			        }
			      },
			      ""filter"": {
			        ""wildcard_edge_ngram"": { \<2>
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 1,
			          ""max_gram"": 32
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""query"": {
			        ""type"": ""percolator""
			      },
			      ""my_field"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""prefix"": { \<3>
			            ""type"": ""text"",
			            ""analyzer"": ""wildcard_prefix"",
			            ""search_analyzer"": ""standard""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:495")]
		public void Line495()
		{
			// tag::ed688d86eeaa4d7969acb0f574eb917f[]
			var response0 = new SearchResponse<object>();
			// end::ed688d86eeaa4d7969acb0f574eb917f[]

			response0.MatchesExample(@"PUT /my_queries1/_doc/1?refresh
			{
			  ""query"": {
			    ""term"": {
			      ""my_field.prefix"": ""abc""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:513")]
		public void Line513()
		{
			// tag::d2f6040c058a9555dfa62bb42d896a8f[]
			var response0 = new SearchResponse<object>();
			// end::d2f6040c058a9555dfa62bb42d896a8f[]

			response0.MatchesExample(@"GET /my_queries1/_search
			{
			  ""query"": {
			    ""percolate"": {
			      ""field"": ""query"",
			      ""document"": {
			        ""my_field"": ""abcd""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:574")]
		public void Line574()
		{
			// tag::343dd09a8c76987e586858be3bdc51eb[]
			var response0 = new SearchResponse<object>();
			// end::343dd09a8c76987e586858be3bdc51eb[]

			response0.MatchesExample(@"PUT my_queries2
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""wildcard_suffix"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""reverse"",
			            ""wildcard_edge_ngram""
			          ]
			        },
			        ""wildcard_suffix_search_time"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""reverse""
			          ]
			        }
			      },
			      ""filter"": {
			        ""wildcard_edge_ngram"": {
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 1,
			          ""max_gram"": 32
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""query"": {
			        ""type"": ""percolator""
			      },
			      ""my_field"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""suffix"": {
			            ""type"": ""text"",
			            ""analyzer"": ""wildcard_suffix"",
			            ""search_analyzer"": ""wildcard_suffix_search_time"" \<1>
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:649")]
		public void Line649()
		{
			// tag::bd7330af2609bdd8aa10958f5e640b93[]
			var response0 = new SearchResponse<object>();
			// end::bd7330af2609bdd8aa10958f5e640b93[]

			response0.MatchesExample(@"PUT /my_queries2/_doc/2?refresh
			{
			  ""query"": {
			    ""match"": { \<1>
			      ""my_field.suffix"": ""xyz""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/percolator.asciidoc:668")]
		public void Line668()
		{
			// tag::4aa81a694266fb634904224d14cd9a87[]
			var response0 = new SearchResponse<object>();
			// end::4aa81a694266fb634904224d14cd9a87[]

			response0.MatchesExample(@"GET /my_queries2/_search
			{
			  ""query"": {
			    ""percolate"": {
			      ""field"": ""query"",
			      ""document"": {
			        ""my_field"": ""wxyz""
			      }
			    }
			  }
			}");
		}
	}
}

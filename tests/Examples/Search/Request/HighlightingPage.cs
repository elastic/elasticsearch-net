// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class HighlightingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:24")]
		public void Line24()
		{
			// tag::05e1088d2c04391203cc8eb3ab287b71[]
			var response0 = new SearchResponse<object>();
			// end::05e1088d2c04391203cc8eb3ab287b71[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""content"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""content"" : {}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:279")]
		public void Line279()
		{
			// tag::3cc4e8b1e2aecac644ba52d34ca29422[]
			var response0 = new SearchResponse<object>();
			// end::3cc4e8b1e2aecac644ba52d34ca29422[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""number_of_fragments"" : 3,
			        ""fragment_size"" : 150,
			        ""fields"" : {
			            ""body"" : { ""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] },
			            ""blog.title"" : { ""number_of_fragments"" : 0 },
			            ""blog.author"" : { ""number_of_fragments"" : 0 },
			            ""blog.comment"" : { ""number_of_fragments"" : 5, ""order"" : ""score"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:309")]
		public void Line309()
		{
			// tag::129cddb56fafef5cc454917a374eae1a[]
			var response0 = new SearchResponse<object>();
			// end::129cddb56fafef5cc454917a374eae1a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"": [ ""_id"" ],
			    ""query"" : {
			        ""match"": {
			            ""comment"": {
			                ""query"": ""foo bar""
			            }
			        }
			    },
			    ""rescore"": {
			        ""window_size"": 50,
			        ""query"": {
			            ""rescore_query"" : {
			                ""match_phrase"": {
			                    ""comment"": {
			                        ""query"": ""foo bar"",
			                        ""slop"": 1
			                    }
			                }
			            },
			            ""rescore_query_weight"" : 10
			        }
			    },
			    ""highlight"" : {
			        ""order"" : ""score"",
			        ""fields"" : {
			            ""comment"" : {
			                ""fragment_size"" : 150,
			                ""number_of_fragments"" : 3,
			                ""highlight_query"": {
			                    ""bool"": {
			                        ""must"": {
			                            ""match"": {
			                                ""comment"": {
			                                    ""query"": ""foo bar""
			                                }
			                            }
			                        },
			                        ""should"": {
			                            ""match_phrase"": {
			                                ""comment"": {
			                                    ""query"": ""foo bar"",
			                                    ""slop"": 1,
			                                    ""boost"": 10.0
			                                }
			                            }
			                        },
			                        ""minimum_should_match"": 0
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:377")]
		public void Line377()
		{
			// tag::9e502038aa4ebb9cb4df230c0c4a854e[]
			var response0 = new SearchResponse<object>();
			// end::9e502038aa4ebb9cb4df230c0c4a854e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""type"" : ""plain""}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:401")]
		public void Line401()
		{
			// tag::ee079a3f9eb529aac33f09be16747aa9[]
			var response0 = new SearchResponse<object>();
			// end::ee079a3f9eb529aac33f09be16747aa9[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""pre_tags"" : [""<tag1>""],
			        ""post_tags"" : [""</tag1>""],
			        ""fields"" : {
			            ""body"" : {}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:422")]
		public void Line422()
		{
			// tag::a225bb439c204b20ed52a28e1dcd663b[]
			var response0 = new SearchResponse<object>();
			// end::a225bb439c204b20ed52a28e1dcd663b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""pre_tags"" : [""<tag1>"", ""<tag2>""],
			        ""post_tags"" : [""</tag1>"", ""</tag2>""],
			        ""fields"" : {
			            ""body"" : {}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:442")]
		public void Line442()
		{
			// tag::05ce63b83a89fddb63fd60c923811582[]
			var response0 = new SearchResponse<object>();
			// end::05ce63b83a89fddb63fd60c923811582[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""tags_schema"" : ""styled"",
			        ""fields"" : {
			            ""comment"" : {}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:466")]
		public void Line466()
		{
			// tag::87b697eb7340e9e52ca790922eca0066[]
			var response0 = new SearchResponse<object>();
			// end::87b697eb7340e9e52ca790922eca0066[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""force_source"" : true}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:490")]
		public void Line490()
		{
			// tag::1e8b687c757981af3a9f005cfd2b4946[]
			var response0 = new SearchResponse<object>();
			// end::1e8b687c757981af3a9f005cfd2b4946[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""require_field_match"": false,
			        ""fields"": {
			                ""body"" : { ""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:523")]
		public void Line523()
		{
			// tag::a182c91923ad1e47cf502ea890c53015[]
			var response0 = new SearchResponse<object>();
			// end::a182c91923ad1e47cf502ea890c53015[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""comment.plain:running scissors"",
			            ""fields"": [""comment""]
			        }
			    },
			    ""highlight"": {
			        ""order"": ""score"",
			        ""fields"": {
			            ""comment"": {
			                ""matched_fields"": [""comment"", ""comment.plain""],
			                ""type"" : ""fvh""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:552")]
		public void Line552()
		{
			// tag::974bb1452f614f9a378a695fa9addd4e[]
			var response0 = new SearchResponse<object>();
			// end::974bb1452f614f9a378a695fa9addd4e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""running scissors"",
			            ""fields"": [""comment"", ""comment.plain^10""]
			        }
			    },
			    ""highlight"": {
			        ""order"": ""score"",
			        ""fields"": {
			            ""comment"": {
			                ""matched_fields"": [""comment"", ""comment.plain""],
			                ""type"" : ""fvh""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:579")]
		public void Line579()
		{
			// tag::4971d093f19f85e3c622f1e0257ff60f[]
			var response0 = new SearchResponse<object>();
			// end::4971d093f19f85e3c622f1e0257ff60f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""query_string"": {
			            ""query"": ""running scissors"",
			            ""fields"": [""comment"", ""comment.plain^10""]
			        }
			    },
			    ""highlight"": {
			        ""order"": ""score"",
			        ""fields"": {
			            ""comment"": {
			                ""matched_fields"": [""comment.plain""],
			                ""type"" : ""fvh""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:648")]
		public void Line648()
		{
			// tag::2859fb1a8139777dca087862a5b1c205[]
			var response0 = new SearchResponse<object>();
			// end::2859fb1a8139777dca087862a5b1c205[]

			response0.MatchesExample(@"GET /_search
			{
			    ""highlight"": {
			        ""fields"": [
			            { ""title"": {} },
			            { ""text"": {} }
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:677")]
		public void Line677()
		{
			// tag::e8446172481fb6298c04b4bdc3340f3f[]
			var response0 = new SearchResponse<object>();
			// end::e8446172481fb6298c04b4bdc3340f3f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""fragment_size"" : 150, ""number_of_fragments"" : 3}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:696")]
		public void Line696()
		{
			// tag::4ae1e4f88af2f9be50696e5a59466bb6[]
			var response0 = new SearchResponse<object>();
			// end::4ae1e4f88af2f9be50696e5a59466bb6[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""order"" : ""score"",
			        ""fields"" : {
			            ""comment"" : {""fragment_size"" : 150, ""number_of_fragments"" : 3}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:719")]
		public void Line719()
		{
			// tag::62b15eac8c6d294da9114541fdfc527f[]
			var response0 = new SearchResponse<object>();
			// end::62b15eac8c6d294da9114541fdfc527f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""body"" : {},
			            ""blog.title"" : {""number_of_fragments"" : 0}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:745")]
		public void Line745()
		{
			// tag::3d10eba5cac0069486bc3c2854d15689[]
			var response0 = new SearchResponse<object>();
			// end::3d10eba5cac0069486bc3c2854d15689[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {
			                ""fragment_size"" : 150,
			                ""number_of_fragments"" : 3,
			                ""no_match_size"": 150
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:772")]
		public void Line772()
		{
			// tag::5ea9da129ca70a5fe534f27a82d80b29[]
			var response0 = new SearchResponse<object>();
			// end::5ea9da129ca70a5fe534f27a82d80b29[]

			response0.MatchesExample(@"PUT /example
			{
			  ""mappings"": {
			    ""properties"": {
			      ""comment"" : {
			        ""type"": ""text"",
			        ""index_options"" : ""offsets""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:790")]
		public void Line790()
		{
			// tag::17a1e308761afd3282f13d44d7be008a[]
			var response0 = new SearchResponse<object>();
			// end::17a1e308761afd3282f13d44d7be008a[]

			response0.MatchesExample(@"PUT /example
			{
			  ""mappings"": {
			    ""properties"": {
			      ""comment"" : {
			        ""type"": ""text"",
			        ""term_vector"" : ""with_positions_offsets""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:812")]
		public void Line812()
		{
			// tag::146bfeeaa2ac4fc1352bf8d41097baa0[]
			var response0 = new SearchResponse<object>();
			// end::146bfeeaa2ac4fc1352bf8d41097baa0[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""query"" : {
			        ""match_phrase"": { ""message"": ""number 1"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""message"" : {
			                ""type"": ""plain"",
			                ""fragment_size"" : 15,
			                ""number_of_fragments"" : 3,
			                ""fragmenter"": ""simple""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:869")]
		public void Line869()
		{
			// tag::bc9bd39420f810edae72b9fb33a154fd[]
			var response0 = new SearchResponse<object>();
			// end::bc9bd39420f810edae72b9fb33a154fd[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""query"" : {
			        ""match_phrase"": { ""message"": ""number 1"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""message"" : {
			                ""type"": ""plain"",
			                ""fragment_size"" : 15,
			                ""number_of_fragments"" : 3,
			                ""fragmenter"": ""span""
			            }
			        }
			    }
			}");
		}
	}
}

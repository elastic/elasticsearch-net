// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Examples.Models;
using Newtonsoft.Json.Linq;

namespace Examples.Search.Request
{
	public class HighlightingPage : ExampleBase
	{
		[U]
		[Description("search/request/highlighting.asciidoc:24")]
		public void Line24()
		{
			// tag::05e1088d2c04391203cc8eb3ab287b71[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("content")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field("content")
					)
				)

			);
			// end::05e1088d2c04391203cc8eb3ab287b71[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""content"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""content"" : {}
			        }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["content"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:279")]
		public void Line279()
		{
			// tag::3cc4e8b1e2aecac644ba52d34ca29422[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.NumberOfFragments(3)
					.FragmentSize(150)
					.Fields(hf => hf
						.Field("body")
						.PreTags("<em>")
						.PostTags("</em>"), hf => hf
						.Field("blog.title")
						.NumberOfFragments(0), hf => hf
						.Field("blog.author")
						.NumberOfFragments(0), hf => hf
						.Field("blog.comment")
						.NumberOfFragments(5)
						.Order(HighlighterOrder.Score)
					)
				)

			);
			// end::3cc4e8b1e2aecac644ba52d34ca29422[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/highlighting.asciidoc:309")]
		public void Line309()
		{
			// tag::977882872876edd3a37c6769ab75b90b[]
			var response0 = new SearchResponse<object>();
			// end::977882872876edd3a37c6769ab75b90b[]

			response0.MatchesExample(@"GET /_search
			{
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
			    ""_source"": false,
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

		[U]
		[Description("search/request/highlighting.asciidoc:377")]
		public void Line377()
		{
			// tag::9e502038aa4ebb9cb4df230c0c4a854e[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(f => f
						.Field("comment")
						.Type(HighlighterType.Plain)
					)
				)
			);
			// end::9e502038aa4ebb9cb4df230c0c4a854e[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""type"" : ""plain""}
			        }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:401")]
		public void Line401()
		{
			// tag::ee079a3f9eb529aac33f09be16747aa9[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.PreTags("<tag1>")
					.PostTags("</tag1>")
					.Fields(f => f
						.Field("body")
					)
				)
			);
			// end::ee079a3f9eb529aac33f09be16747aa9[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:422")]
		public void Line422()
		{
			// tag::a225bb439c204b20ed52a28e1dcd663b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.PreTags("<tag1>", "<tag2>")
					.PostTags("</tag1>", "</tag2>")
					.Fields(f => f
						.Field("body")
					)
				)
			);
			// end::a225bb439c204b20ed52a28e1dcd663b[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:442")]
		public void Line442()
		{
			// tag::05ce63b83a89fddb63fd60c923811582[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.TagsSchema(HighlighterTagsSchema.Styled)
					.Fields(f => f
						.Field("comment")
					)
				)
			);
			// end::05ce63b83a89fddb63fd60c923811582[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:466")]
		public void Line466()
		{
			// tag::87b697eb7340e9e52ca790922eca0066[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(f => f
						.Field("comment")
						.ForceSource()
					)
				)
			);
			// end::87b697eb7340e9e52ca790922eca0066[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""force_source"" : true}
			        }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:490")]
		public void Line490()
		{
			// tag::1e8b687c757981af3a9f005cfd2b4946[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.RequireFieldMatch(false)
					.Fields(f => f
						.Field("body")
						.PreTags("<em>")
						.PostTags("</em>")
					)
				)
			);
			// end::1e8b687c757981af3a9f005cfd2b4946[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:523")]
		public void Line523()
		{
			// tag::a182c91923ad1e47cf502ea890c53015[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("comment.plain:running scissors")
						.Fields("comment")
					)
				)
				.Highlight(h => h
					.Order(HighlighterOrder.Score)
					.Fields(f => f
						.Field("comment")
						.MatchedFields(mf => mf
							.Field("comment")
							.Field("comment.plain")
						)
						.Type(HighlighterType.Fvh)
					)
				)
			);
			// end::a182c91923ad1e47cf502ea890c53015[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("search/request/highlighting.asciidoc:552")]
		public void Line552()
		{
			// tag::974bb1452f614f9a378a695fa9addd4e[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("running scissors")
						.Fields(new[] { "comment", "comment.plain^10" })
					)
				)
				.Highlight(h => h
					.Order(HighlighterOrder.Score)
					.Fields(f => f
						.Field("comment")
						.MatchedFields(mf => mf
							.Field("comment")
							.Field("comment.plain")
						)
						.Type(HighlighterType.Fvh)
					)
				)
			);
			// end::974bb1452f614f9a378a695fa9addd4e[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("search/request/highlighting.asciidoc:579")]
		public void Line579()
		{
			// tag::4971d093f19f85e3c622f1e0257ff60f[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.QueryString(qs => qs
						.Query("running scissors")
						.Fields(new[] { "comment", "comment.plain^10" })
					)
				)
				.Highlight(h => h
					.Order(HighlighterOrder.Score)
					.Fields(hf => hf
						.Field("comment")
						.MatchedFields(mf => mf
							.Field("comment.plain")
						)
						.Type(HighlighterType.Fvh)
					)
				)
			);
			// end::4971d093f19f85e3c622f1e0257ff60f[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U(Skip = "Waiting on PR to support Fields array: https://github.com/elastic/elasticsearch-net/issues/4724")]
		[Description("search/request/highlighting.asciidoc:648")]
		public void Line648()
		{
			// tag::2859fb1a8139777dca087862a5b1c205[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Highlight(h => h
					.Fields(hf => hf
						.Field("title"), hf => hf
						.Field("text")
					)
				)
			);
			// end::2859fb1a8139777dca087862a5b1c205[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""highlight"": {
			        ""fields"": [
			            { ""title"": {} },
			            { ""text"": {} }
			        ]
			    }
			}");
		}

		[U]
		[Description("search/request/highlighting.asciidoc:677")]
		public void Line677()
		{
			// tag::e8446172481fb6298c04b4bdc3340f3f[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(m => m
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field("comment")
						.FragmentSize(150)
						.NumberOfFragments(3)
					)
				)
			);
			// end::e8446172481fb6298c04b4bdc3340f3f[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"": { ""user"": ""kimchy"" }
			    },
			    ""highlight"" : {
			        ""fields"" : {
			            ""comment"" : {""fragment_size"" : 150, ""number_of_fragments"" : 3}
			        }
			    }
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:696")]
		public void Line696()
		{
			// tag::4ae1e4f88af2f9be50696e5a59466bb6[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(mp => mp
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Order(HighlighterOrder.Score)
					.Fields(hf => hf
							.Field("comment")
							.FragmentSize(150)
							.NumberOfFragments(3)
					)
				)
			);
			// end::4ae1e4f88af2f9be50696e5a59466bb6[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:719")]
		public void Line719()
		{
			// tag::62b15eac8c6d294da9114541fdfc527f[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(mp => mp
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field("body"), hf => hf
						.Field("blog.title")
						.NumberOfFragments(0)
					)
				)
			);
			// end::62b15eac8c6d294da9114541fdfc527f[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:745")]
		public void Line745()
		{
			// tag::3d10eba5cac0069486bc3c2854d15689[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Match(mp => mp
						.Field("user")
						.Query("kimchy")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field("comment")
						.FragmentSize(150)
						.NumberOfFragments(3)
						.NoMatchSize(150)
					)
				)
			);
			// end::3d10eba5cac0069486bc3c2854d15689[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match"]["user"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:772")]
		public void Line772()
		{
			// tag::5ea9da129ca70a5fe534f27a82d80b29[]
			var createIndexResponse = client.Indices.Create("example", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("comment")
							.IndexOptions(IndexOptions.Offsets)
						)
					)
				)
			);
			// end::5ea9da129ca70a5fe534f27a82d80b29[]

			createIndexResponse.MatchesExample(@"PUT /example
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

		[U]
		[Description("search/request/highlighting.asciidoc:790")]
		public void Line790()
		{
			// tag::17a1e308761afd3282f13d44d7be008a[]
			var createIndexResponse = client.Indices.Create("example", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("comment")
							.TermVector(TermVectorOption.WithPositionsOffsets)
						)
					)
				)
			);
			// end::17a1e308761afd3282f13d44d7be008a[]

			createIndexResponse.MatchesExample(@"PUT /example
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

		[U]
		[Description("search/request/highlighting.asciidoc:812")]
		public void Line812()
		{
			// tag::146bfeeaa2ac4fc1352bf8d41097baa0[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.MatchPhrase(mp => mp
						.Field(f => f.Message)
						.Query("number 1")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field(f => f.Message)
						.Type(HighlighterType.Plain)
						.FragmentSize(15)
						.NumberOfFragments(3)
						.Fragmenter(HighlighterFragmenter.Simple)
					)

				)
			);
			// end::146bfeeaa2ac4fc1352bf8d41097baa0[]

			searchResponse.MatchesExample(@"GET twitter/_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match_phrase"]["message"].ToLongFormQuery()));
		}

		[U]
		[Description("search/request/highlighting.asciidoc:869")]
		public void Line869()
		{
			// tag::bc9bd39420f810edae72b9fb33a154fd[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q
					.MatchPhrase(mp => mp
						.Field(f => f.Message)
						.Query("number 1")
					)
				)
				.Highlight(h => h
					.Fields(hf => hf
						.Field(f => f.Message)
						.Type(HighlighterType.Plain)
						.FragmentSize(15)
						.NumberOfFragments(3)
						.Fragmenter(HighlighterFragmenter.Span)
					)

				)
			);
			// end::bc9bd39420f810edae72b9fb33a154fd[]

			searchResponse.MatchesExample(@"GET twitter/_search
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
			}", e => e.ApplyBodyChanges(json => json["query"]["match_phrase"]["message"].ToLongFormQuery()));
		}
	}
}

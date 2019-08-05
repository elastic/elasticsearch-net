using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class PercolateQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line18()
		{
			// tag::e79bff3fe9fe9d8732e0b034f17a03c5[]
			var response0 = new SearchResponse<object>();
			// end::e79bff3fe9fe9d8732e0b034f17a03c5[]

			response0.MatchesExample(@"PUT /my-index
			{
			    ""mappings"": {
			        ""properties"": {
			             ""message"": {
			                 ""type"": ""text""
			             },
			             ""query"": {
			                 ""type"": ""percolator""
			             }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line47()
		{
			// tag::25843127c07257bf09154920779d3055[]
			var response0 = new SearchResponse<object>();
			// end::25843127c07257bf09154920779d3055[]

			response0.MatchesExample(@"PUT /my-index/_doc/1?refresh
			{
			    ""query"" : {
			        ""match"" : {
			            ""message"" : ""bonsai tree""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line63()
		{
			// tag::4ef2837148b6b23e2eb0a11d14ccae80[]
			var response0 = new SearchResponse<object>();
			// end::4ef2837148b6b23e2eb0a11d14ccae80[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""percolate"" : {
			            ""field"" : ""query"",
			            ""document"" : {
			                ""message"" : ""A new bonsai tree in the office""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line162()
		{
			// tag::4e4e6a2e173cc20c00cca1a06166a687[]
			var response0 = new SearchResponse<object>();
			// end::4e4e6a2e173cc20c00cca1a06166a687[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""constant_score"": {
			            ""filter"": {
			                ""percolate"" : {
			                    ""field"" : ""query"",
			                    ""document"" : {
			                        ""message"" : ""A new bonsai tree in the office""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line203()
		{
			// tag::2d417d4eea299b45f384af7303252611[]
			var response0 = new SearchResponse<object>();
			// end::2d417d4eea299b45f384af7303252611[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""percolate"" : {
			            ""field"" : ""query"",
			            ""documents"" : [ \<1>
			                {
			                    ""message"" : ""bonsai tree""
			                },
			                {
			                    ""message"" : ""new tree""
			                },
			                {
			                    ""message"" : ""the office""
			                },
			                {
			                    ""message"" : ""office tree""
			                }
			            ]
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line290()
		{
			// tag::fe0b180951e143d4c624d9fbf677b884[]
			var response0 = new SearchResponse<object>();
			// end::fe0b180951e143d4c624d9fbf677b884[]

			response0.MatchesExample(@"PUT /my-index/_doc/2
			{
			  ""message"" : ""A new bonsai tree in the office""
			}");
		}

		[U]
		[SkipExample]
		public void Line322()
		{
			// tag::6736f6e4e04379918a21e7c223c08cf9[]
			var response0 = new SearchResponse<object>();
			// end::6736f6e4e04379918a21e7c223c08cf9[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""percolate"" : {
			            ""field"": ""query"",
			            ""index"" : ""my-index"",
			            ""id"" : ""2"",
			            ""version"" : 1 \<1>
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line359()
		{
			// tag::f33cfd0350f5f474362aa6f2e03f734f[]
			var response0 = new SearchResponse<object>();
			// end::f33cfd0350f5f474362aa6f2e03f734f[]

			response0.MatchesExample(@"PUT /my-index/_doc/3?refresh
			{
			    ""query"" : {
			        ""match"" : {
			            ""message"" : ""brown fox""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line375()
		{
			// tag::1ae1587dfc299b9f3f57d3da0dbc9a3b[]
			var response0 = new SearchResponse<object>();
			// end::1ae1587dfc299b9f3f57d3da0dbc9a3b[]

			response0.MatchesExample(@"PUT /my-index/_doc/4?refresh
			{
			    ""query"" : {
			        ""match"" : {
			            ""message"" : ""lazy dog""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line391()
		{
			// tag::a8852f083978b748b93b87ff7fa7b15b[]
			var response0 = new SearchResponse<object>();
			// end::a8852f083978b748b93b87ff7fa7b15b[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""percolate"" : {
			            ""field"": ""query"",
			            ""document"" : {
			                ""message"" : ""The quick brown fox jumps over the lazy dog""
			            }
			        }
			    },
			    ""highlight"": {
			      ""fields"": {
			        ""message"": {}
			      }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line488()
		{
			// tag::3bbf150f4ae5c8e53beb6d6ae6f07775[]
			var response0 = new SearchResponse<object>();
			// end::3bbf150f4ae5c8e53beb6d6ae6f07775[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""percolate"" : {
			            ""field"": ""query"",
			            ""documents"" : [
			                {
			                    ""message"" : ""bonsai tree""
			                },
			                {
			                    ""message"" : ""new tree""
			                },
			                {
			                    ""message"" : ""the office""
			                },
			                {
			                    ""message"" : ""office tree""
			                }
			            ]
			        }
			    },
			    ""highlight"": {
			      ""fields"": {
			        ""message"": {}
			      }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line582()
		{
			// tag::6163e92fa93136a1907f820e8d57db45[]
			var response0 = new SearchResponse<object>();
			// end::6163e92fa93136a1907f820e8d57db45[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"" : {
			        ""bool"" : {
			            ""should"" : [
			                {
			                    ""percolate"" : {
			                        ""field"" : ""query"",
			                        ""document"" : {
			                            ""message"" : ""bonsai tree""
			                        },
			                        ""name"": ""query1"" \<1>
			                    }
			                },
			                {
			                    ""percolate"" : {
			                        ""field"" : ""query"",
			                        ""document"" : {
			                            ""message"" : ""tulip flower""
			                        },
			                        ""name"": ""query2"" \<1>
			                    }
			                }
			            ]
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line688()
		{
			// tag::9501e6c8e95c21838653ea15b9b7ed5f[]
			var response0 = new SearchResponse<object>();
			// end::9501e6c8e95c21838653ea15b9b7ed5f[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""term"" : {
			      ""query.extraction_result"" : ""failed""
			    }
			  }
			}");
		}
	}
}
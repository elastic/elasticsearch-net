using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class ReindexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::0cc991e3f7f8511a34730e154b3c5edc[]
			var response0 = new SearchResponse<object>();
			// end::0cc991e3f7f8511a34730e154b3c5edc[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line65()
		{
			// tag::9ebee1ff26ac0f91321d1d7596c05643[]
			var response0 = new SearchResponse<object>();
			// end::9ebee1ff26ac0f91321d1d7596c05643[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter"",
			    ""version_type"": ""internal""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::65adbf941edfe6efcfaec4754230ff47[]
			var response0 = new SearchResponse<object>();
			// end::65adbf941edfe6efcfaec4754230ff47[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter"",
			    ""version_type"": ""external""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line106()
		{
			// tag::8a104f3787e778cdde057d5e2f4eb919[]
			var response0 = new SearchResponse<object>();
			// end::8a104f3787e778cdde057d5e2f4eb919[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter"",
			    ""op_type"": ""create""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line128()
		{
			// tag::dfa9ebfd5216280a42312ea09f73d74d[]
			var response0 = new SearchResponse<object>();
			// end::dfa9ebfd5216280a42312ea09f73d74d[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""conflicts"": ""proceed"",
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter"",
			    ""op_type"": ""create""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line148()
		{
			// tag::764f9884b370cbdc82a1c5c42ed40ff3[]
			var response0 = new SearchResponse<object>();
			// end::764f9884b370cbdc82a1c5c42ed40ff3[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter"",
			    ""query"": {
			      ""term"": {
			        ""user"": ""kimchy""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line172()
		{
			// tag::6f097c298a7abf4c032c4314920c49c8[]
			var response0 = new SearchResponse<object>();
			// end::6f097c298a7abf4c032c4314920c49c8[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": [""twitter"", ""blog""]
			  },
			  ""dest"": {
			    ""index"": ""all_together""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line197()
		{
			// tag::52b2bfbdd78f8283b6f4891c48013237[]
			var response0 = new SearchResponse<object>();
			// end::52b2bfbdd78f8283b6f4891c48013237[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""max_docs"": 1,
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line218()
		{
			// tag::96064cf450ccd198ac03cd0c33d3be3d[]
			var response0 = new SearchResponse<object>();
			// end::96064cf450ccd198ac03cd0c33d3be3d[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""max_docs"": 10000,
			  ""source"": {
			    ""index"": ""twitter"",
			    ""sort"": { ""date"": ""desc"" }
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line240()
		{
			// tag::e9c2e15b36372d5281c879d336322b6c[]
			var response0 = new SearchResponse<object>();
			// end::e9c2e15b36372d5281c879d336322b6c[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter"",
			    ""_source"": [""user"", ""_doc""]
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line261()
		{
			// tag::8871b8fcb6de4f0c7dff22798fb10fb7[]
			var response0 = new SearchResponse<object>();
			// end::8871b8fcb6de4f0c7dff22798fb10fb7[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter"",
			    ""version_type"": ""external""
			  },
			  ""script"": {
			    ""source"": ""if (ctx._source.foo == 'bar') {ctx._version++; ctx._source.remove('foo')}"",
			    ""lang"": ""painless""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line334()
		{
			// tag::78c96113ae4ed0054e581b17542528a7[]
			var response0 = new SearchResponse<object>();
			// end::78c96113ae4ed0054e581b17542528a7[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""company"": ""cat""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest"",
			    ""routing"": ""=cat""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line358()
		{
			// tag::400e89eb46ead8e9c9e40f123fd5e590[]
			var response0 = new SearchResponse<object>();
			// end::400e89eb46ead8e9c9e40f123fd5e590[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""source"",
			    ""size"": 100
			  },
			  ""dest"": {
			    ""index"": ""dest"",
			    ""routing"": ""=cat""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line378()
		{
			// tag::b1efa1c51a34dd5ab5511b71a399f5b1[]
			var response0 = new SearchResponse<object>();
			// end::b1efa1c51a34dd5ab5511b71a399f5b1[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""source""
			  },
			  ""dest"": {
			    ""index"": ""dest"",
			    ""pipeline"": ""some_ingest_pipeline""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line400()
		{
			// tag::36b2778f23d0955255f52c075c4d213d[]
			var response0 = new SearchResponse<object>();
			// end::36b2778f23d0955255f52c075c4d213d[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://otherhost:9200"",
			      ""username"": ""user"",
			      ""password"": ""pass""
			    },
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line468()
		{
			// tag::64b9baa6d7556b960b29698f3383aa31[]
			var response0 = new SearchResponse<object>();
			// end::64b9baa6d7556b960b29698f3383aa31[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://otherhost:9200""
			    },
			    ""index"": ""source"",
			    ""size"": 10,
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line500()
		{
			// tag::7f697eb436dfa3c30dfe610d8c32d132[]
			var response0 = new SearchResponse<object>();
			// end::7f697eb436dfa3c30dfe610d8c32d132[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://otherhost:9200"",
			      ""socket_timeout"": ""1m"",
			      ""connect_timeout"": ""10s""
			    },
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line790()
		{
			// tag::973ff58c444e48580971b7203d304502[]
			var response0 = new SearchResponse<object>();
			// end::973ff58c444e48580971b7203d304502[]

			response0.MatchesExample(@"GET _tasks?detailed=true&actions=*reindex");
		}

		[U(Skip = "Example not implemented")]
		public void Line855()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var response0 = new SearchResponse<object>();
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			response0.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U(Skip = "Example not implemented")]
		public void Line877()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var response0 = new SearchResponse<object>();
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			response0.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}

		[U(Skip = "Example not implemented")]
		public void Line896()
		{
			// tag::68738b4fd0dda177022be45be95b4c84[]
			var response0 = new SearchResponse<object>();
			// end::68738b4fd0dda177022be45be95b4c84[]

			response0.MatchesExample(@"POST _reindex/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U(Skip = "Example not implemented")]
		public void Line918()
		{
			// tag::1577e6e806b3283c9e99f1596d310754[]
			var response0 = new SearchResponse<object>();
			// end::1577e6e806b3283c9e99f1596d310754[]

			response0.MatchesExample(@"POST test/_doc/1?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""foo""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line931()
		{
			// tag::1216f8f7367df3aa823012cef310c08a[]
			var response0 = new SearchResponse<object>();
			// end::1216f8f7367df3aa823012cef310c08a[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""test""
			  },
			  ""dest"": {
			    ""index"": ""test2""
			  },
			  ""script"": {
			    ""source"": ""ctx._source.tag = ctx._source.remove(\""flag\"")""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line951()
		{
			// tag::cfc37446bd892d1ac42a3c8e8b204e6c[]
			var response0 = new SearchResponse<object>();
			// end::cfc37446bd892d1ac42a3c8e8b204e6c[]

			response0.MatchesExample(@"GET test2/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line996()
		{
			// tag::1b8655e6ba99fe39933c6eafe78728b7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1b8655e6ba99fe39933c6eafe78728b7[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter"",
			    ""slice"": {
			      ""id"": 0,
			      ""max"": 2
			    }
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");

			response1.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter"",
			    ""slice"": {
			      ""id"": 1,
			      ""max"": 2
			    }
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line1030()
		{
			// tag::3ae03ba3b56e5e287953094050766738[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3ae03ba3b56e5e287953094050766738[]

			response0.MatchesExample(@"GET _refresh");

			response1.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total");
		}

		[U(Skip = "Example not implemented")]
		public void Line1060()
		{
			// tag::cb01106bf524df5e0501d4c655c1aa7b[]
			var response0 = new SearchResponse<object>();
			// end::cb01106bf524df5e0501d4c655c1aa7b[]

			response0.MatchesExample(@"POST _reindex?slices=5&refresh
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line1077()
		{
			// tag::e567e6dbf86300142573c73789c8fce4[]
			var response0 = new SearchResponse<object>();
			// end::e567e6dbf86300142573c73789c8fce4[]

			response0.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total");
		}

		[U(Skip = "Example not implemented")]
		public void Line1182()
		{
			// tag::9a4d5e41c52c20635d1fd9c6e13f6c7a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9a4d5e41c52c20635d1fd9c6e13f6c7a[]

			response0.MatchesExample(@"PUT metricbeat-2016.05.30/_doc/1?refresh
			{""system.cpu.idle.pct"": 0.908}");

			response1.MatchesExample(@"PUT metricbeat-2016.05.31/_doc/1?refresh
			{""system.cpu.idle.pct"": 0.105}");
		}

		[U(Skip = "Example not implemented")]
		public void Line1199()
		{
			// tag::973a3ff47fc4ce036ecd9bd363fef9f7[]
			var response0 = new SearchResponse<object>();
			// end::973a3ff47fc4ce036ecd9bd363fef9f7[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""metricbeat-*""
			  },
			  ""dest"": {
			    ""index"": ""metricbeat""
			  },
			  ""script"": {
			    ""lang"": ""painless"",
			    ""source"": ""ctx._index = 'metricbeat-' + (ctx._index.substring('metricbeat-'.length(), ctx._index.length())) + '-1'""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line1220()
		{
			// tag::3b04cc894e6a47d57983484010feac0c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3b04cc894e6a47d57983484010feac0c[]

			response0.MatchesExample(@"GET metricbeat-2016.05.30-1/_doc/1");

			response1.MatchesExample(@"GET metricbeat-2016.05.31-1/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line1236()
		{
			// tag::8b33c9257041fabad8cea43fa049f98f[]
			var response0 = new SearchResponse<object>();
			// end::8b33c9257041fabad8cea43fa049f98f[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""max_docs"": 10,
			  ""source"": {
			    ""index"": ""twitter"",
			    ""query"": {
			      ""function_score"" : {
			        ""query"" : { ""match_all"": {} },
			        ""random_score"" : {}
			      }
			    },
			    ""sort"": ""_score""    \<1>
			  },
			  ""dest"": {
			    ""index"": ""random_twitter""
			  }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class ReindexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line20()
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
		public void Line161()
		{
			// tag::68738b4fd0dda177022be45be95b4c84[]
			var response0 = new SearchResponse<object>();
			// end::68738b4fd0dda177022be45be95b4c84[]

			response0.MatchesExample(@"POST _reindex/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U(Skip = "Example not implemented")]
		public void Line191()
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
		public void Line224()
		{
			// tag::3ae03ba3b56e5e287953094050766738[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3ae03ba3b56e5e287953094050766738[]

			response0.MatchesExample(@"GET _refresh");

			response1.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total");
		}

		[U(Skip = "Example not implemented")]
		public void Line251()
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
		public void Line267()
		{
			// tag::e567e6dbf86300142573c73789c8fce4[]
			var response0 = new SearchResponse<object>();
			// end::e567e6dbf86300142573c73789c8fce4[]

			response0.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total");
		}

		[U(Skip = "Example not implemented")]
		public void Line359()
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
		public void Line384()
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
		public void Line403()
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
		public void Line592()
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
		public void Line618()
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
		public void Line640()
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
		public void Line666()
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
		public void Line687()
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
		public void Line699()
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
		public void Line718()
		{
			// tag::cfc37446bd892d1ac42a3c8e8b204e6c[]
			var response0 = new SearchResponse<object>();
			// end::cfc37446bd892d1ac42a3c8e8b204e6c[]

			response0.MatchesExample(@"GET test2/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line751()
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
		public void Line767()
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
		public void Line787()
		{
			// tag::3b04cc894e6a47d57983484010feac0c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3b04cc894e6a47d57983484010feac0c[]

			response0.MatchesExample(@"GET metricbeat-2016.05.30-1/_doc/1");

			response1.MatchesExample(@"GET metricbeat-2016.05.31-1/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line802()
		{
			// tag::1bc731a4df952228af6dfa6b48627332[]
			var response0 = new SearchResponse<object>();
			// end::1bc731a4df952228af6dfa6b48627332[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""max_docs"": 10,
			  ""source"": {
			    ""index"": ""twitter"",
			    ""query"": {
			      ""function_score"" : {
			        ""random_score"" : {},
			        ""min_score"" : 0.9    <1>
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""random_twitter""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line833()
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
		public void Line888()
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
		public void Line955()
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
		public void Line986()
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
	}
}
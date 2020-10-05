// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Elastic.Transport;

namespace Examples.Docs
{
	public class ReindexPage : ExampleBase
	{
		[U]
		[Description("docs/reindex.asciidoc:25")]
		public void Line25()
		{
			// tag::0cc991e3f7f8511a34730e154b3c5edc[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter"))
				 .Destination(d => d.Index("new_twitter"))
			);
			// end::0cc991e3f7f8511a34730e154b3c5edc[]

			reindexResponse.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U]
		[Description("docs/reindex.asciidoc:174")]
		public void Line174()
		{
			// tag::68738b4fd0dda177022be45be95b4c84[]
			var reindexResponse = client.ReindexRethrottle("r1A2WoRbTwKZ516z6NEs5A:36619", d =>
				d.RequestsPerSecond(-1)
			);
			// end::68738b4fd0dda177022be45be95b4c84[]

			reindexResponse.MatchesExample(@"POST _reindex/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U]
		[Description("docs/reindex.asciidoc:204")]
		public void Line204()
		{
			// tag::1b8655e6ba99fe39933c6eafe78728b7[]
			var reindexResponse1 = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter").Slice<Tweet>(slice => slice.Id(0).Max(2)))
				 .Destination(d => d.Index("new_twitter"))
			);

			var reindexResponse2 = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter").Slice<Tweet>(slice => slice.Id(1).Max(2)))
				 .Destination(d => d.Index("new_twitter"))
			);
			// end::1b8655e6ba99fe39933c6eafe78728b7[]

			reindexResponse1.MatchesExample(@"POST _reindex
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

			reindexResponse2.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:237")]
		public void Line237()
		{
			// tag::3ae03ba3b56e5e287953094050766738[]
			var refreshResponse = client.Indices.Refresh();

			var searchResponse = client.Search<Tweet>(s => s.Index("new_twitter").Size(0).FilterPath(new[] { "hits.total" }));
			// end::3ae03ba3b56e5e287953094050766738[]

			refreshResponse.MatchesExample(@"GET _refresh", e =>
			{
				e.Method = HttpMethod.POST;
			});

			searchResponse.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:264")]
		public void Line264()
		{
			// tag::cb01106bf524df5e0501d4c655c1aa7b[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter"))
				 .Destination(d => d.Index("new_twitter"))
				 .Slices(5)
				 .Refresh()
			);
			// end::cb01106bf524df5e0501d4c655c1aa7b[]

			reindexResponse.MatchesExample(@"POST _reindex?slices=5&refresh
			{
			  ""source"": {
			    ""index"": ""twitter""
			  },
			  ""dest"": {
			    ""index"": ""new_twitter""
			  }
			}");
		}

		[U]
		[Description("docs/reindex.asciidoc:280")]
		public void Line280()
		{
			// tag::e567e6dbf86300142573c73789c8fce4[]
			var searchResponse = client.Search<Tweet>(s => s.Index("new_twitter").Size(0).FilterPath(new[] { "hits.total" }));
			// end::e567e6dbf86300142573c73789c8fce4[]

			searchResponse.MatchesExample(@"POST new_twitter/_search?size=0&filter_path=hits.total", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:373")]
		public void Line373()
		{
			// tag::78c96113ae4ed0054e581b17542528a7[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("source").Query<object>(q => q.Match(m => m.Field("company").Query("cat"))))
				 .Destination(d => d.Index("dest").Routing("=cat"))
				 .Slices(5)
				 .Refresh()
			);
			// end::78c96113ae4ed0054e581b17542528a7[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", (e, body) =>
			{
				body["source"]["query"]["match"]["company"].ToLongFormQuery();
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:398")]
		public void Line398()
		{
			// tag::400e89eb46ead8e9c9e40f123fd5e590[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("source").Size(100))
				 .Destination(d => d.Index("dest").Routing("=cat"))
			);
			// end::400e89eb46ead8e9c9e40f123fd5e590[]

			reindexResponse.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:417")]
		public void Line417()
		{
			// tag::b1efa1c51a34dd5ab5511b71a399f5b1[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("source"))
				 .Destination(d => d.Index("dest").Pipeline("some_ingest_pipeline"))
			);
			// end::b1efa1c51a34dd5ab5511b71a399f5b1[]

			reindexResponse.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:609")]
		public void Line609()
		{
			// tag::764f9884b370cbdc82a1c5c42ed40ff3[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter").Query<Tweet>(q => q.Term(f => f.User, "kimchy")))
				 .Destination(d => d.Index("new_twitter"))
			);
			// end::764f9884b370cbdc82a1c5c42ed40ff3[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", (e, body) =>
			{
				body["source"]["query"]["term"]["user"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:635")]
		public void Line635()
		{
			// tag::52b2bfbdd78f8283b6f4891c48013237[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter"))
				 .Destination(d => d.Index("new_twitter"))
				 .MaximumDocuments(1)
			);
			// end::52b2bfbdd78f8283b6f4891c48013237[]

			reindexResponse.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:657")]
		public void Line657()
		{
			// tag::6f097c298a7abf4c032c4314920c49c8[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index(new[] { "twitter", "blog" }))
				 .Destination(d => d.Index("all_together"))
			);
			// end::6f097c298a7abf4c032c4314920c49c8[]

			reindexResponse.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": [""twitter"", ""blog""]
			  },
			  ""dest"": {
			    ""index"": ""all_together""
			  }
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["source"]["index"] = b["source"]["index"].Flatten<string>(); });
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:683")]
		public void Line683()
		{
			// tag::e9c2e15b36372d5281c879d336322b6c[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter").Source<object>(source => source.Fields("user", "_doc")))
				 .Destination(d => d.Index("new_twitter"))
			);
			// end::e9c2e15b36372d5281c879d336322b6c[]

			reindexResponse.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:704")]
		public void Line704()
		{
			// tag::1577e6e806b3283c9e99f1596d310754[]
			var indexResponse = client.Index(new { text = "words words", flag = "foo" },
				i => i.Index("test").Id(1).Refresh(Refresh.True));
			// end::1577e6e806b3283c9e99f1596d310754[]

			indexResponse.MatchesExample(@"POST test/_doc/1?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""foo""
			}", e =>
			{
				e.Method = HttpMethod.PUT;
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:716")]
		public void Line716()
		{
			// tag::1216f8f7367df3aa823012cef310c08a[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("test"))
				 .Destination(d => d.Index("test2"))
				 .Script(@"ctx._source.tag = ctx._source.remove(""flag"")")
			);
			// end::1216f8f7367df3aa823012cef310c08a[]

			reindexResponse.MatchesExample(@"POST _reindex
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

		[U]
		[Description("docs/reindex.asciidoc:735")]
		public void Line735()
		{
			// tag::cfc37446bd892d1ac42a3c8e8b204e6c[]
			var getResponse = client.Get<object>(1, d => d.Index("test2"));
			// end::cfc37446bd892d1ac42a3c8e8b204e6c[]

			getResponse.MatchesExample(@"GET test2/_doc/1");
		}

		[U]
		[Description("docs/reindex.asciidoc:768")]
		public void Line768()
		{
			// tag::9a4d5e41c52c20635d1fd9c6e13f6c7a[]
			var indexResponse1 = client.Index(new Dictionary<string, double> { { "system.cpu.idle.pct", 0.908 } },
				i => i.Index("metricbeat-2016.05.30").Id(1).Refresh(Refresh.True));

			var indexResponse2 = client.Index(new Dictionary<string, double> { { "system.cpu.idle.pct", 0.105 } },
				i => i.Index("metricbeat-2016.05.31").Id(1).Refresh(Refresh.True));
			// end::9a4d5e41c52c20635d1fd9c6e13f6c7a[]

			indexResponse1.MatchesExample(@"PUT metricbeat-2016.05.30/_doc/1?refresh
			{""system.cpu.idle.pct"": 0.908}");

			indexResponse2.MatchesExample(@"PUT metricbeat-2016.05.31/_doc/1?refresh
			{""system.cpu.idle.pct"": 0.105}");
		}

		[U]
		[Description("docs/reindex.asciidoc:784")]
		public void Line784()
		{
			// tag::973a3ff47fc4ce036ecd9bd363fef9f7[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("metricbeat-*"))
				 .Destination(d => d.Index("metricbeat"))
				 .Script(@"ctx._index = 'metricbeat-' + (ctx._index.substring('metricbeat-'.length(), ctx._index.length())) + '-1'")
			);
			// end::973a3ff47fc4ce036ecd9bd363fef9f7[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", e =>
			{
				e.ApplyBodyChanges(o => ((JObject)o["script"]).Remove("lang"));
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:804")]
		public void Line804()
		{
			// tag::3b04cc894e6a47d57983484010feac0c[]
			var getResponse1 = client.Get<object>(1, i => i.Index("metricbeat-2016.05.30-1"));

			var getResponse2 = client.Get<object>(1, i => i.Index("metricbeat-2016.05.31-1"));
			// end::3b04cc894e6a47d57983484010feac0c[]

			getResponse1.MatchesExample(@"GET metricbeat-2016.05.30-1/_doc/1");

			getResponse2.MatchesExample(@"GET metricbeat-2016.05.31-1/_doc/1");
		}

		[U]
		[Description("docs/reindex.asciidoc:819")]
		public void Line819()
		{
			// tag::1bc731a4df952228af6dfa6b48627332[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.MaximumDocuments(10)
				 .Source(s => s.Index("twitter").Query<object>(q => q.FunctionScore(f => f.Functions(ff => ff.RandomScore()).MinScore(0.9))))
				 .Destination(d => d.Index("random_twitter"))
			);
			// end::1bc731a4df952228af6dfa6b48627332[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", e =>
			{
				e.ApplyBodyChanges(o =>
				{
					((JObject)o["source"]["query"]["function_score"]).Remove("random_score");
					var array = new JArray { JToken.FromObject(new { random_score = new object() }) };
					((JObject)o["source"]["query"]["function_score"]).Add("functions", array);
				});
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:850")]
		public void Line850()
		{
			// tag::8871b8fcb6de4f0c7dff22798fb10fb7[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s.Index("twitter"))
				 .Destination(d => d.Index("new_twitter").VersionType(VersionType.External))
				 .Script(@"if (ctx._source.foo == 'bar') {ctx._version++; ctx._source.remove('foo')}")
			);
			// end::8871b8fcb6de4f0c7dff22798fb10fb7[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", e =>
			{
				e.ApplyBodyChanges(o => ((JObject)o["script"]).Remove("lang"));
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:905")]
		public void Line905()
		{
			// tag::36b2778f23d0955255f52c075c4d213d[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s
						.Index("source")
						.Remote(rr => rr
							.Host(new Uri("http://otherhost:9200"))
							.Username("user")
							.Password("pass"))
						.Query<object>(q => q.Match(m => m.Field("test").Query("data"))))
				 .Destination(d => d.Index("dest"))
			);
			// end::36b2778f23d0955255f52c075c4d213d[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", (e, body) =>
			{
				body["source"]["query"]["match"]["test"].ToLongFormQuery();
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:971")]
		public void Line971()
		{
			// tag::64b9baa6d7556b960b29698f3383aa31[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s
						.Remote(rr => rr.Host(new Uri("http://otherhost:9200")))
						.Index("source")
						.Size(10)
						.Query<object>(q => q.Match(m => m.Field("test").Query("data"))))
					.Destination(d => d.Index("dest"))
			);
			// end::64b9baa6d7556b960b29698f3383aa31[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", (e, body) =>
			{
				body["source"]["query"]["match"]["test"].ToLongFormQuery();
			});
		}

		[U]
		[Description("docs/reindex.asciidoc:1002")]
		public void Line1002()
		{
			// tag::7f697eb436dfa3c30dfe610d8c32d132[]
			var reindexResponse = client.ReindexOnServer(r =>
				r.Source(s => s
						.Index("source")
						.Remote(rr => rr
							.Host(new Uri("http://otherhost:9200"))
							.SocketTimeout("1m")
							.ConnectTimeout("10s"))
						.Query<object>(q => q.Match(m => m.Field("test").Query("data"))))
					.Destination(d => d.Index("dest"))
			);
			// end::7f697eb436dfa3c30dfe610d8c32d132[]

			reindexResponse.MatchesExample(@"POST _reindex
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
			}", (e, body) =>
			{
				body["source"]["query"]["match"]["test"].ToLongFormQuery();
			});
		}
	}
}

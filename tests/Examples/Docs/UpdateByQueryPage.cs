// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Elastic.Transport;

namespace Examples.Docs
{
	public class UpdateByQueryPage : ExampleBase
	{
		[U]
		[Description("docs/update-by-query.asciidoc:12")]
		public void Line12()
		{
			// tag::a4a396cd07657b3977713fb3a742c41b[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Conflicts(Conflicts.Proceed)
			);
			// end::a4a396cd07657b3977713fb3a742c41b[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?conflicts=proceed");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:307")]
		public void Line307()
		{
			// tag::52a87b81e4e0b6b11e23e85db1602a63[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Conflicts(Conflicts.Proceed)
				.Query(q => q
					.Term(f => f.User, "kimchy")
				)
			);
			// end::52a87b81e4e0b6b11e23e85db1602a63[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?conflicts=proceed
			{
			  ""query"": { \<1>
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					var value = body["query"]["term"]["user"];
					body["query"]["term"]["user"] = new JObject { ["value"] = value };
				});
			});
		}

		[U]
		[Description("docs/update-by-query.asciidoc:326")]
		public void Line326()
		{
			// tag::cde4dddae5c06e7f1d38c9d933dbc7ac[]
			var updateByQueryResponse = client.UpdateByQuery<object>(u => u
				.Index(new[] { "twitter", "blog" })
			);
			// end::cde4dddae5c06e7f1d38c9d933dbc7ac[]

			updateByQueryResponse.MatchesExample(@"POST twitter,blog/_update_by_query");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:334")]
		public void Line334()
		{
			// tag::d8b115341da772a628a024e7d1644e73[]
			var updateByQueryResponse = client.UpdateByQuery<object>(u => u
				.Index("twitter")
				.Routing(1)
			);
			// end::d8b115341da772a628a024e7d1644e73[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?routing=1");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:343")]
		public void Line343()
		{
			// tag::54a770f053f3225ea0d1e34334232411[]
			var updateByQueryResponse = client.UpdateByQuery<object>(u => u
				.Index("twitter")
				.ScrollSize(100)
			);
			// end::54a770f053f3225ea0d1e34334232411[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?scroll_size=100");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:355")]
		public void Line355()
		{
			// tag::2fd69fb0538e4f36ac69a8b8f8bf5ae8[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Script(s => s
					.Source("ctx._source.likes++")
					.Lang("painless")
				)
				.Query(q => q
					.Term(f => f.User, "kimchy")
				)
			);
			// end::2fd69fb0538e4f36ac69a8b8f8bf5ae8[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""script"": {
			    ""source"": ""ctx._source.likes++"",
			    ""lang"": ""painless""
			  },
			  ""query"": {
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					var value = body["query"]["term"]["user"];
					body["query"]["term"]["user"] = new JObject { ["value"] = value };
				});
			});
		}

		[U]
		[Description("docs/update-by-query.asciidoc:396")]
		public void Line396()
		{
			// tag::c4b278ba293abd0d02a0b5ad1a99f84a[]
			var putPipelineResponse = client.Ingest.PutPipeline("set-foo", p => p
				.Description("sets foo")
				.Processors(pr => pr
					.Set<object>(s => s
						.Field("foo")
						.Value("bar")
					)
				)
			);

			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Pipeline("set-foo")
			);
			// end::c4b278ba293abd0d02a0b5ad1a99f84a[]

			putPipelineResponse.MatchesExample(@"PUT _ingest/pipeline/set-foo
			{
			  ""description"" : ""sets foo"",
			  ""processors"" : [ {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			  } ]
			}");

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?pipeline=set-foo");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:420")]
		public void Line420()
		{
			// tag::7df191cc7f814e410a4ac7261065e6ef[]
			var listTasksResponse = client.Tasks.List(t => t
				.Detailed()
				.Actions("*byquery")
			);
			// end::7df191cc7f814e410a4ac7261065e6ef[]

			listTasksResponse.MatchesExample(@"GET _tasks?detailed=true&actions=*byquery");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:478")]
		public void Line478()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var getTaskResponse = client.Tasks.GetTask("r1A2WoRbTwKZ516z6NEs5A:36619");
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			getTaskResponse.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:498")]
		public void Line498()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var cancelTasksResponse = client.Tasks.Cancel(c => c
				.TaskId("r1A2WoRbTwKZ516z6NEs5A:36619")
			);
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			cancelTasksResponse.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:517")]
		public void Line517()
		{
			// tag::bdb30dd52d32f50994008f4f9c0da5f0[]
			var updateByQueryRethrottleResponse = client.UpdateByQueryRethrottle(
				"r1A2WoRbTwKZ516z6NEs5A:36619", r => r
				.RequestsPerSecond(-1)
			);
			// end::bdb30dd52d32f50994008f4f9c0da5f0[]

			updateByQueryRethrottleResponse.MatchesExample(@"POST _update_by_query/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:537")]
		public void Line537()
		{
			// tag::0d664883151008b1051ef2c9ab2d0373[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Slice(s => s
					.Id(0)
					.Max(2)
				)
				.Script(s => s
					.Source("ctx._source['extra'] = 'test'")
				)
			);

			var updateByQueryResponse2 = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Slice(s => s
					.Id(1)
					.Max(2)
				)
				.Script(s => s
					.Source("ctx._source['extra'] = 'test'")
				)
			);
			// end::0d664883151008b1051ef2c9ab2d0373[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""slice"": {
			    ""id"": 0,
			    ""max"": 2
			  },
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");

			updateByQueryResponse2.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""slice"": {
			    ""id"": 1,
			    ""max"": 2
			  },
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:564")]
		public void Line564()
		{
			// tag::4acf902c2598b2558f34f20c1744c433[]
			var refreshResponse = client.Indices.Refresh();

			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(0)
				.QueryOnQueryString("extra:test")
				.FilterPath(new[] { "hits.total" }) // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::4acf902c2598b2558f34f20c1744c433[]

			refreshResponse.MatchesExample(@"GET _refresh", e =>
			{
				e.Method = HttpMethod.POST;
			});

			searchResponse.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("docs/update-by-query.asciidoc:593")]
		public void Line593()
		{
			// tag::ea02de2dbe05091fcb0dac72c8ba5f83[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("twitter")
				.Slices(5)
				.Script(s => s
					.Source("ctx._source['extra'] = 'test'")
				)
				.Refresh()
			);
			// end::ea02de2dbe05091fcb0dac72c8ba5f83[]

			updateByQueryResponse.MatchesExample(@"POST twitter/_update_by_query?refresh&slices=5
			{
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:606")]
		public void Line606()
		{
			// tag::025b54db0edc50c24ea48a2bd94366ad[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(0)
				.QueryOnQueryString("extra:test")
				.FilterPath(new[] { "hits.total" })
			);
			// end::025b54db0edc50c24ea48a2bd94366ad[]

			searchResponse.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total", e =>
			{
				e.MoveQueryStringToBody("size", 0);
			});
		}

		[U]
		[Description("docs/update-by-query.asciidoc:662")]
		public void Line662()
		{
			// tag::2fe28d9a91b3081a9ec4601af8fb7b1c[]
			var createIndexResponse = client.Indices.Create("test", c => c
				.Map(m => m
					.Dynamic(false)
					.Properties(p => p.Text(t => t.Name("text")))
				)
			);

			var indexResponse1 = client.Index(new
			{
				Text = "words words",
				Flag = "bar"
			},
				i => i
					.Index("test")
			);

			var indexResponse2 = client.Index(new
			{
				Text = "words words",
				Flag = "foo"
			},
				i => i
					.Index("test")
			);

			var putMappingResponse = client.Map<object>(c =>
				c.Index("test")
					.Properties(p =>
						p.Text(t => t.Name("text"))
						 .Text(t => t.Name("flag").Analyzer("keyword"))
					)
			);
			// end::2fe28d9a91b3081a9ec4601af8fb7b1c[]

			createIndexResponse.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""dynamic"": false,   \<1>
			    ""properties"": {
			      ""text"": {""type"": ""text""}
			    }
			  }
			}");

			indexResponse1.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""bar""
			}");

			indexResponse2.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""foo""
			}");

			putMappingResponse.MatchesExample(@"PUT test/_mapping   \<2>
			{
			  ""properties"": {
			    ""text"": {""type"": ""text""},
			    ""flag"": {""type"": ""text"", ""analyzer"": ""keyword""}
			  }
			}");
		}

		[U]
		[Description("docs/update-by-query.asciidoc:700")]
		public void Line700()
		{
			// tag::abd4fc3ce7784413a56fe2dcfe2809b5[]
			var searchResponse = client.Search<object>(s => s
				.Index("test")
				.Query(q => q
					.Match(m => m
						.Field("flag")
						.Query("foo")
					)
				)
				.FilterPath(new[] { "hits.total" })
			);
			// end::abd4fc3ce7784413a56fe2dcfe2809b5[]

			searchResponse.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["query"]["match"]["flag"].ToLongFormQuery();
				});
			});
		}

		[U]
		[Description("docs/update-by-query.asciidoc:727")]
		public void Line727()
		{
			// tag::97babc8d19ef0866774576716eb6d19e[]
			var updateByQueryResponse = client.UpdateByQuery<Tweet>(u => u
				.Index("test")
				.Conflicts(Conflicts.Proceed)
				.Refresh()
			);

			var searchResponse = client.Search<Tweet>(s => s
				.Index("test")
				.Query(q => q
					.Match(m => m
						.Field("flag")
						.Query("foo")
					)
				)
				.FilterPath(new[] { "hits.total" })
			);
			// end::97babc8d19ef0866774576716eb6d19e[]

			updateByQueryResponse.MatchesExample(@"POST test/_update_by_query?refresh&conflicts=proceed");

			searchResponse.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["query"]["match"]["flag"].ToLongFormQuery();
				});
			});
		}
	}
}

using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;
using Newtonsoft.Json.Linq;

namespace Examples.Docs
{
	public class UpdateByQueryPage : ExampleBase
	{
		[U]
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
		public void Line298()
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
				return e;
			});
		}

		[U]
		public void Line318()
		{
			// tag::cde4dddae5c06e7f1d38c9d933dbc7ac[]
			var updateByQueryResponse = client.UpdateByQuery<object>(u => u
				.Index(new [] { "twitter", "blog" })
			);
			// end::cde4dddae5c06e7f1d38c9d933dbc7ac[]

			updateByQueryResponse.MatchesExample(@"POST twitter,blog/_update_by_query");
		}

		[U]
		public void Line327()
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
		public void Line337()
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
		public void Line350()
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
				return e;
			});
		}

		[U]
		public void Line392()
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
		public void Line417()
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
		public void Line475()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var getTaskResponse = client.Tasks.GetTask("r1A2WoRbTwKZ516z6NEs5A:36619");
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			getTaskResponse.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U]
		public void Line495()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var cancelTasksResponse = client.Tasks.Cancel(c => c
				.TaskId("r1A2WoRbTwKZ516z6NEs5A:36619")
			);
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			cancelTasksResponse.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}

		[U]
		public void Line514()
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
		public void Line534()
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
		public void Line561()
		{
			// tag::4acf902c2598b2558f34f20c1744c433[]
			var refreshResponse = client.Indices.Refresh();

			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(0)
				.Query(q => q
					.QueryString(qs => qs
						.Query("extra:test")
					)
				)
				.FilterPath(new [] { "hits.total" }) // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
			);
			// end::4acf902c2598b2558f34f20c1744c433[]

			refreshResponse.MatchesExample(@"GET _refresh", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});

			searchResponse.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total", e =>
			{
				// size and query are defined in the body
				e.Uri = new Uri(e.Uri.ToString().Replace("size=0&q=extra:test&", string.Empty));
				e.ApplyBodyChanges(body =>
				{
					body["size"] = 0;
					body["query"] = new JObject { ["query_string"] = new JObject { ["query"] = "extra:test" } };
				});
				return e;
			});
		}

		[U(Skip = "Example not implemented")]
		public void Line590()
		{
			// tag::ea02de2dbe05091fcb0dac72c8ba5f83[]
			var response0 = new SearchResponse<object>();
			// end::ea02de2dbe05091fcb0dac72c8ba5f83[]

			response0.MatchesExample(@"POST twitter/_update_by_query?refresh&slices=5
			{
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line603()
		{
			// tag::025b54db0edc50c24ea48a2bd94366ad[]
			var response0 = new SearchResponse<object>();
			// end::025b54db0edc50c24ea48a2bd94366ad[]

			response0.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total");
		}

		[U(Skip = "Example not implemented")]
		public void Line659()
		{
			// tag::2fe28d9a91b3081a9ec4601af8fb7b1c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::2fe28d9a91b3081a9ec4601af8fb7b1c[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""dynamic"": false,   \<1>
			    ""properties"": {
			      ""text"": {""type"": ""text""}
			    }
			  }
			}");

			response1.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""bar""
			}");

			response2.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""foo""
			}");

			response3.MatchesExample(@"PUT test/_mapping   \<2>
			{
			  ""properties"": {
			    ""text"": {""type"": ""text""},
			    ""flag"": {""type"": ""text"", ""analyzer"": ""keyword""}
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line697()
		{
			// tag::abd4fc3ce7784413a56fe2dcfe2809b5[]
			var response0 = new SearchResponse<object>();
			// end::abd4fc3ce7784413a56fe2dcfe2809b5[]

			response0.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line724()
		{
			// tag::97babc8d19ef0866774576716eb6d19e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::97babc8d19ef0866774576716eb6d19e[]

			response0.MatchesExample(@"POST test/_update_by_query?refresh&conflicts=proceed");

			response1.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}");
		}
	}
}

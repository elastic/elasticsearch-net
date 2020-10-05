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
	public class DeleteByQueryPage : ExampleBase
	{
		[U]
		[Description("docs/delete-by-query.asciidoc:10")]
		public void Line10()
		{
			// tag::ebb6b59fbc9325c17e45f524602d6be2[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Query(q => q
					.Match(m => m
						.Field(f => f.Message)
						.Query("some message")
					)
				)
			);
			// end::ebb6b59fbc9325c17e45f524602d6be2[]

			deleteByQueryResponse.MatchesExample(@"POST /twitter/_delete_by_query
			{
			  ""query"": {
			    ""match"": {
			      ""message"": ""some message""
			    }
			  }
			}", e =>
			{
				// client does not support shorthand match query syntax. Expand out
				e.ApplyBodyChanges(body =>
				{
					var query = body["query"]["match"]["message"].Value<string>();
					body["query"]["match"]["message"] = new JObject { { "query", query } };
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:356")]
		public void Line356()
		{
			// tag::e21e1c26dc8687e7bf7bd2bf019a6698[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Conflicts(Conflicts.Proceed)
				.Query(q => q.MatchAll())
			);
			// end::e21e1c26dc8687e7bf7bd2bf019a6698[]

			deleteByQueryResponse.MatchesExample(@"POST twitter/_delete_by_query?conflicts=proceed
			{
			  ""query"": {
			    ""match_all"": {}
			  }
			}");
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:369")]
		public void Line369()
		{
			// tag::c22b72c4a52ee098331b3f252c22860d[]
			var deleteByQueryResponse = client.DeleteByQuery<object>(d => d
				.Index("twitter,blog")
				.Query(q => q.MatchAll())
			);
			// end::c22b72c4a52ee098331b3f252c22860d[]

			deleteByQueryResponse.MatchesExample(@"POST /twitter,blog/_delete_by_query
			{
			  ""query"": {
			    ""match_all"": {}
			  }
			}");
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:383")]
		public void Line383()
		{
			// tag::c32a3f8071d87f0a3f5a78e07fe7a669[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Routing(1)
				.Query(q => q
					.Range(r => r
						.Field(f => f.Age)
						.GreaterThanOrEquals(10)
					)
				)
			);
			// end::c32a3f8071d87f0a3f5a78e07fe7a669[]

			deleteByQueryResponse.MatchesExample(@"POST twitter/_delete_by_query?routing=1
			{
			  ""query"": {
			    ""range"" : {
			        ""age"" : {
			           ""gte"" : 10
			        }
			    }
			  }
			}", e =>
			{
				// numeric ranges always use doubles
				e.ApplyBodyChanges(body =>
				{
					body["query"]["range"]["age"]["gte"] = 10d;
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:401")]
		public void Line401()
		{
			// tag::dfb1fe96d806a644214d06f9b4b87878[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.ScrollSize(5000)
				.Query(q => q
					.Term(r => r
						.Field(f => f.User)
						.Value("kimchy")
					)
				)
			);
			// end::dfb1fe96d806a644214d06f9b4b87878[]

			deleteByQueryResponse.MatchesExample(@"POST twitter/_delete_by_query?scroll_size=5000
			{
			  ""query"": {
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					var value = body["query"]["term"]["user"].Value<string>();
					body["query"]["term"]["user"] = new JObject { { "value", value } };
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:421")]
		public void Line421()
		{
			// tag::1e49eba5b9042c1900a608fe5105ba43[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Slice(s => s
					.Id(0)
					.Max(2)
				)
				.Query(q => q
					.Range(r => r
						.Field(f => f.Likes)
						.LessThan(10)
					)
				)
			);

			var deleteByQueryResponse2 = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Slice(s => s
					.Id(1)
					.Max(2)
				)
				.Query(q => q
					.Range(r => r
						.Field(f => f.Likes)
						.LessThan(10)
					)
				)
			);
			// end::1e49eba5b9042c1900a608fe5105ba43[]

			deleteByQueryResponse.MatchesExample(@"POST twitter/_delete_by_query
			{
			  ""slice"": {
			    ""id"": 0,
			    ""max"": 2
			  },
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}", e =>
			{
				// numeric ranges always use doubles
				e.ApplyBodyChanges(body =>
				{
					body["query"]["range"]["likes"]["lt"] = 10d;
				});
			});

			deleteByQueryResponse2.MatchesExample(@"POST twitter/_delete_by_query
			{
			  ""slice"": {
			    ""id"": 1,
			    ""max"": 2
			  },
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}", e =>
			{
				e.ApplyBodyChanges(body =>
				{
					// numeric ranges always use doubles
					body["query"]["range"]["likes"]["lt"] = 10d;
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:456")]
		public void Line456()
		{
			// tag::3e573bfabe00f8bfb8bb69aa5820768e[]
			var refreshResponse = client.Indices.Refresh();

			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(0)
				.FilterPath(new[] { "hits.total" }) // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
				.Query(q => q
					.Range(r => r
						.Field(f => f.Likes)
						.LessThan(10)
					)
				)
			);
			// end::3e573bfabe00f8bfb8bb69aa5820768e[]

			refreshResponse.MatchesExample(@"GET _refresh", e =>
			{
				// refresh is always a POST with the client
				e.Method = HttpMethod.POST;
			});

			searchResponse.MatchesExample(@"POST twitter/_search?size=0&filter_path=hits.total
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}", e =>
			{
				e.Uri.Query = e.Uri.Query.Replace("size=0", string.Empty);
				e.ApplyBodyChanges(body =>
				{
					// size always in the body
					body["size"] = 0;
					// numeric ranges always use doubles
					body["query"]["range"]["likes"]["lt"] = 10d;
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:494")]
		public void Line494()
		{
			// tag::a5a7050fb9dcb9574e081957ade28617[]
			var deleteByQueryResponse = client.DeleteByQuery<Tweet>(d => d
				.Index("twitter")
				.Refresh()
				.Slices(5)
				.Query(q => q
					.Range(r => r
						.Field(f => f.Likes)
						.LessThan(10)
					)
				)
			);
			// end::a5a7050fb9dcb9574e081957ade28617[]

			deleteByQueryResponse.MatchesExample(@"POST twitter/_delete_by_query?refresh&slices=5
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}", e =>
			{
				// query string params always need a value
				e.Uri.Query = e.Uri.Query.Replace("refresh", "refresh=true");
				e.ApplyBodyChanges(body =>
				{
					// slices is defined in body
					body["query"]["range"]["likes"]["lt"] = 10d;
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:511")]
		public void Line511()
		{
			// tag::14701dcc0cca9665fce2aace0cb62af7[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Size(0)
				.FilterPath(new[] { "hits.total" }) // <1> Using filter path can result in a response that cannot be parsed by the client's serializer. In these cases, using the low level client and parsing the JSON response may be preferred.
				.Query(q => q
					.Range(r => r
						.Field(f => f.Likes)
						.LessThan(10)
					)
				)
			);
			// end::14701dcc0cca9665fce2aace0cb62af7[]

			searchResponse.MatchesExample(@"POST twitter/_search?size=0&filter_path=hits.total
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}", e =>
			{
				// size is specified in the body
				e.Uri.Query = e.Uri.Query.Replace("size=0", string.Empty);
				e.ApplyBodyChanges(body =>
				{
					body["size"] = 0;
					body["query"]["range"]["likes"]["lt"] = 10d;
				});
			});
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:579")]
		public void Line579()
		{
			// tag::52c7e4172a446c394210a07c464c57d2[]
			var rethrottleResponse = client.DeleteByQueryRethrottle("r1A2WoRbTwKZ516z6NEs5A:36619",
				r => r
				.RequestsPerSecond(-1)
			);
			// end::52c7e4172a446c394210a07c464c57d2[]

			rethrottleResponse.MatchesExample(@"POST _delete_by_query/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:593")]
		public void Line593()
		{
			// tag::216848930c2d344fe0bed0daa70c35b9[]
			var listTasksResponse = client.Tasks.List(t => t
				.Detailed()
				.Actions("*/delete/byquery")
			);
			// end::216848930c2d344fe0bed0daa70c35b9[]

			listTasksResponse.MatchesExample(@"GET _tasks?detailed=true&actions=*/delete/byquery");
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:647")]
		public void Line647()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var getTaskResponse = client.Tasks.GetTask("r1A2WoRbTwKZ516z6NEs5A:36619");
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			getTaskResponse.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U]
		[Description("docs/delete-by-query.asciidoc:667")]
		public void Line667()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var cancelTaskResponse = client.Tasks.Cancel(t => t
				.TaskId("r1A2WoRbTwKZ516z6NEs5A:36619")
			);
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			cancelTaskResponse.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}
	}
}

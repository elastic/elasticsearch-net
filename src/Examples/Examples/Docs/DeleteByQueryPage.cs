using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class DeleteByQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line8()
		{
			// tag::c1de6df850c4111c68ec57a6f9c2ec6d[]
			var response0 = new SearchResponse<object>();
			// end::c1de6df850c4111c68ec57a6f9c2ec6d[]

			response0.MatchesExample(@"POST twitter/_delete_by_query
			{
			  ""query"": { \<1>
			    ""match"": {
			      ""message"": ""some message""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line77()
		{
			// tag::e21e1c26dc8687e7bf7bd2bf019a6698[]
			var response0 = new SearchResponse<object>();
			// end::e21e1c26dc8687e7bf7bd2bf019a6698[]

			response0.MatchesExample(@"POST twitter/_delete_by_query?conflicts=proceed
			{
			  ""query"": {
			    ""match_all"": {}
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line92()
		{
			// tag::099e1dbe296568756df5a9816efcae45[]
			var response0 = new SearchResponse<object>();
			// end::099e1dbe296568756df5a9816efcae45[]

			response0.MatchesExample(@"POST twitter,blog/_delete_by_query
			{
			  ""query"": {
			    ""match_all"": {}
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[SkipExample]
		public void Line107()
		{
			// tag::c32a3f8071d87f0a3f5a78e07fe7a669[]
			var response0 = new SearchResponse<object>();
			// end::c32a3f8071d87f0a3f5a78e07fe7a669[]

			response0.MatchesExample(@"POST twitter/_delete_by_query?routing=1
			{
			  ""query"": {
			    ""range"" : {
			        ""age"" : {
			           ""gte"" : 10
			        }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line126()
		{
			// tag::dfb1fe96d806a644214d06f9b4b87878[]
			var response0 = new SearchResponse<object>();
			// end::dfb1fe96d806a644214d06f9b4b87878[]

			response0.MatchesExample(@"POST twitter/_delete_by_query?scroll_size=5000
			{
			  ""query"": {
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line303()
		{
			// tag::216848930c2d344fe0bed0daa70c35b9[]
			var response0 = new SearchResponse<object>();
			// end::216848930c2d344fe0bed0daa70c35b9[]

			response0.MatchesExample(@"GET _tasks?detailed=true&actions=*/delete/byquery");
		}

		[U]
		[SkipExample]
		public void Line358()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var response0 = new SearchResponse<object>();
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			response0.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U]
		[SkipExample]
		public void Line379()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var response0 = new SearchResponse<object>();
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			response0.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}

		[U]
		[SkipExample]
		public void Line399()
		{
			// tag::52c7e4172a446c394210a07c464c57d2[]
			var response0 = new SearchResponse<object>();
			// end::52c7e4172a446c394210a07c464c57d2[]

			response0.MatchesExample(@"POST _delete_by_query/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U]
		[SkipExample]
		public void Line429()
		{
			// tag::1e49eba5b9042c1900a608fe5105ba43[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1e49eba5b9042c1900a608fe5105ba43[]

			response0.MatchesExample(@"POST twitter/_delete_by_query
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
			}");

			response1.MatchesExample(@"POST twitter/_delete_by_query
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
			}");
		}

		[U]
		[SkipExample]
		public void Line465()
		{
			// tag::3e573bfabe00f8bfb8bb69aa5820768e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3e573bfabe00f8bfb8bb69aa5820768e[]

			response0.MatchesExample(@"GET _refresh");

			response1.MatchesExample(@"POST twitter/_search?size=0&filter_path=hits.total
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line505()
		{
			// tag::a5a7050fb9dcb9574e081957ade28617[]
			var response0 = new SearchResponse<object>();
			// end::a5a7050fb9dcb9574e081957ade28617[]

			response0.MatchesExample(@"POST twitter/_delete_by_query?refresh&slices=5
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line523()
		{
			// tag::14701dcc0cca9665fce2aace0cb62af7[]
			var response0 = new SearchResponse<object>();
			// end::14701dcc0cca9665fce2aace0cb62af7[]

			response0.MatchesExample(@"POST twitter/_search?size=0&filter_path=hits.total
			{
			  ""query"": {
			    ""range"": {
			      ""likes"": {
			        ""lt"": 10
			      }
			    }
			  }
			}");
		}
	}
}

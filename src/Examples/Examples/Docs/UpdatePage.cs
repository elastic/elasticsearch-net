using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class UpdatePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line18()
		{
			// tag::381fced1882ca8337143e6bb180a5715[]
			var response0 = new SearchResponse<object>();
			// end::381fced1882ca8337143e6bb180a5715[]

			response0.MatchesExample(@"PUT test/_doc/1
			{
			    ""counter"" : 1,
			    ""tags"" : [""red""]
			}");
		}

		[U]
		[SkipExample]
		public void Line33()
		{
			// tag::96de5703ba0bd43fd4ac239ec5408542[]
			var response0 = new SearchResponse<object>();
			// end::96de5703ba0bd43fd4ac239ec5408542[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : {
			        ""source"": ""ctx._source.counter += params.count"",
			        ""lang"": ""painless"",
			        ""params"" : {
			            ""count"" : 4
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line52()
		{
			// tag::4cd246e5c4c035a2cd4081ae9a3d54e5[]
			var response0 = new SearchResponse<object>();
			// end::4cd246e5c4c035a2cd4081ae9a3d54e5[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : {
			        ""source"": ""ctx._source.tags.add(params.tag)"",
			        ""lang"": ""painless"",
			        ""params"" : {
			            ""tag"" : ""blue""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line74()
		{
			// tag::ac544eb247a29ca42aab13826ca88561[]
			var response0 = new SearchResponse<object>();
			// end::ac544eb247a29ca42aab13826ca88561[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : {
			        ""source"": ""if (ctx._source.tags.contains(params.tag)) { ctx._source.tags.remove(ctx._source.tags.indexOf(params.tag)) }"",
			        ""lang"": ""painless"",
			        ""params"" : {
			            ""tag"" : ""blue""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line96()
		{
			// tag::eb30ba547e4a7b8f54f33ab259aca523[]
			var response0 = new SearchResponse<object>();
			// end::eb30ba547e4a7b8f54f33ab259aca523[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : ""ctx._source.new_field = 'value_of_new_field'""
			}");
		}

		[U]
		[SkipExample]
		public void Line108()
		{
			// tag::58df61acbfb15b8ef0aaa18b81ae98a6[]
			var response0 = new SearchResponse<object>();
			// end::58df61acbfb15b8ef0aaa18b81ae98a6[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : ""ctx._source.remove('new_field')""
			}");
		}

		[U]
		[SkipExample]
		public void Line122()
		{
			// tag::98aeb275f829b5f7b8eb2147701565ff[]
			var response0 = new SearchResponse<object>();
			// end::98aeb275f829b5f7b8eb2147701565ff[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : {
			        ""source"": ""if (ctx._source.tags.contains(params.tag)) { ctx.op = 'delete' } else { ctx.op = 'none' }"",
			        ""lang"": ""painless"",
			        ""params"" : {
			            ""tag"" : ""green""
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line149()
		{
			// tag::38c1d0f6668e9563c0827f839f9fa505[]
			var response0 = new SearchResponse<object>();
			// end::38c1d0f6668e9563c0827f839f9fa505[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line207()
		{
			// tag::015294a400986295039e52ebc62033be[]
			var response0 = new SearchResponse<object>();
			// end::015294a400986295039e52ebc62033be[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    },
			    ""detect_noop"": false
			}");
		}

		[U]
		[SkipExample]
		public void Line228()
		{
			// tag::0a958e486ede3f519d48431ab689eded[]
			var response0 = new SearchResponse<object>();
			// end::0a958e486ede3f519d48431ab689eded[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : {
			        ""source"": ""ctx._source.counter += params.count"",
			        ""lang"": ""painless"",
			        ""params"" : {
			            ""count"" : 4
			        }
			    },
			    ""upsert"" : {
			        ""counter"" : 1
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line255()
		{
			// tag::f9636d7ef1a45be4f36418c875cf6bef[]
			var response0 = new SearchResponse<object>();
			// end::f9636d7ef1a45be4f36418c875cf6bef[]

			response0.MatchesExample(@"POST sessions/_update/dh3sgudg8gsrgl
			{
			    ""scripted_upsert"":true,
			    ""script"" : {
			        ""id"": ""my_web_session_summariser"",
			        ""params"" : {
			            ""pageViewEvent"" : {
			                ""url"":""foo.com/bar"",
			                ""response"":404,
			                ""time"":""2014-01-01 12:32""
			            }
			        }
			    },
			    ""upsert"" : {}
			}");
		}

		[U]
		[SkipExample]
		public void Line285()
		{
			// tag::7cac05cb589f1614fd5b8589153bef06[]
			var response0 = new SearchResponse<object>();
			// end::7cac05cb589f1614fd5b8589153bef06[]

			response0.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    },
			    ""doc_as_upsert"" : true
			}");
		}
	}
}
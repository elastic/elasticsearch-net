using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;

namespace Examples.Docs
{
	public class UpdatePage : ExampleBase
	{
		[U]
		public void Line84()
		{
			// tag::381fced1882ca8337143e6bb180a5715[]
			var indexResponse = client.Index(new
			{
				counter = 1,
				tags = new[] { "red" }
			}, i => i
				.Index("test")
				.Id(1)
			);
			// end::381fced1882ca8337143e6bb180a5715[]

			indexResponse.MatchesExample(@"PUT test/_doc/1
			{
			    ""counter"" : 1,
			    ""tags"" : [""red""]
			}");
		}

		[U]
		public void Line96()
		{
			// tag::96de5703ba0bd43fd4ac239ec5408542[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("ctx._source.counter += params.count")
					.Lang("painless")
					.Params(p => p
						.Add("count", 4)
					)
				)
			);
			// end::96de5703ba0bd43fd4ac239ec5408542[]

			updateResponse.MatchesExample(@"POST test/_update/1
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
		public void Line114()
		{
			// tag::4cd246e5c4c035a2cd4081ae9a3d54e5[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("ctx._source.tags.add(params.tag)")
					.Lang("painless")
					.Params(p => p
						.Add("tag", "blue")
					)
				)
			);
			// end::4cd246e5c4c035a2cd4081ae9a3d54e5[]

			updateResponse.MatchesExample(@"POST test/_update/1
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
		public void Line135()
		{
			// tag::ac544eb247a29ca42aab13826ca88561[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("if (ctx._source.tags.contains(params.tag)) { ctx._source.tags.remove(ctx._source.tags.indexOf(params.tag)) }")
					.Lang("painless")
					.Params(p => p
						.Add("tag", "blue")
					)
				)
			);
			// end::ac544eb247a29ca42aab13826ca88561[]

			updateResponse.MatchesExample(@"POST test/_update/1
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
		public void Line153()
		{
			// tag::eb30ba547e4a7b8f54f33ab259aca523[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("ctx._source.new_field = 'value_of_new_field'")
				)
			);
			// end::eb30ba547e4a7b8f54f33ab259aca523[]

			updateResponse.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : ""ctx._source.new_field = 'value_of_new_field'""
			}", e =>
			{
				// client only supports long form of script
				e.ApplyBodyChanges(body =>
				{
					var script = body["script"].Value<string>();
					body["script"] = new JObject { { "source", script } };
				});
				return e;
			});
		}

		[U]
		public void Line164()
		{
			// tag::58df61acbfb15b8ef0aaa18b81ae98a6[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("ctx._source.remove('new_field')")
				)
			);
			// end::58df61acbfb15b8ef0aaa18b81ae98a6[]

			updateResponse.MatchesExample(@"POST test/_update/1
			{
			    ""script"" : ""ctx._source.remove('new_field')""
			}", e =>
			{
				// client only supports long form of script
				e.ApplyBodyChanges(body =>
				{
					var script = body["script"].Value<string>();
					body["script"] = new JObject { { "source", script } };
				});
				return e;
			});
		}

		[U]
		public void Line177()
		{
			// tag::98aeb275f829b5f7b8eb2147701565ff[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("if (ctx._source.tags.contains(params.tag)) { ctx.op = 'delete' } else { ctx.op = 'none' }")
					.Lang("painless")
					.Params(p => p
						.Add("tag", "green")
					)
				)
			);
			// end::98aeb275f829b5f7b8eb2147701565ff[]

			updateResponse.MatchesExample(@"POST test/_update/1
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
		public void Line198()
		{
			// tag::38c1d0f6668e9563c0827f839f9fa505[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Doc(new
				{
					name = "new_name"
				})
			);
			// end::38c1d0f6668e9563c0827f839f9fa505[]

			updateResponse.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    }
			}");
		}

		[U]
		public void Line251()
		{
			// tag::015294a400986295039e52ebc62033be[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Doc(new
				{
					name = "new_name"
				})
				.DetectNoop(false)
			);
			// end::015294a400986295039e52ebc62033be[]

			updateResponse.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    },
			    ""detect_noop"": false
			}");
		}

		[U]
		public void Line271()
		{
			// tag::0a958e486ede3f519d48431ab689eded[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Script(s => s
					.Source("ctx._source.counter += params.count")
					.Lang("painless")
					.Params(p => p
						.Add("count", 4)
					)
				)
				.Upsert(new
				{
					counter = 1
				})
			);
			// end::0a958e486ede3f519d48431ab689eded[]

			updateResponse.MatchesExample(@"POST test/_update/1
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
		public void Line296()
		{
			// tag::f9636d7ef1a45be4f36418c875cf6bef[]
			var updateResponse = client.Update<object>("dh3sgudg8gsrgl", u => u
				.Index("sessions")
				.ScriptedUpsert(true)
				.Script(s => s
					.Id("my_web_session_summariser")
					.Params(p => p
						.Add("pageViewEvent", new
						{
							url = "foo.com/bar",
							response = 404,
							time = "2014-01-01 12:32"
						})
					)
				)
				.Upsert(new {})
			);
			// end::f9636d7ef1a45be4f36418c875cf6bef[]

			updateResponse.MatchesExample(@"POST sessions/_update/dh3sgudg8gsrgl
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
		public void Line325()
		{
			// tag::7cac05cb589f1614fd5b8589153bef06[]
			var updateResponse = client.Update<object>(1, u => u
				.Index("test")
				.Doc(new
				{
					name = "new_name"
				})
				.DocAsUpsert(true)
			);
			// end::7cac05cb589f1614fd5b8589153bef06[]

			updateResponse.MatchesExample(@"POST test/_update/1
			{
			    ""doc"" : {
			        ""name"" : ""new_name""
			    },
			    ""doc_as_upsert"" : true
			}");
		}
	}
}

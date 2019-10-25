using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Watcher
{
	public class ActionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::ec4e4462b5394f991a4384425feee8a4[]
			var response0 = new SearchResponse<object>();
			// end::ec4e4462b5394f991a4384425feee8a4[]

			response0.MatchesExample(@"PUT _watcher/watch/error_logs_alert
			{
			  ""metadata"" : {
			    ""color"" : ""red""
			  },
			  ""trigger"" : {
			    ""schedule"" : {
			      ""interval"" : ""5m""
			    }
			  },
			  ""input"" : {
			    ""search"" : {
			      ""request"" : {
			        ""indices"" : ""log-events"",
			        ""body"" : {
			          ""size"" : 0,
			          ""query"" : { ""match"" : { ""status"" : ""error"" } }
			        }
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : { ""ctx.payload.hits.total.value"" : { ""gt"" : 5 }}
			  },
			  ""actions"" : {
			    ""email_administrator"" : {
			      ""throttle_period"": ""15m"", <1>
			      ""email"" : { <2>
			        ""to"" : ""sys.admino@host.domain"",
			        ""subject"" : ""Encountered {{ctx.payload.hits.total.value}} errors"",
			        ""body"" : ""Too many error in the system, see attached data"",
			        ""attachments"" : {
			          ""attached_data"" : {
			            ""data"" : {
			              ""format"" : ""json""
			            }
			          }
			        },
			        ""priority"" : ""high""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line104()
		{
			// tag::a8e923739fdc7c9118b7b4a0ba791195[]
			var response0 = new SearchResponse<object>();
			// end::a8e923739fdc7c9118b7b4a0ba791195[]

			response0.MatchesExample(@"PUT _watcher/watch/log_event_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""5m"" }
			  },
			  ""input"" : {
			    ""search"" : {
			      ""request"" : {
			        ""indices"" : ""log-events"",
			        ""body"" : {
			          ""size"" : 0,
			          ""query"" : { ""match"" : { ""status"" : ""error"" } }
			        }
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : { ""ctx.payload.hits.total.value"" : { ""gt"" : 5 }}
			  },
			  ""throttle_period"" : ""15m"", <1>
			  ""actions"" : {
			    ""email_administrator"" : {
			      ""email"" : {
			        ""to"" : ""sys.admino@host.domain"",
			        ""subject"" : ""Encountered {{ctx.payload.hits.total.value}} errors"",
			        ""body"" : ""Too many error in the system, see attached data"",
			        ""attachments"" : {
			          ""attached_data"" : {
			            ""data"" : {
			              ""format"" : ""json""
			            }
			          }
			        },
			        ""priority"" : ""high""
			      }
			    },
			    ""notify_pager"" : {
			      ""webhook"" : {
			        ""method"" : ""POST"",
			        ""host"" : ""pager.service.domain"",
			        ""port"" : 1234,
			        ""path"" : ""/{{watch_id}}"",
			        ""body"" : ""Encountered {{ctx.payload.hits.total.value}} errors""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line178()
		{
			// tag::3d48d1ba49f680aac32177d653944623[]
			var response0 = new SearchResponse<object>();
			// end::3d48d1ba49f680aac32177d653944623[]

			response0.MatchesExample(@"POST _watcher/watch/<id>/_ack/<action_ids>");
		}

		[U(Skip = "Example not implemented")]
		public void Line205()
		{
			// tag::9eef31d85ebaf6c27054d7375715dbe0[]
			var response0 = new SearchResponse<object>();
			// end::9eef31d85ebaf6c27054d7375715dbe0[]

			response0.MatchesExample(@"PUT _watcher/watch/log_event_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""5m"" }
			  },
			  ""input"" : {
			    ""search"" : {
			      ""request"" : {
			        ""indices"" : ""log-events"",
			        ""body"" : {
			          ""query"" : { ""match"" : { ""status"" : ""error"" } }
			        }
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : { ""ctx.payload.hits.total"" : { ""gt"" : 0 } }
			  },
			  ""actions"" : {
			    ""log_hits"" : {
			      ""foreach"" : ""ctx.payload.hits.hits"", <1>
			      ""max_iterations"" : 500,
			      ""logging"" : {
			        ""text"" : ""Found id {{ctx.payload._id}} with field {{ctx.payload._source.my_field}}""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line250()
		{
			// tag::ece81947e2669ce5921723be11e995a5[]
			var response0 = new SearchResponse<object>();
			// end::ece81947e2669ce5921723be11e995a5[]

			response0.MatchesExample(@"PUT _watcher/watch/log_event_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""5m"" }
			  },
			  ""input"" : {
			    ""search"" : {
			      ""request"" : {
			        ""indices"" : ""log-events"",
			        ""body"" : {
			          ""size"" : 0,
			          ""query"" : { ""match"" : { ""status"" : ""error"" } }
			        }
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : { ""ctx.payload.hits.total.value"" : { ""gt"" : 0 } }
			  },
			  ""actions"" : {
			    ""email_administrator"" : {
			      ""email"" : {
			        ""to"" : ""sys.admino@host.domain"",
			        ""subject"" : ""Encountered {{ctx.payload.hits.total.value}} errors"",
			        ""body"" : ""Too many error in the system, see attached data"",
			        ""attachments"" : {
			          ""attached_data"" : {
			            ""data"" : {
			              ""format"" : ""json""
			            }
			          }
			        },
			        ""priority"" : ""high""
			      }
			    },
			    ""notify_pager"" : {
			      ""condition"": { <1>
			        ""compare"" : { ""ctx.payload.hits.total.value"" : { ""gt"" : 5 } }
			      },
			      ""webhook"" : {
			        ""method"" : ""POST"",
			        ""host"" : ""pager.service.domain"",
			        ""port"" : 1234,
			        ""path"" : ""/{{watch_id}}"",
			        ""body"" : ""Encountered {{ctx.payload.hits.total.value}} errors""
			      }
			    }
			  }
			}");
		}
	}
}
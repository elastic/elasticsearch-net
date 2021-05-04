// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class ExecuteWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:147")]
		public void Line147()
		{
			// tag::01dc7bdc223bd651574ed2d3954a5b1c[]
			var response0 = new SearchResponse<object>();
			// end::01dc7bdc223bd651574ed2d3954a5b1c[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_execute");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:155")]
		public void Line155()
		{
			// tag::f6eff830fb0fad200ebfb1e3e46f6f0e[]
			var response0 = new SearchResponse<object>();
			// end::f6eff830fb0fad200ebfb1e3e46f6f0e[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_execute
			{
			  ""trigger_data"" : { \<1>
			     ""triggered_time"" : ""now"",
			     ""scheduled_time"" : ""now""
			  },
			  ""alternative_input"" : { \<2>
			    ""foo"" : ""bar""
			  },
			  ""ignore_condition"" : true, \<3>
			  ""action_modes"" : {
			    ""my-action"" : ""force_simulate"" \<4>
			  },
			  ""record_execution"" : true \<5>
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:297")]
		public void Line297()
		{
			// tag::7f37031fb40b68a61255b7c71d7eed0b[]
			var response0 = new SearchResponse<object>();
			// end::7f37031fb40b68a61255b7c71d7eed0b[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_execute
			{
			  ""action_modes"" : {
			    ""action1"" : ""force_simulate"",
			    ""action2"" : ""skip""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:312")]
		public void Line312()
		{
			// tag::9cf6c7012a4f2bb562bc256aa28c3409[]
			var response0 = new SearchResponse<object>();
			// end::9cf6c7012a4f2bb562bc256aa28c3409[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_execute
			{
			  ""action_modes"" : {
			    ""_all"" : ""force_execute""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:325")]
		public void Line325()
		{
			// tag::9cd37d0ccbc66ad47ddb626564b27cc8[]
			var response0 = new SearchResponse<object>();
			// end::9cd37d0ccbc66ad47ddb626564b27cc8[]

			response0.MatchesExample(@"POST _watcher/watch/_execute
			{
			  ""watch"" : {
			    ""trigger"" : { ""schedule"" : { ""interval"" : ""10s"" } },
			    ""input"" : {
			      ""search"" : {
			        ""request"" : {
			          ""indices"" : [ ""logs"" ],
			          ""body"" : {
			            ""query"" : {
			              ""match"" : { ""message"": ""error"" }
			            }
			          }
			        }
			      }
			    },
			    ""condition"" : {
			      ""compare"" : { ""ctx.payload.hits.total"" : { ""gt"" : 0 }}
			    },
			    ""actions"" : {
			      ""log_error"" : {
			        ""logging"" : {
			          ""text"" : ""Found {{ctx.payload.hits.total}} errors in the logs""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/execute-watch.asciidoc:361")]
		public void Line361()
		{
			// tag::10b924bf6298aa6157ed00ce12f8edc1[]
			var response0 = new SearchResponse<object>();
			// end::10b924bf6298aa6157ed00ce12f8edc1[]

			response0.MatchesExample(@"POST _watcher/watch/_execute
			{
			  ""ignore_condition"" : true,
			  ""watch"" : {
			    ""trigger"" : { ""schedule"" : { ""interval"" : ""10s"" } },
			    ""input"" : {
			      ""search"" : {
			        ""request"" : {
			          ""indices"" : [ ""logs"" ],
			          ""body"" : {
			            ""query"" : {
			              ""match"" : { ""message"": ""error"" }
			            }
			          }
			        }
			      }
			    },
			    ""condition"" : {
			      ""compare"" : { ""ctx.payload.hits.total"" : { ""gt"" : 0 }}
			    },
			    ""actions"" : {
			      ""log_error"" : {
			        ""logging"" : {
			          ""text"" : ""Found {{ctx.payload.hits.total}} errors in the logs""
			        }
			      }
			    }
			  }
			}");
		}
	}
}

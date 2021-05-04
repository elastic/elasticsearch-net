// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Watcher.ExampleWatches
{
	public class ExampleWatchClusterstatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:27")]
		public void Line27()
		{
			// tag::dd0b196a099e1cca08c5ce4dd74e935a[]
			var response0 = new SearchResponse<object>();
			// end::dd0b196a099e1cca08c5ce4dd74e935a[]

			response0.MatchesExample(@"PUT _watcher/watch/cluster_health_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""10s"" } <1>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:45")]
		public void Line45()
		{
			// tag::1e9cab0b2727624e22e8cf4e7ca498ac[]
			var response0 = new SearchResponse<object>();
			// end::1e9cab0b2727624e22e8cf4e7ca498ac[]

			response0.MatchesExample(@"GET _cluster/health?pretty");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:54")]
		public void Line54()
		{
			// tag::221e9b14567f950008459af77757750e[]
			var response0 = new SearchResponse<object>();
			// end::221e9b14567f950008459af77757750e[]

			response0.MatchesExample(@"PUT _watcher/watch/cluster_health_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""10s"" }
			  },
			  ""input"" : {
			    ""http"" : {
			      ""request"" : {
			        ""host"" : ""localhost"",
			        ""port"" : 9200,
			        ""path"" : ""/_cluster/health""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:76")]
		public void Line76()
		{
			// tag::239f615e0009c5cb1dc4e82ec4c0dab5[]
			var response0 = new SearchResponse<object>();
			// end::239f615e0009c5cb1dc4e82ec4c0dab5[]

			response0.MatchesExample(@"PUT _watcher/watch/cluster_health_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""10s"" }
			  },
			  ""input"" : {
			    ""http"" : {
			      ""request"" : {
			        ""host"" : ""localhost"",
			        ""port"" : 9200,
			        ""path"" : ""/_cluster/health"",
			        ""auth"": {
			          ""basic"": {
			            ""username"": ""elastic"",
			            ""password"": ""x-pack-test-password""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:115")]
		public void Line115()
		{
			// tag::dfb20907cfc5ac520ea3b1dba5f00811[]
			var response0 = new SearchResponse<object>();
			// end::dfb20907cfc5ac520ea3b1dba5f00811[]

			response0.MatchesExample(@"GET .watcher-history*/_search
			{
			  ""sort"" : [
			    { ""result.execution_time"" : ""desc"" }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:137")]
		public void Line137()
		{
			// tag::90f1f5304922fb6d097846dd1444c075[]
			var response0 = new SearchResponse<object>();
			// end::90f1f5304922fb6d097846dd1444c075[]

			response0.MatchesExample(@"PUT _watcher/watch/cluster_health_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""10s"" } <1>
			  },
			  ""input"" : {
			    ""http"" : {
			      ""request"" : {
			       ""host"" : ""localhost"",
			       ""port"" : 9200,
			       ""path"" : ""/_cluster/health""
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : {
			      ""ctx.payload.status"" : { ""eq"" : ""red"" }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:169")]
		public void Line169()
		{
			// tag::95c03bdef4faf6bef039c986f4cb3aba[]
			var response0 = new SearchResponse<object>();
			// end::95c03bdef4faf6bef039c986f4cb3aba[]

			response0.MatchesExample(@"GET .watcher-history*/_search?pretty
			{
			  ""query"" : {
			    ""match"" : { ""result.condition.met"" : true }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:193")]
		public void Line193()
		{
			// tag::007179b5e241da650562a5f0a5007823[]
			var response0 = new SearchResponse<object>();
			// end::007179b5e241da650562a5f0a5007823[]

			response0.MatchesExample(@"PUT _watcher/watch/cluster_health_watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""interval"" : ""10s"" }
			  },
			  ""input"" : {
			    ""http"" : {
			      ""request"" : {
			       ""host"" : ""localhost"",
			       ""port"" : 9200,
			       ""path"" : ""/_cluster/health""
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : {
			      ""ctx.payload.status"" : { ""eq"" : ""red"" }
			    }
			  },
			  ""actions"" : {
			    ""send_email"" : {
			      ""email"" : {
			        ""to"" : ""username@example.org"",
			        ""subject"" : ""Cluster Status Warning"",
			        ""body"" : ""Cluster status is RED""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/example-watches/example-watch-clusterstatus.asciidoc:280")]
		public void Line280()
		{
			// tag::60b0fc1b6ae418621ff1b31591fa1fce[]
			var response0 = new SearchResponse<object>();
			// end::60b0fc1b6ae418621ff1b31591fa1fce[]

			response0.MatchesExample(@"DELETE _watcher/watch/cluster_health_watch");
		}
	}
}

using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class PutWatchPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line101()
		{
			// tag::3a12feb0de224bfaaf518d95b9f516ff[]
			var response0 = new SearchResponse<object>();
			// end::3a12feb0de224bfaaf518d95b9f516ff[]

			response0.MatchesExample(@"PUT _watcher/watch/my-watch
			{
			  ""trigger"" : {
			    ""schedule"" : { ""cron"" : ""0 0/1 * * * ?"" }
			  },
			  ""input"" : {
			    ""search"" : {
			      ""request"" : {
			        ""indices"" : [
			          ""logstash*""
			        ],
			        ""body"" : {
			          ""query"" : {
			            ""bool"" : {
			              ""must"" : {
			                ""match"": {
			                   ""response"": 404
			                }
			              },
			              ""filter"" : {
			                ""range"": {
			                  ""@timestamp"": {
			                    ""from"": ""{{ctx.trigger.scheduled_time}}||-5m"",
			                    ""to"": ""{{ctx.trigger.triggered_time}}""
			                  }
			                }
			              }
			            }
			          }
			        }
			      }
			    }
			  },
			  ""condition"" : {
			    ""compare"" : { ""ctx.payload.hits.total"" : { ""gt"" : 0 }}
			  },
			  ""actions"" : {
			    ""email_admin"" : {
			      ""email"" : {
			        ""to"" : ""admin@domain.host.com"",
			        ""subject"" : ""404 recently encountered""
			      }
			    }
			  }
			}");
		}
	}
}
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Watcher.ExampleWatches
{
	public class ExampleWatchMeetupdataPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line198()
		{
			// tag::4912f2140d10dbf15596c60319835e36[]
			var response0 = new SearchResponse<object>();
			// end::4912f2140d10dbf15596c60319835e36[]

			response0.MatchesExample(@"PUT _watcher/watch/meetup
			{
			  ""trigger"": {
			    ""schedule"": {
			      ""interval"": ""1h""
			    }
			  },
			  ""input"": {
			    ""search"": {
			      ""request"": {
			        ""indices"": [
			          ""<logstash-{now-1h}>"",
			          ""<logstash-{now}>""
			        ],
			        ""body"": {
			          ""size"": 0,
			          ""query"": {
			            ""bool"": {
			              ""filter"": [
			                {
			                  ""range"": {
			                    ""@timestamp"": {
			                      ""gte"": ""now-3h""
			                    }
			                  }
			                },
			                {
			                  ""match"": {
			                    ""group.group_topics.topic_name"": ""Open Source""
			                  }
			                }
			              ]
			            }
			          },
			          ""aggs"": {
			            ""group_by_city"": {
			              ""terms"": {
			                ""field"": ""group.group_city.raw"",
			                ""size"": 5
			              },
			              ""aggs"": {
			                ""group_by_event"": {
			                  ""terms"": {
			                    ""field"": ""event.event_url.raw"",
			                    ""size"": 5
			                  },
			                  ""aggs"": {
			                    ""get_latest"": {
			                      ""terms"": {
			                        ""field"": ""@timestamp"",
			                        ""size"": 1,
			                        ""order"": {
			                          ""_key"": ""desc""
			                        }
			                      },
			                      ""aggs"": {
			                        ""group_by_event_name"": {
			                          ""terms"": {
			                            ""field"": ""event.event_name.raw""
			                          }
			                        }
			                      }
			                    }
			                  }
			                }
			              }
			            }
			          }
			        }
			      }
			    }
			  },
			  ""condition"": {
			    ""compare"": {
			      ""ctx.payload.hits.total.value"": {
			        ""gt"": 0
			      }
			    }
			  },
			  ""actions"": {  <1>
			    ""email_me"": {
			      ""throttle_period"": ""10m"",
			      ""email"": {
			        ""from"": ""username@example.org"",  <2>
			        ""to"": ""recipient@example.org"",   <3>
			        ""subject"": ""Open Source events"",
			        ""body"": {
			          ""html"": ""Found events matching Open Source: <ul>{{#ctx.payload.aggregations.group_by_city.buckets}}<li>{{key}} ({{doc_count}})<ul>{{#group_by_event.buckets}}<li><a href=\""{{key}}\"">{{get_latest.buckets.0.group_by_event_name.buckets.0.key}}</a> ({{doc_count}})</li>{{/group_by_event.buckets}}</ul></li>{{/ctx.payload.aggregations.group_by_city.buckets}}</ul>""
			         }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line303()
		{
			// tag::4be6ee22c2cddc72c21a76cda23705ed[]
			var response0 = new SearchResponse<object>();
			// end::4be6ee22c2cddc72c21a76cda23705ed[]

			response0.MatchesExample(@"POST _watcher/watch/meetup/_execute");
		}
	}
}
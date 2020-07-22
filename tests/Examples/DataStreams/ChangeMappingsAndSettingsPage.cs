using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.DataStreams
{
	public class ChangeMappingsAndSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:101")]
		public void Line101()
		{
			// tag::9f7d8207b976f664badc27ef659c9706[]
			var response0 = new SearchResponse<object>();
			// end::9f7d8207b976f664badc27ef659c9706[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""message"": {                              <1>
			          ""type"": ""text""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:131")]
		public void Line131()
		{
			// tag::e13621211b3669c0c053ce93bd01e26a[]
			var response0 = new SearchResponse<object>();
			// end::e13621211b3669c0c053ce93bd01e26a[]

			response0.MatchesExample(@"PUT /logs/_mapping
			{
			  ""properties"": {
			    ""message"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:154")]
		public void Line154()
		{
			// tag::d3eebff7ff3609b5eedcaabb9ac1de86[]
			var response0 = new SearchResponse<object>();
			// end::d3eebff7ff3609b5eedcaabb9ac1de86[]

			response0.MatchesExample(@"PUT /logs/_mapping?write_index_only=true
			{
			  ""properties"": {
			    ""message"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:188")]
		public void Line188()
		{
			// tag::473d8309acc7dc722546c24096a6b993[]
			var response0 = new SearchResponse<object>();
			// end::473d8309acc7dc722546c24096a6b993[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""host"": {
			          ""properties"": {
			            ""ip"": {
			              ""type"": ""ip"",
			              ""ignore_malformed"": true            <1>
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:224")]
		public void Line224()
		{
			// tag::3403c1a9cd528ad74464541260dca4c6[]
			var response0 = new SearchResponse<object>();
			// end::3403c1a9cd528ad74464541260dca4c6[]

			response0.MatchesExample(@"PUT /logs/_mapping
			{
			  ""properties"": {
			    ""host"": {
			      ""properties"": {
			        ""ip"": {
			          ""type"": ""ip"",
			          ""ignore_malformed"": true
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:252")]
		public void Line252()
		{
			// tag::450150da4ac543b41fd1a25be57ed9c8[]
			var response0 = new SearchResponse<object>();
			// end::450150da4ac543b41fd1a25be57ed9c8[]

			response0.MatchesExample(@"PUT /logs/_mapping?write_index_only=true
			{
			  ""properties"": {
			    ""host"": {
			      ""properties"": {
			        ""ip"": {
			          ""type"": ""ip"",
			          ""ignore_malformed"": true
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:297")]
		public void Line297()
		{
			// tag::639df1dd6a53ad4bc33e3cf3fab683b8[]
			var response0 = new SearchResponse<object>();
			// end::639df1dd6a53ad4bc33e3cf3fab683b8[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""settings"": {
			      ""index.refresh_interval"": ""30s""             <1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:323")]
		public void Line323()
		{
			// tag::a7efe98baa728421cc3d367ab30ae01f[]
			var response0 = new SearchResponse<object>();
			// end::a7efe98baa728421cc3d367ab30ae01f[]

			response0.MatchesExample(@"PUT /logs/_settings
			{
			  ""index"": {
			    ""refresh_interval"": ""30s""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:354")]
		public void Line354()
		{
			// tag::30b34155e6cc0fe2d9fbc89408bc3d43[]
			var response0 = new SearchResponse<object>();
			// end::30b34155e6cc0fe2d9fbc89408bc3d43[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""settings"": {
			      ""sort.field"": [ ""@timestamp""],             <1>
			      ""sort.order"": [ ""desc""]                    <2>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:412")]
		public void Line412()
		{
			// tag::ee5060d63a1b93aef6a4a879b647b4d4[]
			var response0 = new SearchResponse<object>();
			// end::ee5060d63a1b93aef6a4a879b647b4d4[]

			response0.MatchesExample(@"GET /_resolve/index/new_logs*");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:460")]
		public void Line460()
		{
			// tag::b37008aadb32356ea7a4072ddc5db3a6[]
			var response0 = new SearchResponse<object>();
			// end::b37008aadb32356ea7a4072ddc5db3a6[]

			response0.MatchesExample(@"PUT /_index_template/new_logs_data_stream
			{
			  ""index_patterns"": [ ""new_logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": {
			          ""type"": ""date_nanos""                 <1>
			        }
			      }
			    },
			    ""settings"": {
			      ""sort.field"": [ ""@timestamp""],          <2>
			      ""sort.order"": [ ""desc""]                 <3>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:513")]
		public void Line513()
		{
			// tag::3ab064461259c1ccc49f719bdbff9d2d[]
			var response0 = new SearchResponse<object>();
			// end::3ab064461259c1ccc49f719bdbff9d2d[]

			response0.MatchesExample(@"PUT /_data_stream/new_logs");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:536")]
		public void Line536()
		{
			// tag::a7164a0da5e76e422c6c8252c0699c4d[]
			var response0 = new SearchResponse<object>();
			// end::a7164a0da5e76e422c6c8252c0699c4d[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""indices.lifecycle.poll_interval"": ""1m""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:567")]
		public void Line567()
		{
			// tag::7e8438023b41733747a2f9976c634f68[]
			var response0 = new SearchResponse<object>();
			// end::7e8438023b41733747a2f9976c634f68[]

			response0.MatchesExample(@"GET /_data_stream/logs");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:613")]
		public void Line613()
		{
			// tag::e7ae44963d6adcbbfbe377868cb52b6b[]
			var response0 = new SearchResponse<object>();
			// end::e7ae44963d6adcbbfbe377868cb52b6b[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": "".ds-logs-000001""
			  },
			  ""dest"": {
			    ""index"": ""new_logs"",
			    ""op_type"": ""create""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:639")]
		public void Line639()
		{
			// tag::3b6c9619b318fd4ebd8db984faef41b7[]
			var response0 = new SearchResponse<object>();
			// end::3b6c9619b318fd4ebd8db984faef41b7[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""logs"",
			    ""query"": {
			      ""range"": {
			        ""@timestamp"": {
			          ""gte"": ""now-7d/d"",
			          ""lte"": ""now/d""
			        }
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""new_logs"",
			    ""op_type"": ""create""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:672")]
		public void Line672()
		{
			// tag::97d3523e74b9c0121f28907f5545c22c[]
			var response0 = new SearchResponse<object>();
			// end::97d3523e74b9c0121f28907f5545c22c[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""indices.lifecycle.poll_interval"": null
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/change-mappings-and-settings.asciidoc:696")]
		public void Line696()
		{
			// tag::9c863e1f9c2e91f78b6486ccb9cb42e7[]
			var response0 = new SearchResponse<object>();
			// end::9c863e1f9c2e91f78b6486ccb9cb42e7[]

			response0.MatchesExample(@"DELETE /_data_stream/logs");
		}
	}
}
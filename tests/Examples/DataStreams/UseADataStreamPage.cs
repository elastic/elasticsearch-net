using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.DataStreams
{
	public class UseADataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:73")]
		public void Line73()
		{
			// tag::69f6dfecb8e11bb215d88557af15a1c9[]
			var response0 = new SearchResponse<object>();
			// end::69f6dfecb8e11bb215d88557af15a1c9[]

			response0.MatchesExample(@"POST /logs/_doc/
			{
			  ""@timestamp"": ""2020-12-07T11:06:07.000Z"",
			  ""user"": {
			    ""id"": ""8a4f500d""
			  },
			  ""message"": ""Login successful""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:103")]
		public void Line103()
		{
			// tag::6bd19ade0029740347386b0705535c19[]
			var response0 = new SearchResponse<object>();
			// end::6bd19ade0029740347386b0705535c19[]

			response0.MatchesExample(@"PUT /logs/_bulk?refresh
			{""create"":{ }}
			{ ""@timestamp"": ""2020-12-08T11:04:05.000Z"", ""user"": { ""id"": ""vlb44hny"" }, ""message"": ""Login attempt failed"" }
			{""create"":{ }}
			{ ""@timestamp"": ""2020-12-08T11:06:07.000Z"", ""user"": { ""id"": ""8a4f500d"" }, ""message"": ""Login successful"" }
			{""create"":{ }}
			{ ""@timestamp"": ""2020-12-09T11:07:08.000Z"", ""user"": { ""id"": ""l7gk7f82"" }, ""message"": ""Logout successful"" }");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:127")]
		public void Line127()
		{
			// tag::25354d8957c8b2dea338f1fd3093a8ef[]
			var response0 = new SearchResponse<object>();
			// end::25354d8957c8b2dea338f1fd3093a8ef[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/lowercase_message_field
			{
			  ""description"" : ""Lowercases the message field value"",
			  ""processors"" : [
			    {
			      ""lowercase"" : {
			        ""field"" : ""message""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:152")]
		public void Line152()
		{
			// tag::effffb3ef22c1e5cdfd38260d8578871[]
			var response0 = new SearchResponse<object>();
			// end::effffb3ef22c1e5cdfd38260d8578871[]

			response0.MatchesExample(@"POST /logs/_doc?pipeline=lowercase_message_field
			{
			  ""@timestamp"": ""2020-12-08T11:12:01.000Z"",
			  ""user"": {
			    ""id"": ""I1YBEOxJ""
			  },
			  ""message"": ""LOGIN Successful""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:195")]
		public void Line195()
		{
			// tag::fe6d566ed541950cb273729eac3eced5[]
			var response0 = new SearchResponse<object>();
			// end::fe6d566ed541950cb273729eac3eced5[]

			response0.MatchesExample(@"GET /logs/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""range"": {
			          ""@timestamp"": {
			            ""gte"": ""now-1d/d"",
			            ""lt"": ""now/d""
			          }
			        }
			      },
			      ""should"": {
			        ""match"": {
			          ""message"": ""login successful""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:229")]
		public void Line229()
		{
			// tag::e86ae61a4574076cbdbcc003650f6929[]
			var response0 = new SearchResponse<object>();
			// end::e86ae61a4574076cbdbcc003650f6929[]

			response0.MatchesExample(@"GET /logs,logs_alt/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:243")]
		public void Line243()
		{
			// tag::58a1c65842b90f2be6261507705852b5[]
			var response0 = new SearchResponse<object>();
			// end::58a1c65842b90f2be6261507705852b5[]

			response0.MatchesExample(@"GET /logs*/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""vlb44hny""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:258")]
		public void Line258()
		{
			// tag::b03da9937ba8d9b354479e24bbb0268d[]
			var response0 = new SearchResponse<object>();
			// end::b03da9937ba8d9b354479e24bbb0268d[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""l7gk7f82""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:295")]
		public void Line295()
		{
			// tag::1311af3d6ce524cf7bf6bbc6438e6632[]
			var response0 = new SearchResponse<object>();
			// end::1311af3d6ce524cf7bf6bbc6438e6632[]

			response0.MatchesExample(@"POST /logs/_rollover/");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:330")]
		public void Line330()
		{
			// tag::6a5043d1d046a30a35409e8892b43fca[]
			var response0 = new SearchResponse<object>();
			// end::6a5043d1d046a30a35409e8892b43fca[]

			response0.MatchesExample(@"GET /_cat/indices/logs?v&s=index&h=index,status");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:353")]
		public void Line353()
		{
			// tag::f2c8bff4454dc5302ba98791295e845c[]
			var response0 = new SearchResponse<object>();
			// end::f2c8bff4454dc5302ba98791295e845c[]

			response0.MatchesExample(@"POST /logs/_open/");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:442")]
		public void Line442()
		{
			// tag::37df24537144741d90033446aad57c54[]
			var response0 = new SearchResponse<object>();
			// end::37df24537144741d90033446aad57c54[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""archive""
			  },
			  ""dest"": {
			    ""index"": ""logs"",
			    ""op_type"": ""create""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:468")]
		public void Line468()
		{
			// tag::d473d961c65d13ca7cfc3e29c6ad8445[]
			var response0 = new SearchResponse<object>();
			// end::d473d961c65d13ca7cfc3e29c6ad8445[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""logs""
			  },
			  ""dest"": {
			    ""index"": ""archive""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:500")]
		public void Line500()
		{
			// tag::3c2e8bca9ab0e87c259dffb6e4b8112f[]
			var response0 = new SearchResponse<object>();
			// end::3c2e8bca9ab0e87c259dffb6e4b8112f[]

			response0.MatchesExample(@"POST /logs/_update_by_query
			{
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""l7gk7f82""
			    }
			  },
			  ""script"": {
			    ""source"": ""ctx._source.user.id = params.new_id"",
			    ""params"": {
			      ""new_id"": ""XgdX0NoX""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:527")]
		public void Line527()
		{
			// tag::357f4038f55d8fe06ac868066c8dc143[]
			var response0 = new SearchResponse<object>();
			// end::357f4038f55d8fe06ac868066c8dc143[]

			response0.MatchesExample(@"POST /logs/_delete_by_query
			{
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""vlb44hny""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:568")]
		public void Line568()
		{
			// tag::c0fe32f4b64bb6a5775c1dc56f881340[]
			var response0 = new SearchResponse<object>();
			// end::c0fe32f4b64bb6a5775c1dc56f881340[]

			response0.MatchesExample(@"GET /logs/_search
			{
			  ""seq_no_primary_term"": true,
			  ""query"": {
			    ""match"": {
			      ""user.id"": ""yWIumJd7""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:645")]
		public void Line645()
		{
			// tag::5674aa7a485daf5e9b0e3bc5ff1fe392[]
			var response0 = new SearchResponse<object>();
			// end::5674aa7a485daf5e9b0e3bc5ff1fe392[]

			response0.MatchesExample(@"PUT /.ds-logs-000003/_doc/bfspvnIBr7VVZlfp2lqX?if_seq_no=0&if_primary_term=1
			{
			  ""@timestamp"": ""2020-12-07T11:06:07.000Z"",
			  ""user"": {
			    ""id"": ""8a4f500d""
			  },
			  ""message"": ""Login successful""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:668")]
		public void Line668()
		{
			// tag::24e7b9e87b9aeb221e206b640b6f5cb9[]
			var response0 = new SearchResponse<object>();
			// end::24e7b9e87b9aeb221e206b640b6f5cb9[]

			response0.MatchesExample(@"DELETE /.ds-logs-000003/_doc/bfspvnIBr7VVZlfp2lqX");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/use-a-data-stream.asciidoc:692")]
		public void Line692()
		{
			// tag::6c8bc724b661046efb7b6b24a1d345c3[]
			var response0 = new SearchResponse<object>();
			// end::6c8bc724b661046efb7b6b24a1d345c3[]

			response0.MatchesExample(@"PUT /_bulk?refresh
			{ ""index"": { ""_index"": "".ds-logs-000003"", ""_id"": ""bfspvnIBr7VVZlfp2lqX"", ""if_seq_no"": 0, ""if_primary_term"": 1 } }
			{ ""@timestamp"": ""2020-12-07T11:06:07.000Z"", ""user"": { ""id"": ""8a4f500d"" }, ""message"": ""Login successful"" }");
		}
	}
}
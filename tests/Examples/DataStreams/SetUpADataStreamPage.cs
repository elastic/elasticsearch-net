using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.DataStreams
{
	public class SetUpADataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:67")]
		public void Line67()
		{
			// tag::f9be1f5f76008daf901192c86b45c02c[]
			var response0 = new SearchResponse<object>();
			// end::f9be1f5f76008daf901192c86b45c02c[]

			response0.MatchesExample(@"PUT /_ilm/policy/logs_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:116")]
		public void Line116()
		{
			// tag::aa747d8125e5326384a29e346d4ff851[]
			var response0 = new SearchResponse<object>();
			// end::aa747d8125e5326384a29e346d4ff851[]

			response0.MatchesExample(@"GET /_resolve/index/logs*");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:173")]
		public void Line173()
		{
			// tag::038dec96752039127df8a2c2fce716ea[]
			var response0 = new SearchResponse<object>();
			// end::038dec96752039127df8a2c2fce716ea[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""settings"": {
			      ""index.lifecycle.name"": ""logs_policy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:190")]
		public void Line190()
		{
			// tag::48ccc72a6963a7e874fde8462928501a[]
			var response0 = new SearchResponse<object>();
			// end::48ccc72a6963a7e874fde8462928501a[]

			response0.MatchesExample(@"PUT /_index_template/logs_data_stream
			{
			  ""index_patterns"": [ ""logs*"" ],
			  ""data_stream"": { },
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": { ""type"": ""date_nanos"" }    <1>
			      }
			    },
			    ""settings"": {
			      ""index.lifecycle.name"": ""logs_policy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:244")]
		public void Line244()
		{
			// tag::1ce0576497abe7e1a9d6ca7fbcf3e910[]
			var response0 = new SearchResponse<object>();
			// end::1ce0576497abe7e1a9d6ca7fbcf3e910[]

			response0.MatchesExample(@"POST /logs/_doc/
			{
			  ""@timestamp"": ""2020-12-06T11:04:05.000Z"",
			  ""user"": {
			    ""id"": ""vlb44hny""
			  },
			  ""message"": ""Login attempt failed""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:294")]
		public void Line294()
		{
			// tag::ef335d1d0b71879ea1b07be1d20a2a1c[]
			var response0 = new SearchResponse<object>();
			// end::ef335d1d0b71879ea1b07be1d20a2a1c[]

			response0.MatchesExample(@"PUT /_data_stream/logs_alt");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:334")]
		public void Line334()
		{
			// tag::7e8438023b41733747a2f9976c634f68[]
			var response0 = new SearchResponse<object>();
			// end::7e8438023b41733747a2f9976c634f68[]

			response0.MatchesExample(@"GET /_data_stream/logs");
		}

		[U(Skip = "Example not implemented")]
		[Description("data-streams/set-up-a-data-stream.asciidoc:399")]
		public void Line399()
		{
			// tag::9c863e1f9c2e91f78b6486ccb9cb42e7[]
			var response0 = new SearchResponse<object>();
			// end::9c863e1f9c2e91f78b6486ccb9cb42e7[]

			response0.MatchesExample(@"DELETE /_data_stream/logs");
		}
	}
}
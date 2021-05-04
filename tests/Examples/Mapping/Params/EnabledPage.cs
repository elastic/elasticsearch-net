// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class EnabledPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/enabled.asciidoc:17")]
		public void Line17()
		{
			// tag::b0b00ab5b673d747d36deabbc4359859[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::b0b00ab5b673d747d36deabbc4359859[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user_id"": {
			        ""type"":  ""keyword""
			      },
			      ""last_updated"": {
			        ""type"": ""date""
			      },
			      ""session_data"": { \<1>
			        ""type"": ""object"",
			        ""enabled"": false
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/session_1
			{
			  ""user_id"": ""kimchy"",
			  ""session_data"": { \<2>
			    ""arbitrary_object"": {
			      ""some_array"": [ ""foo"", ""bar"", { ""baz"": 2 } ]
			    }
			  },
			  ""last_updated"": ""2015-12-06T18:20:22""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/session_2
			{
			  ""user_id"": ""jpountz"",
			  ""session_data"": ""none"", \<3>
			  ""last_updated"": ""2015-12-06T18:22:13""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/enabled.asciidoc:64")]
		public void Line64()
		{
			// tag::d31274ad53af4baa23ec3e5000783cbd[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::d31274ad53af4baa23ec3e5000783cbd[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""enabled"": false \<1>
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/session_1
			{
			  ""user_id"": ""kimchy"",
			  ""session_data"": {
			    ""arbitrary_object"": {
			      ""some_array"": [ ""foo"", ""bar"", { ""baz"": 2 } ]
			    }
			  },
			  ""last_updated"": ""2015-12-06T18:20:22""
			}");

			response2.MatchesExample(@"GET my_index/_doc/session_1 \<2>");

			response3.MatchesExample(@"GET my_index/_mapping \<3>");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/enabled.asciidoc:99")]
		public void Line99()
		{
			// tag::e93514654ea0c7c9f15cda0eed61a292[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::e93514654ea0c7c9f15cda0eed61a292[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""session_data"": {
			        ""type"": ""object"",
			        ""enabled"": false
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/session_1
			{
			  ""session_data"": ""foo bar"" \<1>
			}");
		}
	}
}

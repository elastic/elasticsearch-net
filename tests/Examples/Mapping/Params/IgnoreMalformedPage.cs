// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class IgnoreMalformedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/ignore-malformed.asciidoc:16")]
		public void Line16()
		{
			// tag::56af112ba65955f3ca5ef61a199c0daa[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::56af112ba65955f3ca5ef61a199c0daa[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer"",
			        ""ignore_malformed"": true
			      },
			      ""number_two"": {
			        ""type"": ""integer""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"":       ""Some text value"",
			  ""number_one"": ""foo"" \<1>
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""text"":       ""Some text value"",
			  ""number_two"": ""foo"" \<2>
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/ignore-malformed.asciidoc:69")]
		public void Line69()
		{
			// tag::835faff0d2e8874b7b9693376fa7fc57[]
			var response0 = new SearchResponse<object>();
			// end::835faff0d2e8874b7b9693376fa7fc57[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""index.mapping.ignore_malformed"": true \<1>
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": { \<1>
			        ""type"": ""byte""
			      },
			      ""number_two"": {
			        ""type"": ""integer"",
			        ""ignore_malformed"": false \<2>
			      }
			    }
			  }
			}");
		}
	}
}

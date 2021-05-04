// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class BooleanPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/boolean.asciidoc:22")]
		public void Line22()
		{
			// tag::1c1be1df747c9f8ecc9f82e980387d8f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::1c1be1df747c9f8ecc9f82e980387d8f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""is_published"": {
			        ""type"": ""boolean""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_doc/1
			{
			  ""is_published"": ""true"" \<1>
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""is_published"": true \<2>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/boolean.asciidoc:58")]
		public void Line58()
		{
			// tag::636ec3c018ac15ec11caf6f3d835a08c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::636ec3c018ac15ec11caf6f3d835a08c[]

			response0.MatchesExample(@"POST my_index/_doc/1
			{
			  ""is_published"": true
			}");

			response1.MatchesExample(@"POST my_index/_doc/2
			{
			  ""is_published"": false
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""aggs"": {
			    ""publish_state"": {
			      ""terms"": {
			        ""field"": ""is_published""
			      }
			    }
			  },
			  ""script_fields"": {
			    ""is_published"": {
			      ""script"": {
			        ""lang"": ""painless"",
			        ""source"": ""doc['is_published'].value""
			      }
			    }
			  }
			}");
		}
	}
}

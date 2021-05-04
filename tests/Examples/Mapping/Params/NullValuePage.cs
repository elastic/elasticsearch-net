// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class NullValuePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/null-value.asciidoc:12")]
		public void Line12()
		{
			// tag::463e64093c0dfba910eb5b248085584f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::463e64093c0dfba910eb5b248085584f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""status_code"": {
			        ""type"":       ""keyword"",
			        ""null_value"": ""NULL"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""status_code"": null
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""status_code"": [] \<2>
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""status_code"": ""NULL"" \<3>
			    }
			  }
			}");
		}
	}
}

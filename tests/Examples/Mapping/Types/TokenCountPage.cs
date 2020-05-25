// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class TokenCountPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/token-count.asciidoc:14")]
		public void Line14()
		{
			// tag::98c3d643f71c1fd71238ebb748e846e7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::98c3d643f71c1fd71238ebb748e846e7[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": { \<1>
			        ""type"": ""text"",
			        ""fields"": {
			          ""length"": { \<2>
			            ""type"":     ""token_count"",
			            ""analyzer"": ""standard""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""name"": ""John Smith"" }");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""name"": ""Rachel Alice Williams"" }");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""term"": {
			      ""name.length"": 3 \<3>
			    }
			  }
			}");
		}
	}
}

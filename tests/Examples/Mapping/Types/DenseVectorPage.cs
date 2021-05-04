// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class DenseVectorPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/dense-vector.asciidoc:20")]
		public void Line20()
		{
			// tag::7c7b74084cc9f18b085c25a208bd1306[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::7c7b74084cc9f18b085c25a208bd1306[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_vector"": {
			        ""type"": ""dense_vector"",
			        ""dims"": 3  \<1>
			      },
			      ""my_text"" : {
			        ""type"" : ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_text"" : ""text1"",
			  ""my_vector"" : [0.5, 10, 6]
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_text"" : ""text2"",
			  ""my_vector"" : [-0.5, 10, 10]
			}");

			response3.MatchesExample(@"");
		}
	}
}

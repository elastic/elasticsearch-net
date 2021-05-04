// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class IndexOptionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/index-options.asciidoc:34")]
		public void Line34()
		{
			// tag::3a24ebb542f657420fcd8fdf3f757ce6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::3a24ebb542f657420fcd8fdf3f757ce6[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""text"": {
			        ""type"": ""text"",
			        ""index_options"": ""offsets""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"": ""Quick brown fox""
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""text"": ""brown fox""
			    }
			  },
			  ""highlight"": {
			    ""fields"": {
			      ""text"": {} \<1>
			    }
			  }
			}");
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class SearchAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/search-analyzer.asciidoc:16")]
		public void Line16()
		{
			// tag::60677e5144fed659e8417b7fa9964285[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::60677e5144fed659e8417b7fa9964285[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""autocomplete_filter"": {
			          ""type"": ""edge_ngram"",
			          ""min_gram"": 1,
			          ""max_gram"": 20
			        }
			      },
			      ""analyzer"": {
			        ""autocomplete"": { \<1>
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""autocomplete_filter""
			          ]
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""text"": {
			        ""type"": ""text"",
			        ""analyzer"": ""autocomplete"", \<2>
			        ""search_analyzer"": ""standard"" \<2>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"": ""Quick Brown Fox"" \<3>
			}");

			response2.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""text"": {
			        ""query"": ""Quick Br"", \<4>
			        ""operator"": ""and""
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"");
		}
	}
}

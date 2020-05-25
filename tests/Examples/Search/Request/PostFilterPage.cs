// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class PostFilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/post-filter.asciidoc:11")]
		public void Line11()
		{
			// tag::35390274db3acad03eb77b2376c57e40[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::35390274db3acad03eb77b2376c57e40[]

			response0.MatchesExample(@"PUT /shirts
			{
			    ""mappings"": {
			        ""properties"": {
			            ""brand"": { ""type"": ""keyword""},
			            ""color"": { ""type"": ""keyword""},
			            ""model"": { ""type"": ""keyword""}
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /shirts/_doc/1?refresh
			{
			    ""brand"": ""gucci"",
			    ""color"": ""red"",
			    ""model"": ""slim""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/post-filter.asciidoc:40")]
		public void Line40()
		{
			// tag::f83eb6605c7c56e297a494b318400ef0[]
			var response0 = new SearchResponse<object>();
			// end::f83eb6605c7c56e297a494b318400ef0[]

			response0.MatchesExample(@"GET /shirts/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""filter"": [
			        { ""term"": { ""color"": ""red""   }},
			        { ""term"": { ""brand"": ""gucci"" }}
			      ]
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/post-filter.asciidoc:63")]
		public void Line63()
		{
			// tag::81f1b1e1d5c81683b6bf471c469e6046[]
			var response0 = new SearchResponse<object>();
			// end::81f1b1e1d5c81683b6bf471c469e6046[]

			response0.MatchesExample(@"GET /shirts/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""filter"": [
			        { ""term"": { ""color"": ""red""   }},
			        { ""term"": { ""brand"": ""gucci"" }}
			      ]
			    }
			  },
			  ""aggs"": {
			    ""models"": {
			      ""terms"": { ""field"": ""model"" } \<1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/post-filter.asciidoc:94")]
		public void Line94()
		{
			// tag::48313f620c2871b6f4019b66be730109[]
			var response0 = new SearchResponse<object>();
			// end::48313f620c2871b6f4019b66be730109[]

			response0.MatchesExample(@"GET /shirts/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""filter"": {
			        ""term"": { ""brand"": ""gucci"" } \<1>
			      }
			    }
			  },
			  ""aggs"": {
			    ""colors"": {
			      ""terms"": { ""field"": ""color"" } \<2>
			    },
			    ""color_red"": {
			      ""filter"": {
			        ""term"": { ""color"": ""red"" } \<3>
			      },
			      ""aggs"": {
			        ""models"": {
			          ""terms"": { ""field"": ""model"" } \<3>
			        }
			      }
			    }
			  },
			  ""post_filter"": { \<4>
			    ""term"": { ""color"": ""red"" }
			  }
			}");
		}
	}
}

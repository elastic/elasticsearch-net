// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class MultiFieldsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/params/multi-fields.asciidoc:10")]
		public void Line10()
		{
			// tag::5271f4ff29bb48838396e5a674664ee0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::5271f4ff29bb48838396e5a674664ee0[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""city"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""raw"": { \<1>
			            ""type"":  ""keyword""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""city"": ""New York""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""city"": ""York""
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""city"": ""york"" \<2>
			    }
			  },
			  ""sort"": {
			    ""city.raw"": ""asc"" \<3>
			  },
			  ""aggs"": {
			    ""Cities"": {
			      ""terms"": {
			        ""field"": ""city.raw"" \<3>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/multi-fields.asciidoc:75")]
		public void Line75()
		{
			// tag::fc8097bdfb6f3a4017bf4186ccca8a84[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::fc8097bdfb6f3a4017bf4186ccca8a84[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""text"": { \<1>
			        ""type"": ""text"",
			        ""fields"": {
			          ""english"": { \<2>
			            ""type"":     ""text"",
			            ""analyzer"": ""english""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""text"": ""quick brown fox"" } \<3>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""text"": ""quick brown foxes"" } \<3>");

			response3.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""multi_match"": {
			      ""query"": ""quick brown foxes"",
			      ""fields"": [ \<4>
			        ""text"",
			        ""text.english""
			      ],
			      ""type"": ""most_fields"" \<4>
			    }
			  }
			}");
		}
	}
}

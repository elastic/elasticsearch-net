// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.HowTo.Recipes
{
	public class ScoringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/scoring.asciidoc:123")]
		public void Line123()
		{
			// tag::a0f15dd7fcb07bc8543fe04c2907d4b9[]
			var response0 = new SearchResponse<object>();
			// end::a0f15dd7fcb07bc8543fe04c2907d4b9[]

			response0.MatchesExample(@"GET index/_search
			{
			    ""query"" : {
			        ""script_score"" : {
			            ""query"" : {
			                ""match"": { ""body"": ""elasticsearch"" }
			            },
			            ""script"" : {
			                ""source"" : ""_score * saturation(doc['pagerank'].value, 10)"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/recipes/scoring.asciidoc:168")]
		public void Line168()
		{
			// tag::0dfa66a019712e413652c5eddd057ba8[]
			var response0 = new SearchResponse<object>();
			// end::0dfa66a019712e413652c5eddd057ba8[]

			response0.MatchesExample(@"GET _search
			{
			    ""query"" : {
			        ""bool"" : {
			            ""must"": {
			                ""match"": { ""body"": ""elasticsearch"" }
			            },
			            ""should"": {
			                ""rank_feature"": {
			                    ""field"": ""pagerank"", \<1>
			                    ""saturation"": {
			                        ""pivot"": 10
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class FuzzyQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/fuzzy-query.asciidoc:29")]
		public void Line29()
		{
			// tag::10dd8b5da64f1f6af031706dd50bc9b5[]
			var response0 = new SearchResponse<object>();
			// end::10dd8b5da64f1f6af031706dd50bc9b5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""fuzzy"": {
			            ""user"": {
			                ""value"": ""ki""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/fuzzy-query.asciidoc:46")]
		public void Line46()
		{
			// tag::8baebb670ca5624d7920ccac4afdff06[]
			var response0 = new SearchResponse<object>();
			// end::8baebb670ca5624d7920ccac4afdff06[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""fuzzy"": {
			            ""user"": {
			                ""value"": ""ki"",
			                ""fuzziness"": ""AUTO"",
			                ""max_expansions"": 50,
			                ""prefix_length"": 0,
			                ""transpositions"": true,
			                ""rewrite"": ""constant_score""
			            }
			        }
			    }
			}");
		}
	}
}

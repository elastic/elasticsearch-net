// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class SearchAfterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/search-after.asciidoc:14")]
		public void Line14()
		{
			// tag::402ee4bf8e2e386d5f9100fdaf13a6d6[]
			var response0 = new SearchResponse<object>();
			// end::402ee4bf8e2e386d5f9100fdaf13a6d6[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""size"": 10,
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    },
			    ""sort"": [
			        {""date"": ""asc""},
			        {""tie_breaker_id"": ""asc""}      \<1>
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/search-after.asciidoc:57")]
		public void Line57()
		{
			// tag::525ec32997125d401f9c128ca450cefa[]
			var response0 = new SearchResponse<object>();
			// end::525ec32997125d401f9c128ca450cefa[]

			response0.MatchesExample(@"GET twitter/_search
			{
			    ""size"": 10,
			    ""query"": {
			        ""match"" : {
			            ""title"" : ""elasticsearch""
			        }
			    },
			    ""search_after"": [1463538857, ""654323""],
			    ""sort"": [
			        {""date"": ""asc""},
			        {""tie_breaker_id"": ""asc""}
			    ]
			}");
		}
	}
}

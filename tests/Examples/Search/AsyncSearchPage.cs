// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Search
{
	public class AsyncSearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/async-search.asciidoc:17")]
		public void Line17()
		{
			// tag::e50f9492af9e0174c7ecbe5ad7f09d74[]
			var response0 = new SearchResponse<object>();
			// end::e50f9492af9e0174c7ecbe5ad7f09d74[]

			response0.MatchesExample(@"POST /sales*/_async_search?size=0
			{
			    ""sort"" : [
			      { ""date"" : {""order"" : ""asc""} }
			    ],
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""calendar_interval"": ""1d""
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/async-search.asciidoc:146")]
		public void Line146()
		{
			// tag::14b81f96297952970b78a3216e059596[]
			var response0 = new SearchResponse<object>();
			// end::14b81f96297952970b78a3216e059596[]

			response0.MatchesExample(@"GET /_async_search/FmRldE8zREVEUzA2ZVpUeGs2ejJFUFEaMkZ5QTVrSTZSaVN3WlNFVmtlWHJsdzoxMDc=");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/async-search.asciidoc:233")]
		public void Line233()
		{
			// tag::7a3a7fbd81e5050b42e8c1eca26c7c1d[]
			var response0 = new SearchResponse<object>();
			// end::7a3a7fbd81e5050b42e8c1eca26c7c1d[]

			response0.MatchesExample(@"DELETE /_async_search/FmRldE8zREVEUzA2ZVpUeGs2ejJFUFEaMkZ5QTVrSTZSaVN3WlNFVmtlWHJsdzoxMDc=");
		}
	}
}

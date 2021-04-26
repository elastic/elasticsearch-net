/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

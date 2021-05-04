// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class DisMaxQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/dis-max-query.asciidoc:18")]
		public void Line18()
		{
			// tag::fcf5a593cfe8809d98a5239ad9c82038[]
			var response0 = new SearchResponse<object>();
			// end::fcf5a593cfe8809d98a5239ad9c82038[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""dis_max"" : {
			            ""queries"" : [
			                { ""term"" : { ""title"" : ""Quick pets"" }},
			                { ""term"" : { ""body"" : ""Quick pets"" }}
			            ],
			            ""tie_breaker"" : 0.7
			        }
			    }
			}");
		}
	}
}

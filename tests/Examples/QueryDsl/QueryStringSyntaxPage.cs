// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class QueryStringSyntaxPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/query-string-syntax.asciidoc:294")]
		public void Line294()
		{
			// tag::8ca595edc1e1f1ae6bc3ee05e0aa1e32[]
			var response0 = new SearchResponse<object>();
			// end::8ca595edc1e1f1ae6bc3ee05e0aa1e32[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			  ""query"" : {
			    ""query_string"" : {
			      ""query"" : ""kimchy\\!"",
			      ""fields""  : [""user""]
			    }
			  }
			}");
		}
	}
}

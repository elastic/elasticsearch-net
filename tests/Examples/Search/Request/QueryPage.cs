// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class QueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/query.asciidoc:8")]
		public void Line8()
		{
			// tag::a8e19886f6b4792def0381c3f8cf2b5c[]
			var response0 = new SearchResponse<object>();
			// end::a8e19886f6b4792def0381c3f8cf2b5c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class SeqNoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/seq-no.asciidoc:8")]
		public void Line8()
		{
			// tag::63965d439716ed6d18d30baef09001a5[]
			var response0 = new SearchResponse<object>();
			// end::63965d439716ed6d18d30baef09001a5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""seq_no_primary_term"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
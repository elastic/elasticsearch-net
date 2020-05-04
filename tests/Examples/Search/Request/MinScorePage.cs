// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class MinScorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/min-score.asciidoc:8")]
		public void Line8()
		{
			// tag::8e8ceac8fc99348f885f85ff714557fd[]
			var response0 = new SearchResponse<object>();
			// end::8e8ceac8fc99348f885f85ff714557fd[]

			response0.MatchesExample(@"GET /_search
			{
			    ""min_score"": 0.5,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
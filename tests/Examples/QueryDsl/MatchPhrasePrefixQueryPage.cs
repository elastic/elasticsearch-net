// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MatchPhrasePrefixQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-phrase-prefix-query.asciidoc:22")]
		public void Line22()
		{
			// tag::ca0da81281347e33116710efd36697c8[]
			var response0 = new SearchResponse<object>();
			// end::ca0da81281347e33116710efd36697c8[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_phrase_prefix"" : {
			            ""message"" : {
			                ""query"" : ""quick brown f""
			            }
			        }
			    }
			}");
		}
	}
}

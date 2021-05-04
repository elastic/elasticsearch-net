// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class ConstantScoreQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/constant-score-query.asciidoc:12")]
		public void Line12()
		{
			// tag::d59a084640acf2f5c51d3068d38b5fc0[]
			var response0 = new SearchResponse<object>();
			// end::d59a084640acf2f5c51d3068d38b5fc0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""term"" : { ""user"" : ""kimchy""}
			            },
			            ""boost"" : 1.2
			        }
			    }
			}");
		}
	}
}

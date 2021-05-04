// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class PrefixQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/prefix-query.asciidoc:16")]
		public void Line16()
		{
			// tag::81514791349e0e79ac565160e42889c0[]
			var response0 = new SearchResponse<object>();
			// end::81514791349e0e79ac565160e42889c0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"": {
			            ""user"": {
			                ""value"": ""ki""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/prefix-query.asciidoc:53")]
		public void Line53()
		{
			// tag::32ea547cefa2976c8c3c2eb45a2a4ff4[]
			var response0 = new SearchResponse<object>();
			// end::32ea547cefa2976c8c3c2eb45a2a4ff4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""prefix"" : { ""user"" : ""ki"" }
			    }
			}");
		}
	}
}

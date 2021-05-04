// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class MatchBoolPrefixQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:13")]
		public void Line13()
		{
			// tag::79c7e8a98c47fad3e96c654d34aa049a[]
			var response0 = new SearchResponse<object>();
			// end::79c7e8a98c47fad3e96c654d34aa049a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"" : ""quick brown f""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:28")]
		public void Line28()
		{
			// tag::effc6b4784aca12691de5d5782c0384b[]
			var response0 = new SearchResponse<object>();
			// end::effc6b4784aca12691de5d5782c0384b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""should"": [
			                { ""term"": { ""message"": ""quick"" }},
			                { ""term"": { ""message"": ""brown"" }},
			                { ""prefix"": { ""message"": ""f""}}
			            ]
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/match-bool-prefix-query.asciidoc:59")]
		public void Line59()
		{
			// tag::953aab6cbd12a4f034cf02bf34d62a72[]
			var response0 = new SearchResponse<object>();
			// end::953aab6cbd12a4f034cf02bf34d62a72[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_bool_prefix"" : {
			            ""message"": {
			                ""query"": ""quick brown f"",
			                ""analyzer"": ""keyword""
			            }
			        }
			    }
			}");
		}
	}
}

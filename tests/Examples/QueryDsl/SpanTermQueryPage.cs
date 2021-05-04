// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanTermQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-term-query.asciidoc:11")]
		public void Line11()
		{
			// tag::086b2bbc4c3bfc2310c22d10db42cb82[]
			var response0 = new SearchResponse<object>();
			// end::086b2bbc4c3bfc2310c22d10db42cb82[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-term-query.asciidoc:23")]
		public void Line23()
		{
			// tag::5add42087c83b7e498f8f43e91f343d4[]
			var response0 = new SearchResponse<object>();
			// end::5add42087c83b7e498f8f43e91f343d4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			       ""span_term"" : { ""user"" : { ""value"" : ""kimchy"", ""boost"" : 2.0 } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-term-query.asciidoc:35")]
		public void Line35()
		{
			// tag::2a07d189553602066fefdb6b7cbdf542[]
			var response0 = new SearchResponse<object>();
			// end::2a07d189553602066fefdb6b7cbdf542[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_term"" : { ""user"" : { ""term"" : ""kimchy"", ""boost"" : 2.0 } }
			    }
			}");
		}
	}
}

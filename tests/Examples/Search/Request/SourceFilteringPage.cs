// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class SourceFilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/source-filtering.asciidoc:15")]
		public void Line15()
		{
			// tag::08c5b266f5e5534dc094346974cf7386[]
			var response0 = new SearchResponse<object>();
			// end::08c5b266f5e5534dc094346974cf7386[]

			response0.MatchesExample(@"GET /_search
			{
			    ""_source"": false,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/source-filtering.asciidoc:30")]
		public void Line30()
		{
			// tag::5c10e00c99b338353b3e486e94be253e[]
			var response0 = new SearchResponse<object>();
			// end::5c10e00c99b338353b3e486e94be253e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""_source"": ""obj.*"",
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/source-filtering.asciidoc:43")]
		public void Line43()
		{
			// tag::160ae4ff9c53b8a98700caed0e82d7fe[]
			var response0 = new SearchResponse<object>();
			// end::160ae4ff9c53b8a98700caed0e82d7fe[]

			response0.MatchesExample(@"GET /_search
			{
			    ""_source"": [ ""obj1.*"", ""obj2.*"" ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/source-filtering.asciidoc:60")]
		public void Line60()
		{
			// tag::1e86a78433a0748970d6c3922a34898c[]
			var response0 = new SearchResponse<object>();
			// end::1e86a78433a0748970d6c3922a34898c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""_source"": {
			        ""includes"": [ ""obj1.*"", ""obj2.*"" ],
			        ""excludes"": [ ""*.description"" ]
			    },
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}

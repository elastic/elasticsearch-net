// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class VersionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/version.asciidoc:7")]
		public void Line7()
		{
			// tag::9535be36eac8a589bd6bf7b7228eefd7[]
			var response0 = new SearchResponse<object>();
			// end::9535be36eac8a589bd6bf7b7228eefd7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""version"": true,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}
// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class FromSizePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/from-size.asciidoc:14")]
		public void Line14()
		{
			// tag::9a26759ccbd338224ecaacf7c49ab08e[]
			var response0 = new SearchResponse<object>();
			// end::9a26759ccbd338224ecaacf7c49ab08e[]

			response0.MatchesExample(@"GET /_search
			{
			    ""from"" : 0, ""size"" : 10,
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}

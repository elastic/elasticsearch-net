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
		[Description("search/request/from-size.asciidoc:22")]
		public void Line22()
		{
			// tag::1e50d993bd6517e6c381e82d09f0389e[]
			var response0 = new SearchResponse<object>();
			// end::1e50d993bd6517e6c381e82d09f0389e[]

			response0.MatchesExample(@"GET /_search
			{
			  ""from"": 5,
			  ""size"": 20,
			  ""query"": {
			    ""term"": {
			      ""user.id"": ""8a4f500d""
			    }
			  }
			}");
		}
	}
}

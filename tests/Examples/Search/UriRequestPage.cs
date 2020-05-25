// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class UriRequestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/uri-request.asciidoc:7")]
		public void Line7()
		{
			// tag::68188db64fc50a9b35e5646493b00d2c[]
			var response0 = new SearchResponse<object>();
			// end::68188db64fc50a9b35e5646493b00d2c[]

			response0.MatchesExample(@"GET twitter/_search?q=user:kimchy");
		}
	}
}

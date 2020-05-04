// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class IndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/indices.asciidoc:92")]
		public void Line92()
		{
			// tag::073539a7e38be3cdf13008330b6a536a[]
			var response0 = new SearchResponse<object>();
			// end::073539a7e38be3cdf13008330b6a536a[]

			response0.MatchesExample(@"GET /_cat/indices/twi*?v&s=index");
		}
	}
}
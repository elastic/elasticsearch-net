// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class CountPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/count.asciidoc:57")]
		public void Line57()
		{
			// tag::e7553d4bb4fd82d8f80a4d7af2624afb[]
			var response0 = new SearchResponse<object>();
			// end::e7553d4bb4fd82d8f80a4d7af2624afb[]

			response0.MatchesExample(@"GET /_cat/count/twitter?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/count.asciidoc:79")]
		public void Line79()
		{
			// tag::0a1f8ad54b1d8c9feeaceaeed16c8490[]
			var response0 = new SearchResponse<object>();
			// end::0a1f8ad54b1d8c9feeaceaeed16c8490[]

			response0.MatchesExample(@"GET /_cat/count?v");
		}
	}
}

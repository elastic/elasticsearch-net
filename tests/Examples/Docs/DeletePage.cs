// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;

namespace Examples.Docs
{
	public class DeletePage : ExampleBase
	{

		[U]
		[Description("docs/delete.asciidoc:71")]
		public void Line71()
		{
			// tag::47b5ff897f26e9c943cee5c06034181d[]
			var deleteResponse = client.Delete<Tweet>(1, d => d
				.Index("twitter")
				.Routing("kimchy")
			);
			// end::47b5ff897f26e9c943cee5c06034181d[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1?routing=kimchy");
		}

		[U]
		[Description("docs/delete.asciidoc:127")]
		public void Line127()
		{
			// tag::d90a84a24a407731dfc1929ac8327746[]
			var deleteResponse = client.Delete<Tweet>(1, d => d
				.Index("twitter")
				.Timeout("5m")
			);
			// end::d90a84a24a407731dfc1929ac8327746[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1?timeout=5m");
		}
		[U]
		[Description("docs/delete.asciidoc:168")]
		public void Line168()
		{
			// tag::c5e5873783246c7b1c01d8464fed72c4[]
			var deleteResponse = client.Delete<Tweet>(1, d => d.Index("twitter"));
			// end::c5e5873783246c7b1c01d8464fed72c4[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1");
		}
	}
}

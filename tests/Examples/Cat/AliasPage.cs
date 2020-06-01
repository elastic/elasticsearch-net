// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class AliasPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/alias.asciidoc:75")]
		public void Line75()
		{
			// tag::a003467caeafcb2a935522efb83080cb[]
			var response0 = new SearchResponse<object>();
			// end::a003467caeafcb2a935522efb83080cb[]

			response0.MatchesExample(@"GET /_cat/aliases?v");
		}
	}
}

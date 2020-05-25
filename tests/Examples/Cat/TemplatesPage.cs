// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class TemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/templates.asciidoc:50")]
		public void Line50()
		{
			// tag::289e6033c96f931844770114113cad6a[]
			var response0 = new SearchResponse<object>();
			// end::289e6033c96f931844770114113cad6a[]

			response0.MatchesExample(@"GET /_cat/templates?v&s=name");
		}
	}
}

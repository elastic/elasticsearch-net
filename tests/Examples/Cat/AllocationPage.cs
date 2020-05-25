// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class AllocationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/allocation.asciidoc:47")]
		public void Line47()
		{
			// tag::5c7ece1f30267adabdb832424871900a[]
			var response0 = new SearchResponse<object>();
			// end::5c7ece1f30267adabdb832424871900a[]

			response0.MatchesExample(@"GET /_cat/allocation?v");
		}
	}
}

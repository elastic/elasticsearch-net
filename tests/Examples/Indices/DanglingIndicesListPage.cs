// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class DanglingIndicesListPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-indices-list.asciidoc:13")]
		public void Line13()
		{
			// tag::21c1e6ee886140ce0cd67184dd19b981[]
			var response0 = new SearchResponse<object>();
			// end::21c1e6ee886140ce0cd67184dd19b981[]

			response0.MatchesExample(@"GET /_dangling");
		}
	}
}
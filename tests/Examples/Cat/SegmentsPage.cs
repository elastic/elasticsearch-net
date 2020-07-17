// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class SegmentsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/segments.asciidoc:116")]
		public void Line116()
		{
			// tag::6f507269ad5b31d2bb0885c1b18aac1a[]
			var response0 = new SearchResponse<object>();
			// end::6f507269ad5b31d2bb0885c1b18aac1a[]

			response0.MatchesExample(@"GET /_cat/segments?v");
		}
	}
}

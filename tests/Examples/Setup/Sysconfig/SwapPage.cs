// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Setup.Sysconfig
{
	public class SwapPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/sysconfig/swap.asciidoc:71")]
		public void Line71()
		{
			// tag::ed250b74bc77c15bb794f55a12d762c3[]
			var response0 = new SearchResponse<object>();
			// end::ed250b74bc77c15bb794f55a12d762c3[]

			response0.MatchesExample(@"GET _nodes?filter_path=**.mlockall");
		}
	}
}

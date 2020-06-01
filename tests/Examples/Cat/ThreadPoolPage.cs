// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class ThreadPoolPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/thread_pool.asciidoc:124")]
		public void Line124()
		{
			// tag::ad88e46bb06739991498dee248850223[]
			var response0 = new SearchResponse<object>();
			// end::ad88e46bb06739991498dee248850223[]

			response0.MatchesExample(@"GET /_cat/thread_pool");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/thread_pool.asciidoc:160")]
		public void Line160()
		{
			// tag::ab5e724a4baa0cc44df33f7d62583e7f[]
			var response0 = new SearchResponse<object>();
			// end::ab5e724a4baa0cc44df33f7d62583e7f[]

			response0.MatchesExample(@"GET /_cat/thread_pool/generic?v&h=id,name,active,rejected,completed");
		}
	}
}

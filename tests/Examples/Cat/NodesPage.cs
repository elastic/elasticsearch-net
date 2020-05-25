// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class NodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/nodes.asciidoc:304")]
		public void Line304()
		{
			// tag::db20adb70a8e8d0709d15ba0daf18d23[]
			var response0 = new SearchResponse<object>();
			// end::db20adb70a8e8d0709d15ba0daf18d23[]

			response0.MatchesExample(@"GET /_cat/nodes?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/nodes.asciidoc:332")]
		public void Line332()
		{
			// tag::21d3e98d911642ab3bda2657f7a06f80[]
			var response0 = new SearchResponse<object>();
			// end::21d3e98d911642ab3bda2657f7a06f80[]

			response0.MatchesExample(@"GET /_cat/nodes?v&h=id,ip,port,v,m");
		}
	}
}

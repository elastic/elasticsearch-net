// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Root
{
	public class CatPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat.asciidoc:36")]
		public void Line36()
		{
			// tag::45bde49f35ffae3f3dabc77a592241b4[]
			var response0 = new SearchResponse<object>();
			// end::45bde49f35ffae3f3dabc77a592241b4[]

			response0.MatchesExample(@"GET /_cat/master?v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat.asciidoc:57")]
		public void Line57()
		{
			// tag::179dabbc531ede7a1813d1a11ce5b5fd[]
			var response0 = new SearchResponse<object>();
			// end::179dabbc531ede7a1813d1a11ce5b5fd[]

			response0.MatchesExample(@"GET /_cat/master?help");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat.asciidoc:85")]
		public void Line85()
		{
			// tag::d940059e16675a40e3d278073331eeed[]
			var response0 = new SearchResponse<object>();
			// end::d940059e16675a40e3d278073331eeed[]

			response0.MatchesExample(@"GET /_cat/nodes?h=ip,port,heapPercent,name");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat.asciidoc:121")]
		public void Line121()
		{
			// tag::53b027c2d3ac80e93d398762c55894eb[]
			var response0 = new SearchResponse<object>();
			// end::53b027c2d3ac80e93d398762c55894eb[]

			response0.MatchesExample(@"GET /_cat/indices?bytes=b&s=store.size:desc&v");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat.asciidoc:211")]
		public void Line211()
		{
			// tag::794fa23d07c42900b5e97fb9bf323941[]
			var response0 = new SearchResponse<object>();
			// end::794fa23d07c42900b5e97fb9bf323941[]

			response0.MatchesExample(@"GET _cat/templates?v&s=order:desc,index_patterns");
		}
	}
}

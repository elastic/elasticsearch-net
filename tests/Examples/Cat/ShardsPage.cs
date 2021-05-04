// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class ShardsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:294")]
		public void Line294()
		{
			// tag::7e126e2751311db60cfcbb22c9c41caa[]
			var response0 = new SearchResponse<object>();
			// end::7e126e2751311db60cfcbb22c9c41caa[]

			response0.MatchesExample(@"GET _cat/shards");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:320")]
		public void Line320()
		{
			// tag::e42e92050dd1c20262ce9e38f4b42ba0[]
			var response0 = new SearchResponse<object>();
			// end::e42e92050dd1c20262ce9e38f4b42ba0[]

			response0.MatchesExample(@"GET _cat/shards/twitt*");
		}

		[U(Skip = "Example not implemented")]
		[Description("cat/shards.asciidoc:385")]
		public void Line385()
		{
			// tag::25c0e66a433a0cd596e0641b752ff6d7[]
			var response0 = new SearchResponse<object>();
			// end::25c0e66a433a0cd596e0641b752ff6d7[]

			response0.MatchesExample(@"GET _cat/shards?h=index,shard,prirep,state,unassigned.reason");
		}
	}
}

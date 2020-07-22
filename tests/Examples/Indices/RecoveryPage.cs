// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class RecoveryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/recovery.asciidoc:13")]
		public void Line13()
		{
			// tag::499e9bdf7848fd4de33b6f2bd8a6b1f2[]
			var response0 = new SearchResponse<object>();
			// end::499e9bdf7848fd4de33b6f2bd8a6b1f2[]

			response0.MatchesExample(@"GET /twitter/_recovery");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/recovery.asciidoc:177")]
		public void Line177()
		{
			// tag::13ebcb01ebf1b5d2b5c52739db47e30c[]
			var response0 = new SearchResponse<object>();
			// end::13ebcb01ebf1b5d2b5c52739db47e30c[]

			response0.MatchesExample(@"GET index1,index2/_recovery?human");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/recovery.asciidoc:232")]
		public void Line232()
		{
			// tag::5dfb23f6e36ef484f1d3271bae76a8d1[]
			var response0 = new SearchResponse<object>();
			// end::5dfb23f6e36ef484f1d3271bae76a8d1[]

			response0.MatchesExample(@"GET /_recovery?human");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/recovery.asciidoc:332")]
		public void Line332()
		{
			// tag::5619103306878d58a058bce87c5bd82b[]
			var response0 = new SearchResponse<object>();
			// end::5619103306878d58a058bce87c5bd82b[]

			response0.MatchesExample(@"GET _recovery?human&detailed=true");
		}
	}
}

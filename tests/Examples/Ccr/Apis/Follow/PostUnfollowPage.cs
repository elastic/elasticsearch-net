// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.Follow
{
	public class PostUnfollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-unfollow.asciidoc:34")]
		public void Line34()
		{
			// tag::f6d493650b4344f17297b568016fb445[]
			var response0 = new SearchResponse<object>();
			// end::f6d493650b4344f17297b568016fb445[]

			response0.MatchesExample(@"POST /<follower_index>/_ccr/unfollow");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-unfollow.asciidoc:70")]
		public void Line70()
		{
			// tag::6a350a17701e8c8158407191f2718b66[]
			var response0 = new SearchResponse<object>();
			// end::6a350a17701e8c8158407191f2718b66[]

			response0.MatchesExample(@"POST /follower_index/_ccr/unfollow");
		}
	}
}

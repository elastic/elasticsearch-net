// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.Follow
{
	public class GetFollowInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/get-follow-info.asciidoc:29")]
		public void Line29()
		{
			// tag::b2440b492149b705ef107137fdccb0c2[]
			var response0 = new SearchResponse<object>();
			// end::b2440b492149b705ef107137fdccb0c2[]

			response0.MatchesExample(@"GET /<index>/_ccr/info");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/get-follow-info.asciidoc:139")]
		public void Line139()
		{
			// tag::a520168c1c8b454a8f102d6a13027c73[]
			var response0 = new SearchResponse<object>();
			// end::a520168c1c8b454a8f102d6a13027c73[]

			response0.MatchesExample(@"GET /follower_index/_ccr/info");
		}
	}
}

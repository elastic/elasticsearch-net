// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.Follow
{
	public class GetFollowStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/get-follow-stats.asciidoc:36")]
		public void Line36()
		{
			// tag::020c95db88ef356093f03be84893ddf9[]
			var response0 = new SearchResponse<object>();
			// end::020c95db88ef356093f03be84893ddf9[]

			response0.MatchesExample(@"GET /<index>/_ccr/stats");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/get-follow-stats.asciidoc:211")]
		public void Line211()
		{
			// tag::8e43bb5b7946143e69d397bb81d87df0[]
			var response0 = new SearchResponse<object>();
			// end::8e43bb5b7946143e69d397bb81d87df0[]

			response0.MatchesExample(@"GET /follower_index/_ccr/stats");
		}
	}
}

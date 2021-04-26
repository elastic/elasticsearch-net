/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

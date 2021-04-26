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
	public class PostPauseFollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-pause-follow.asciidoc:30")]
		public void Line30()
		{
			// tag::483d669ec0768bc4e275a568c6164704[]
			var response0 = new SearchResponse<object>();
			// end::483d669ec0768bc4e275a568c6164704[]

			response0.MatchesExample(@"POST /<follower_index>/_ccr/pause_follow");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-pause-follow.asciidoc:63")]
		public void Line63()
		{
			// tag::d3263afc69b6f969b9bbd8738cd07b97[]
			var response0 = new SearchResponse<object>();
			// end::d3263afc69b6f969b9bbd8738cd07b97[]

			response0.MatchesExample(@"POST /follower_index/_ccr/pause_follow");
		}
	}
}

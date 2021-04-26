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

namespace Examples.Ccr.Apis.AutoFollow
{
	public class PutAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/put-auto-follow-pattern.asciidoc:15")]
		public void Line15()
		{
			// tag::6323012afc5d0421840d67cb8a0c4cb9[]
			var response0 = new SearchResponse<object>();
			// end::6323012afc5d0421840d67cb8a0c4cb9[]

			response0.MatchesExample(@"PUT /_ccr/auto_follow/<auto_follow_pattern_name>
			{
			  ""remote_cluster"" : ""<remote_cluster>"",
			  ""leader_index_patterns"" :
			  [
			    ""<leader_index_pattern>""
			  ],
			  ""follow_index_pattern"" : ""<follow_index_pattern>""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/put-auto-follow-pattern.asciidoc:88")]
		public void Line88()
		{
			// tag::90c087560ea6c0b7405f710971c86ef0[]
			var response0 = new SearchResponse<object>();
			// end::90c087560ea6c0b7405f710971c86ef0[]

			response0.MatchesExample(@"PUT /_ccr/auto_follow/my_auto_follow_pattern
			{
			  ""remote_cluster"" : ""remote_cluster"",
			  ""leader_index_patterns"" :
			  [
			    ""leader_index*""
			  ],
			  ""follow_index_pattern"" : ""{{leader_index}}-follower"",
			  ""settings"": {
			    ""index.number_of_replicas"": 0
			  },
			  ""max_read_request_operation_count"" : 1024,
			  ""max_outstanding_read_requests"" : 16,
			  ""max_read_request_size"" : ""1024k"",
			  ""max_write_request_operation_count"" : 32768,
			  ""max_write_request_size"" : ""16k"",
			  ""max_outstanding_write_requests"" : 8,
			  ""max_write_buffer_count"" : 512,
			  ""max_write_buffer_size"" : ""512k"",
			  ""max_retry_delay"" : ""10s"",
			  ""read_poll_timeout"" : ""30s""
			}");
		}
	}
}

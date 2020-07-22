// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.Follow
{
	public class PutFollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/put-follow.asciidoc:25")]
		public void Line25()
		{
			// tag::73646c12ad33a813ab2280f1dc83500e[]
			var response0 = new SearchResponse<object>();
			// end::73646c12ad33a813ab2280f1dc83500e[]

			response0.MatchesExample(@"PUT /<follower_index>/_ccr/follow?wait_for_active_shards=1
			{
			  ""remote_cluster"" : ""<remote_cluster>"",
			  ""leader_index"" : ""<leader_index>""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/put-follow.asciidoc:88")]
		public void Line88()
		{
			// tag::1b3762712c14a19e8c2956b4f530d327[]
			var response0 = new SearchResponse<object>();
			// end::1b3762712c14a19e8c2956b4f530d327[]

			response0.MatchesExample(@"PUT /follower_index/_ccr/follow?wait_for_active_shards=1
			{
			  ""remote_cluster"" : ""remote_cluster"",
			  ""leader_index"" : ""leader_index"",
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

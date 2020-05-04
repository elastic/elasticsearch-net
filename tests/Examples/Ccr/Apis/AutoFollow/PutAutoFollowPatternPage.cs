// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
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
			// tag::754a082212929e02a9f71d5404d3301d[]
			var response0 = new SearchResponse<object>();
			// end::754a082212929e02a9f71d5404d3301d[]

			response0.MatchesExample(@"PUT /_ccr/auto_follow/my_auto_follow_pattern
			{
			  ""remote_cluster"" : ""remote_cluster"",
			  ""leader_index_patterns"" :
			  [
			    ""leader_index*""
			  ],
			  ""follow_index_pattern"" : ""{{leader_index}}-follower"",
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
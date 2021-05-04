// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class FollowConfig
	{
		[DataMember(Name = "max_read_request_operation_count")]
		public int MaximumReadRequestOperationCount { get; internal set; }

		[DataMember(Name = "max_read_request_size")]
		public string MaximumReadRequestSize { get; internal set; }

		[DataMember(Name = "max_outstanding_read_requests")]
		public int MaximumOutstandingReadRequests { get; internal set; }

		[DataMember(Name = "max_write_request_operation_count")]
		public int MaximumWriteRequestOperationCount { get; internal set; }

		[DataMember(Name = "max_write_request_size")]
		public string MaximumWriteRequestSize { get; internal set; }

		[DataMember(Name = "max_outstanding_write_requests")]
		public int MaximumOutstandingWriteRequests { get; internal set; }

		[DataMember(Name = "max_write_buffer_count")]
		public int MaximumWriteBufferCount { get; internal set; }

		[DataMember(Name = "max_write_buffer_size")]
		public string MaximumWriteBufferSize { get; internal set; }

		[DataMember(Name = "max_retry_delay")]
		public Time MaximumRetryDelay { get; internal set; }

		[DataMember(Name = "read_poll_timeout")]
		public Time ReadPollTimeout { get; internal set; }
	}
}

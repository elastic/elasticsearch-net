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

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Resumes a follower index that has been paused either explicitly with the pause follower API or
	/// implicitly due to execution that can not be retried due to failure during following. When this API returns,
	/// the follower index will resume fetching operations from the leader index.
	/// </summary>
	[MapsApi("ccr.resume_follow.json")]
	[ReadAs(typeof(ResumeFollowIndexRequest))]
	public partial interface IResumeFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		[DataMember(Name = "max_read_request_operation_count")]
		long? MaxReadRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		[DataMember(Name = "max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		[DataMember(Name = "max_read_request_size")]
		string MaxRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		[DataMember(Name = "max_write_request_operation_count")]
		long? MaxWriteRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		[DataMember(Name = "max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		[DataMember(Name = "max_outstanding_write_requests")]
		long? MaxOutstandingWriteRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		[DataMember(Name = "max_write_buffer_count")]
		long? MaxWriteBufferCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		[DataMember(Name = "max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		[DataMember(Name = "max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		[DataMember(Name = "read_poll_timeout")]
		Time ReadPollTimeout { get; set; }
	}

	/// <inheritdoc cref="IResumeFollowIndexRequest"/>
	public partial class ResumeFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		public long? MaxReadRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		public long? MaxOutstandingReadRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		public string MaxRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		public long? MaxWriteRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		public string MaxWriteRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		public long? MaxOutstandingWriteRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		public long? MaxWriteBufferCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		public string MaxWriteBufferSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		public Time MaxRetryDelay { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		public Time ReadPollTimeout { get; set; }

	}

	/// <inheritdoc cref="IResumeFollowIndexRequest"/>
	public partial class ResumeFollowIndexDescriptor
	{
		long? IResumeFollowIndexRequest.MaxReadRequestOperationCount { get; set; }
		long? IResumeFollowIndexRequest.MaxOutstandingReadRequests { get; set; }
		string IResumeFollowIndexRequest.MaxRequestSize { get; set; }
		long? IResumeFollowIndexRequest.MaxWriteRequestOperationCount { get; set; }
		string IResumeFollowIndexRequest.MaxWriteRequestSize { get; set; }
		long? IResumeFollowIndexRequest.MaxOutstandingWriteRequests { get; set; }
		long? IResumeFollowIndexRequest.MaxWriteBufferCount { get; set; }
		string IResumeFollowIndexRequest.MaxWriteBufferSize { get; set; }
		Time IResumeFollowIndexRequest.MaxRetryDelay { get; set; }
		Time IResumeFollowIndexRequest.ReadPollTimeout { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		public ResumeFollowIndexDescriptor MaxReadRequestOperationCount(long? max) => Assign(max, (a, v) => a.MaxReadRequestOperationCount = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		public ResumeFollowIndexDescriptor MaxOutstandingReadRequests(long? max) => Assign(max, (a, v) => a.MaxOutstandingReadRequests = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		public ResumeFollowIndexDescriptor MaxRequestSize(string maxRequestSize) => Assign(maxRequestSize, (a, v) => a.MaxRequestSize = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		public ResumeFollowIndexDescriptor MaxWriteRequestOperationCount(long? max) => Assign(max, (a, v) => a.MaxWriteRequestOperationCount = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		public ResumeFollowIndexDescriptor MaxWriteRequestSize(string size) => Assign(size, (a, v) => a.MaxWriteRequestSize = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		public ResumeFollowIndexDescriptor MaxOutstandingWriteRequests(long? max) => Assign(max, (a, v) => a.MaxOutstandingWriteRequests = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		public ResumeFollowIndexDescriptor MaxWriteBufferCount(long? max) => Assign(max, (a, v) => a.MaxWriteBufferCount = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		public ResumeFollowIndexDescriptor MaxWriteBufferSize(string max) => Assign(max, (a, v) => a.MaxWriteBufferSize = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		public ResumeFollowIndexDescriptor MaxRetryDelay(Time maxRetryDelay) => Assign(maxRetryDelay, (a, v) => a.MaxRetryDelay = v);

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		public ResumeFollowIndexDescriptor ReadPollTimeout(Time readPollTimeout) => Assign(readPollTimeout, (a, v) => a.ReadPollTimeout = v);
	}
}

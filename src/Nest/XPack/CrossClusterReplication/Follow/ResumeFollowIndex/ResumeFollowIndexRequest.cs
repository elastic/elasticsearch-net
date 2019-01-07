using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API resumes a follower index that has been paused either explicitly with the pause follower API or
	/// implicitly due to execution that can not be retried due to failure during following. When this API returns,
	/// the follower index will resume fetching operations from the leader index.
	/// </summary>
	[MapsApi("ccr.resume_follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ResumeFollowIndexRequest>))]
	public partial interface IResumeFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		[JsonProperty("max_read_request_operation_count")]
		long? MaxReadRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		[JsonProperty("max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		[JsonProperty("max_read_request_size")]
		string MaxRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		[JsonProperty("max_write_request_operation_count")]
		long? MaxWriteRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		[JsonProperty("max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		[JsonProperty("max_outstanding_write_requests")]
		long? MaxOutstandingWriteRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		[JsonProperty("max_write_buffer_count")]
		long? MaxWriteBufferCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		[JsonProperty("max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		[JsonProperty("max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		[JsonProperty("read_poll_timeout")]
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
		public ResumeFollowIndexDescriptor MaxReadRequestOperationCount(long? max) => Assign(a => a.MaxReadRequestOperationCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		public ResumeFollowIndexDescriptor MaxOutstandingReadRequests(long? max) => Assign(a => a.MaxOutstandingReadRequests = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		public ResumeFollowIndexDescriptor MaxRequestSize(string maxRequestSize) => Assign(a => a.MaxRequestSize = maxRequestSize);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		public ResumeFollowIndexDescriptor MaxWriteRequestOperationCount(long? max) => Assign(a => a.MaxWriteRequestOperationCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		public ResumeFollowIndexDescriptor MaxWriteRequestSize(string size) => Assign(a => a.MaxWriteRequestSize = size);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		public ResumeFollowIndexDescriptor MaxOutstandingWriteRequests(long? max) => Assign(a => a.MaxOutstandingWriteRequests = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		public ResumeFollowIndexDescriptor MaxWriteBufferCount(long? max) => Assign(a => a.MaxWriteBufferCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		public ResumeFollowIndexDescriptor MaxWriteBufferSize(string max) => Assign(a => a.MaxWriteBufferSize = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		public ResumeFollowIndexDescriptor MaxRetryDelay(Time maxRetryDelay) => Assign(a => a.MaxRetryDelay = maxRetryDelay);

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		public ResumeFollowIndexDescriptor ReadPollTimeout(Time readPollTimeout) => Assign(a => a.ReadPollTimeout = readPollTimeout);
	}
}

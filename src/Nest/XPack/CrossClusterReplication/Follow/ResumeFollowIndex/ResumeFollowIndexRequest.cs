using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.resume_follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ResumeFollowIndexRequest>))]
	public partial interface IResumeFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		long? MaxReadRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		long? MaxOutstandingReadRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		string MaxRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		long? MaxWriteRequestOperationCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		string MaxWriteRequestSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		long? MaxOutstandingWriteRequests { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		long? MaxWriteBufferCount { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		string MaxWriteBufferSize { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		Time MaxRetryDelay { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		Time ReadPollTimeout { get; set; }
	}

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

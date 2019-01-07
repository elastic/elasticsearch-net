using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API creates a new follower index that is configured to follow the referenced leader index.
	/// When this API returns, the follower index exists, and cross-cluster replication starts replicating operations
	/// from the leader index to the follower index
	/// </summary>
	[MapsApi("ccr.follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CreateFollowIndexRequest>))]
	public partial interface ICreateFollowIndexRequest
	{
		/// <summary> the remote cluster containing the leader index <para>
		[JsonProperty("remote_cluster")]
		string RemoteCluster { get; set; }

		/// <summary> the name of the index in the leader cluster to follow </summary>
		[JsonProperty("leader_index")]
		IndexName LeaderIndex { get; set; }

		/// <summary>the maximum number of operations to pull per read from the remote cluster </summary>
		[JsonProperty("max_read_request_operation_count")]
		long? MaxReadRequestOperationCount { get; set; }

		/// <summary>the maximum number of outstanding reads requests from the remote cluster</summary>
		[JsonProperty("max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <summary>the maximum size in bytes of per read of a batch of operations pulled from the remote cluster</summary>
		[JsonProperty("max_read_request_size")]
		string MaxRequestSize { get; set; }

		/// <summary>the maximum number of operations per bulk write request executed on the follower</summary>
		[JsonProperty("max_write_request_operation_count")]
		long? MaxWriteRequestOperationCount { get; set; }

		/// <summary>the maximum total bytes of operations per bulk write request executed on the follower</summary>
		[JsonProperty("max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <summary>the maximum number of outstanding write requests on the follower</summary>
		[JsonProperty("max_outstanding_write_requests")]
		long? MaxOutstandingWriteRequests { get; set; }

		/// <summary>
		/// the maximum number of operations that can be queued for writing; when this limit is reached, reads from
		/// the remote cluster will be deferred until the number of queued operations goes below the limit
		/// </summary>
		[JsonProperty("max_write_buffer_count")]
		long? MaxWriteBufferCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations that can be queued for writing; when this limit is reached, reads
		/// from the remote cluster will be deferred until the total bytes of queued operations goes below the limit
		/// </summary>
		[JsonProperty("max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <summary>
		/// the maximum time to wait before retrying an operation that failed exceptionally; an exponential backoff
		/// strategy is employed when retrying
		/// </summary>
		[JsonProperty("max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <summary>
		/// the maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the
		/// leader index; when the timeout has elapsed, the poll for operations will return to the follower so that it can
		/// update some statistics, and then the follower will immediately attempt to read from the leader again
		/// </summary>
		[JsonProperty("read_poll_timeout")]
		Time ReadPollTimeout { get; set; }
	}

	/// <inheritdoc cref="ICreateFollowIndexRequest"/>
	public partial class CreateFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.RemoteCluster"/>
		public string RemoteCluster { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.LeaderIndex"/>
		public IndexName LeaderIndex { get; set; }

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

	/// <inheritdoc cref="ICreateFollowIndexRequest"/>
	public partial class CreateFollowIndexDescriptor
	{
		string ICreateFollowIndexRequest.RemoteCluster { get; set; }
		IndexName ICreateFollowIndexRequest.LeaderIndex { get; set; }
		long? ICreateFollowIndexRequest.MaxReadRequestOperationCount { get; set; }
		long? ICreateFollowIndexRequest.MaxOutstandingReadRequests { get; set; }
		string ICreateFollowIndexRequest.MaxRequestSize { get; set; }
		long? ICreateFollowIndexRequest.MaxWriteRequestOperationCount { get; set; }
		string ICreateFollowIndexRequest.MaxWriteRequestSize { get; set; }
		long? ICreateFollowIndexRequest.MaxOutstandingWriteRequests { get; set; }
		long? ICreateFollowIndexRequest.MaxWriteBufferCount { get; set; }
		string ICreateFollowIndexRequest.MaxWriteBufferSize { get; set; }
		Time ICreateFollowIndexRequest.MaxRetryDelay { get; set; }
		Time ICreateFollowIndexRequest.ReadPollTimeout { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.RemoteCluster"/>
		public CreateFollowIndexDescriptor RemoteCluster(string remoteCluster) => Assign(a => a.RemoteCluster = remoteCluster);

		/// <inheritdoc cref="ICreateFollowIndexRequest.LeaderIndex"/>
		public CreateFollowIndexDescriptor LeaderIndex(IndexName index) => Assign(a => a.LeaderIndex = index);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxReadRequestOperationCount"/>
		public CreateFollowIndexDescriptor MaxReadRequestOperationCount(long? max) => Assign(a => a.MaxReadRequestOperationCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingReadRequests"/>
		public CreateFollowIndexDescriptor MaxOutstandingReadRequests(long? max) => Assign(a => a.MaxOutstandingReadRequests = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRequestSize"/>
		public CreateFollowIndexDescriptor MaxRequestSize(string maxRequestSize) => Assign(a => a.MaxRequestSize = maxRequestSize);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestOperationCount"/>
		public CreateFollowIndexDescriptor MaxWriteRequestOperationCount(long? max) => Assign(a => a.MaxWriteRequestOperationCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteRequestSize"/>
		public CreateFollowIndexDescriptor MaxWriteRequestSize(string maxSize) => Assign(a => a.MaxWriteRequestSize = maxSize);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxOutstandingWriteRequests"/>
		public CreateFollowIndexDescriptor MaxOutstandingWriteRequests(long? max) => Assign(a => a.MaxOutstandingWriteRequests = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferCount"/>
		public CreateFollowIndexDescriptor MaxWriteBufferCount(long? max) => Assign(a => a.MaxWriteBufferCount = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxWriteBufferSize"/>
		public CreateFollowIndexDescriptor MaxWriteBufferSize(string max) => Assign(a => a.MaxWriteBufferSize = max);

		/// <inheritdoc cref="ICreateFollowIndexRequest.MaxRetryDelay"/>
		public CreateFollowIndexDescriptor MaxRetryDelay(Time maxRetryDelay) => Assign(a => a.MaxRetryDelay = maxRetryDelay);

		/// <inheritdoc cref="ICreateFollowIndexRequest.ReadPollTimeout"/>
		public CreateFollowIndexDescriptor ReadPollTimeout(Time readPollTimeout) => Assign(a => a.ReadPollTimeout = readPollTimeout);
	}
}

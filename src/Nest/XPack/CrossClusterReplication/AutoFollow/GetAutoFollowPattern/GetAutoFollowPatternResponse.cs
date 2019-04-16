using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetAutoFollowPatternResponse : IResponse
	{
		IReadOnlyDictionary<string, AutoFollowPattern> Patterns { get; }
	}

	[JsonFormatter(typeof(DictionaryResponseFormatter<GetAutoFollowPatternResponse, string, AutoFollowPattern>))]
	public class GetAutoFollowPatternResponse : DictionaryResponseBase<string, AutoFollowPattern>, IGetAutoFollowPatternResponse
	{
		public IReadOnlyDictionary<string, AutoFollowPattern> Patterns => Self.BackingDictionary;
	}

	public interface IAutoFollowPattern
	{
		/// <summary>
		/// the remote cluster containing the leader indices to match against
		/// </summary>
		[DataMember(Name = "remote_cluster")]
		string RemoteCluster { get; set; }

		/// <summary>
		/// an array of simple index patterns to match against indices in the remote cluster specified by the remote_cluster field
		/// </summary>
		[DataMember(Name = "leader_index_patterns")]
		IEnumerable<string> LeaderIndexPatterns { get; set; }

		/// <summary>
		/// the name of follower index; the template {{leader_index}} can be used to derive the name of the follower index from the name of the leader index
		/// </summary>
		[DataMember(Name = "follow_index_pattern")]
		string FollowIndexPattern { get; set; }

		/// <summary>
		/// the maximum number of operations to pull per read from the remote cluster
		/// </summary>
		[DataMember(Name = "max_read_request_operation_count")]
		int? MaxReadRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum number of outstanding reads requests from the remote cluster
		/// </summary>
		[DataMember(Name = "max_outstanding_read_requests")]
		long? MaxOutstandingReadRequests { get; set; }

		/// <summary>
		/// the maximum size in bytes of per read of a batch of operations pulled from the remote cluster
		/// </summary>
		[DataMember(Name = "max_read_request_size")]
		string MaxReadRequestSize { get; set; }

		/// <summary>
		/// the maximum number of operations per bulk write request executed on the follower
		/// </summary>
		[DataMember(Name = "max_write_request_operation_count")]
		int? MaxWriteRequestOperationCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations per bulk write request executed on the follower
		/// </summary>
		[DataMember(Name = "max_write_request_size")]
		string MaxWriteRequestSize { get; set; }

		/// <summary>
		/// the maximum number of outstanding write requests on the follower
		/// </summary>
		[DataMember(Name = "max_outstanding_write_requests")]
		int? MaxOutstandingWriteRequests { get; set; }

		/// <summary>
		/// the maximum number of operations that can be queued for writing; when this limit is reached, reads from the remote cluster will be deferred until the number of queued operations goes below the limit
		/// </summary>
		[DataMember(Name = "max_write_buffer_count")]
		int? MaxWriteBufferCount { get; set; }

		/// <summary>
		/// the maximum total bytes of operations that can be queued for writing; when this limit is reached, reads from the remote cluster will be deferred until the total bytes of queued operations goes below the limit
		/// </summary>
		[DataMember(Name = "max_write_buffer_size")]
		string MaxWriteBufferSize { get; set; }

		/// <summary>
		/// the maximum time to wait before retrying an operation that failed exceptionally; an exponential backoff strategy is employed when retrying
		/// </summary>
		[DataMember(Name = "max_retry_delay")]
		Time MaxRetryDelay { get; set; }

		/// <summary>
		/// the maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index; when the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics, and then the follower will immediately attempt to read from the leader again
		/// </summary>
		[DataMember(Name = "read_poll_timeout")]
		Time MaxPollTimeout { get; set; }
	}

	public class AutoFollowPattern : IAutoFollowPattern
	{
		/// <inheritdoc cref="IAutoFollowPattern.RemoteCluster"/>
		public string RemoteCluster { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.LeaderIndexPatterns"/>
		public IEnumerable<string> LeaderIndexPatterns { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.FollowIndexPattern"/>
		public string FollowIndexPattern { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestOperationCount"/>
		public int? MaxReadRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingReadRequests"/>
		public long? MaxOutstandingReadRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxReadRequestSize"/>
		public string MaxReadRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestOperationCount"/>
		public int? MaxWriteRequestOperationCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteRequestSize"/>
		public string MaxWriteRequestSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxOutstandingWriteRequests"/>
		public int? MaxOutstandingWriteRequests { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferCount"/>
		public int? MaxWriteBufferCount { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxWriteBufferSize"/>
		public string MaxWriteBufferSize { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxRetryDelay"/>
		public Time MaxRetryDelay { get; set; }
		/// <inheritdoc cref="IAutoFollowPattern.MaxPollTimeout"/>
		public Time MaxPollTimeout { get; set; }
	}


}

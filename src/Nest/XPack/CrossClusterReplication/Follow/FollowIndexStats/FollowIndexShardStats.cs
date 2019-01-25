using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public class FollowIndexShardStats
	{
		/// <summary>
		/// the total of transferred bytes read from the leader (note this is only an estimate, and does not
		/// account for compression if enabled)
		/// </summary>
		[JsonProperty("bytes_read")]
		public long BytesRead { get; set; }

		///<summary>the number of failed reads</summary>
		[JsonProperty("failed_read_requests")]
		public long FailedReadRequests { get; set; }

		///<summary>the number of failed bulk write requests executed on the follower</summary>
		[JsonProperty("failed_write_requests")]
		public long FailedWriteRequests { get; set; }

		/// <summary>
		/// the current global checkpoint on the follower; the difference between the leader_global_checkpoint and
		/// the follower_global_checkpoint is an indication of how much the follower is lagging the leader
		/// </summary>
		[JsonProperty("follower_global_checkpoint")]
		public long FollowerGlobalCheckpoint { get; set; }

		///<summary>the name of the follower index</summary>
		[JsonProperty("follower_index")]
		public string FollowerIndex { get; set; }

		///<summary>the mapping version the follower is synced up to</summary>
		[JsonProperty("follower_mapping_version")]
		public long FollowerMappingVersion { get; set; }

		///<summary>the current maximum sequence number on the follower</summary>
		[JsonProperty("follower_max_seq_no")]
		public long FollowerMaxSequenceNumber { get; set; }

		///<summary>the index settings version the follower is synced up to</summary>
		[JsonProperty("follower_settings_version")]
		public long FollowerSettingsVersion { get; set; }

		///<summary>the starting sequence number of the last batch of operations requested from the leader</summary>
		[JsonProperty("last_requested_seq_no")]
		public long LastRequestedSequenceNumber { get; set; }

		///<summary>the current global checkpoint on the leader known to the follower task</summary>
		[JsonProperty("leader_global_checkpoint")]
		public long LeaderGlobalCheckpoint { get; set; }

		///<summary>the name of the index in the leader cluster being followed</summary>
		[JsonProperty("leader_index")]
		public string LeaderIndex { get; set; }

		///<summary>the current maximum sequence number on the leader known to the follower task</summary>
		[JsonProperty("leader_max_seq_no")]
		public long LeaderMaxSequenceNumber { get; set; }

		///<summary>the total number of operations read from the leader</summary>
		[JsonProperty("operations_read")]
		public long OperationsRead { get; set; }

		///<summary>the number of operations written on the follower</summary>
		[JsonProperty("operations_written")]
		public long OperationsWritten { get; set; }

		///<summary>the number of active read requests from the follower</summary>
		[JsonProperty("outstanding_read_requests")]
		public int OutstandingReadRequests { get; set; }

		///<summary>the number of active bulk write requests on the follower</summary>
		[JsonProperty("outstanding_write_requests")]
		public int OutstandingWriteRequest { get; set; }

		///<summary>the remote cluster containing the leader index</summary>
		[JsonProperty("remote_cluster")]
		public string RemoteCluster { get; set; }

		///<summary>the numerical shard ID, with values from 0 to one less than the number of replicas</summary>
		[JsonProperty("shard_id")]
		public int ShardId { get; set; }

		///<summary>the number of successful fetches</summary>
		[JsonProperty("successful_read_requests")]
		public long SuccessfulReadRequests { get; set; }

		///<summary>the number of bulk write requests executed on the follower</summary>
		[JsonProperty("successful_write_requests")]
		public long SuccessfulWriteRequests { get; set; }

		///<summary>the total time reads spent executing on the remote cluster</summary>
		[JsonProperty("total_read_remote_exec_time_millis")]
		public long TotalReadRemoteExecutionTimeInMilliseconds { get; set; }

		/// <summary>
		/// the total time reads were outstanding, measured from the time a read was sent to the leader to the time
		/// a reply was returned to the follower
		/// </summary>
		[JsonProperty("total_read_time_millis")]
		public long TotalReadTimeInMilliseconds { get; set; }

		///<summary>the total time spent writing on the follower</summary>
		[JsonProperty("total_write_time_millis")]
		public long TotalWriteTimeInMilliseconds { get; set; }

		///<summary>the number of write operations queued on the follower</summary>
		[JsonProperty("write_buffer_operation_count")]
		public long WriteBufferOperationCount { get; set; }

		///<summary>the total number of bytes of operations currently queued for writing</summary>
		[JsonProperty("write_buffer_size_in_bytes")]
		public long WriteBufferSizeInBytes { get; set; }

		[JsonProperty("read_exceptions")]
		public IReadOnlyCollection<FollowIndexReadException> ReadExceptions { get; internal set; } = EmptyReadOnly<FollowIndexReadException>.Collection;

		///<summary>
		/// the number of milliseconds since a read request was sent to the leader; note that when the follower is caught up
		/// to the leader, this number will increase up to the configured read_poll_timeout at which point another read request
		/// will be sent to the leader
		/// </summary>
		[JsonProperty("time_since_last_read_millis")]
		public long TimeSinceLastReadInMilliseconds { get; set; }

		///<summary>
		/// an object representing a fatal exception that cancelled the following task; in this situation,
		/// the following task must be resumed manually with the resume follower API
		/// </summary>
		[JsonProperty("fatal_exception")]
		public ErrorCause FatalException { get; set; }
	}

	public class FollowIndexReadException
	{
		///<summary>the starting sequence number of the batch requested from the leader</summary>
		[JsonProperty("from_seq_no")]
		public long FromSquenceNumber { get; set; }

		///<summary>the number of times the batch has been retried</summary>
		[JsonProperty("retries")]
		public int Retries { get; set; }

		///<summary>Represents the exception that caused the read to fail</summary>
		[JsonProperty("exception")]
		public ErrorCause Exception { get; set; }

	}
}

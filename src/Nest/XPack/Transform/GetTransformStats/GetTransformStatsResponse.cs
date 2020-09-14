using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetTransformStatsResponse : ResponseBase
	{
		/// <summary>
		/// The count of transforms.
		/// </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary>
		/// An array of transform statistics
		/// </summary>
		[DataMember(Name = "transforms")]
		public IReadOnlyCollection<TransformStats> Transforms { get; internal set; } = EmptyReadOnly<TransformStats>.Collection;
	}

	/// <summary>
	/// Statistics about a transform
	/// </summary>
	public class TransformStats
	{
		/// <summary>
		/// Identifier for the transform.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The status of the transform
		/// </summary>
		[DataMember(Name = "state")]
		public string State { get; internal set; }

		/// <summary>
		/// If a transform has a failed state, this property provides details about the reason for the failure.
		/// </summary>
		[DataMember(Name = "reason")]
		public string Reason { get; internal set; }

		/// <inheritdoc cref="NodeAttributes"/>
		[DataMember(Name = "node")]
		public NodeAttributes Node { get; internal set; }

		/// <inheritdoc cref="TransformIndexerStats"/>
		[DataMember(Name = "stats")]
		public TransformIndexerStats Stats { get; internal set; }

		/// <inheritdoc cref="TransformCheckpointingInfo"/>
		[DataMember(Name = "checkpointing")]
		public TransformCheckpointingInfo Checkpointing { get; internal set; }
	}

	/// <summary>
	/// Contains statistics about checkpoints.
	/// </summary>
	public class TransformCheckpointingInfo
	{
		/// <summary>
		/// The timestamp when changes were last detected in the source indices.
		/// </summary>
		[DataMember(Name = "changes_last_detected_at")]
		public long ChangesLastDetectedAt { get; internal set; }

		/// <inheritdoc cref="ChangesLastDetectedAt"/>
		[IgnoreDataMember]
		public DateTimeOffset ChangesLastDetectedAtDateTime => DateTimeUtil.UnixEpoch.AddMilliseconds(ChangesLastDetectedAt);

		/// <summary>
		/// Contains statistics about the last completed checkpoint.
		/// </summary>
		[DataMember(Name = "last")]
		public TransformCheckpointStats Last { get; internal set; }

		/// <summary>
		/// Contains statistics about the next checkpoint that is currently in progress. This object appears only when the transform state is indexing.
		/// </summary>
		[DataMember(Name = "next")]
		public TransformCheckpointStats Next { get; internal set; }

		/// <summary>
		/// The number of operations that have occurred on the source index but have not been applied to the destination index yet.
		/// A high number can indicate that the transform is failing to keep up.
		/// </summary>
		[DataMember(Name = "operations_behind")]
		public long OperationsBehind { get; internal set; }
	}

	/// <summary>
	/// Contains statistics about a transform checkpoint
	/// </summary>
	public class TransformCheckpointStats
	{
		/// <summary>
		/// The sequence number for the checkpoint.
		/// </summary>
		[DataMember(Name = "checkpoint")]
		public long Checkpoint { get; internal set; }

		/// <inheritdoc cref="TransformProgress"/>
		[DataMember(Name = "checkpoint_progress")]
		public TransformProgress CheckpointProgress { get; internal set; }

		/// <summary>
		/// The timestamp of the checkpoint, which indicates when the checkpoint was created.
		/// </summary>
		[DataMember(Name = "timestamp_millis")]
		public long TimestampMilliseconds { get; internal set; }

		/// <inheritdoc cref="TimestampMilliseconds"/>
		[IgnoreDataMember]
		public DateTimeOffset Timestamp => DateTimeUtil.UnixEpoch.AddMilliseconds(TimestampMilliseconds);

		/// <summary>
		/// When using time-based synchronization, this timestamp indicates the upper bound of data that is included in the checkpoint.
		/// </summary>
		[DataMember(Name = "time_upper_bound_millis")]
		public long TimeUpperBoundMilliseconds { get; internal set; }

		/// <inheritdoc cref="TimeUpperBoundMilliseconds"/>
		[IgnoreDataMember]
		public DateTimeOffset TimeUpperBound => DateTimeUtil.UnixEpoch.AddMilliseconds(TimeUpperBoundMilliseconds);
	}

	/// <summary>
	///  Contains statistics about the progress of the checkpoint.
	/// </summary>
	public class TransformProgress
	{
		[DataMember(Name = "total_docs")]
		public long TotalDocs {get; internal set;}

		[DataMember(Name = "docs_remaining")]
		public long DocsRemaining {get; internal set;}

		[DataMember(Name = "percent_complete")]
		public double PercentComplete {get; internal set;}

		[DataMember(Name = "docs_processed")]
		public long DocsProcessed {get; internal set;}

		[DataMember(Name = "docs_indexed")]
		public long DocsIndexed {get; internal set;}
	}

	/// <summary>
	/// An object that provides statistical information about the transform.
	/// </summary>
	[DataContract]
	public class TransformIndexerStats
	{
		/// <summary>
		/// Exponential moving average of the duration of the checkpoint, in milliseconds.
		/// </summary>
		[DataMember(Name = "exponential_avg_checkpoint_duration_ms")]
		public double ExponentialAverageCheckpointDurationMs {get; internal set; }

		/// <summary>
		/// Exponential moving average of the number of new documents that have been indexed.
		/// </summary>
		[DataMember(Name = "exponential_avg_documents_indexed")]
		public double ExponentialAverageDocumentsIndexed {get; internal set; }

		/// <summary>
		/// Exponential moving average of the number of documents that have been processed.
		/// </summary>
		[DataMember(Name = "exponential_avg_documents_processed")]
		public double ExponentialAverageDocumentsProcessed {get; internal set; }

		/// <summary>
		/// The number of search or bulk index operations processed. Documents are processed in batches instead of individually.
		/// </summary>
		[DataMember(Name = "pages_processed")]
		public long PagesProcessed {get; internal set; }

		/// <summary>
		/// The number of documents that have been processed from the source index of the transform.
		/// </summary>
		[DataMember(Name = "documents_processed")]
		public long DocumentsProcessed {get; internal set; }

		/// <summary>
		/// The number of documents that have been indexed into the destination index for the transform.
		/// </summary>
		[DataMember(Name = "documents_indexed")]
		public long DocumentsIndexed {get; internal set; }

		/// <summary>
		/// The number of times the transform has been triggered by the scheduler. For example, the scheduler triggers the transform indexer
		/// to check for updates or ingest new data at an interval specified in the frequency property.
		/// </summary>
		[DataMember(Name = "trigger_count")]
		public long TriggerCount {get; internal set; }

		/// <summary>
		/// The amount of time spent indexing, in milliseconds.
		/// </summary>
		[DataMember(Name = "index_time_in_ms")]
		public long IndexTimeInMilliseconds {get; internal set; }

		/// <summary>
		/// The amount of time spent searching, in milliseconds.
		/// </summary>
		[DataMember(Name = "search_time_in_ms")]
		public long SearchTimeInMilliseconds {get; internal set; }

		/// <summary>
		/// The amount of time spent processing results, in milliseconds.
		/// </summary>
		[DataMember(Name = "processing_time_in_ms")]
		public long ProcessingTimeInMilliseconds {get; internal set; }

		/// <summary>
		/// The number of indices created.
		/// </summary>
		[DataMember(Name = "index_total")]
		public long IndexTotal {get; internal set; }

		/// <summary>
		/// The number of search operations on the source index for the transform.
		/// </summary>
		[DataMember(Name = "search_total")]
		public long SearchTotal {get; internal set; }

		/// <summary>
		/// The number of processing operations.
		/// </summary>
		[DataMember(Name = "processing_total")]
		public long ProcessingTotal {get; internal set; }

		/// <summary>
		/// The number of search failures.
		/// </summary>
		[DataMember(Name = "search_failures")]
		public long SearchFailures {get; internal set; }

		/// <summary>
		/// The number of indexing failures.
		/// </summary>
		[DataMember(Name = "index_failures")]
		public long IndexFailures {get; internal set; }
	}

	/// <summary>
	/// For started transforms only, the node upon which the transform is started.
	/// </summary>
	public class NodeAttributes
	{
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "ephemeral_id")]
		public string EphemeralId { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }

		[DataMember(Name ="attributes")]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}

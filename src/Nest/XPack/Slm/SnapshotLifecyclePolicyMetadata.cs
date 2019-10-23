using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Metadata about a Snapshot lifecycle policy
	/// </summary>
	public class SnapshotLifecyclePolicyMetadata
	{
		/// <summary>
		/// The modified date.
		/// </summary>
		[DataMember(Name = "modified_date_millis")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset ModifiedDate { get; internal set; }

		/// <summary>
		/// The next execution date.
		/// </summary>
		[DataMember(Name = "next_execution_millis")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset NextExecution { get; internal set; }

		/// <summary>
		/// The snapshot lifecycle policy
		/// </summary>
		[DataMember(Name = "policy")]
		public SnapshotLifecyclePolicy Policy { get; internal set; }

		/// <summary>
		/// The version
		/// </summary>
		[DataMember(Name = "version")]
		public int Version { get; internal set; }

		/// <summary>
		/// If a snapshot is currently in progress this will return information about the snapshot.
		/// </summary>
		[DataMember(Name = "in_progress")]
		public SnapshotLifecycleInProgress InProgress { get; internal set; }

		/// <summary>
		///	 Information about the last time the policy successfully initiated a snapshot.
		/// </summary>
		[DataMember(Name = "last_success")]
		public SnapshotLifecycleInvocationRecord LastSuccess { get; set; }

		/// <summary>
		///	 Information about the last time the policy failed to initiate a snapshot
		/// </summary>
		[DataMember(Name = "last_failure")]
		public SnapshotLifecycleInvocationRecord LastFailure { get; set; }
	}

	public class SnapshotLifecycleInvocationRecord
	{
		[DataMember(Name = "snapshot_name")]
		public string SnapshotName { get; set; }

		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		[DataMember(Name = "time")]
		public DateTimeOffset Time { get; set; }
	}

	/// <summary>
	/// If a snapshot is in progress when calling the Get Snapshot Lifecycle metadata
	/// this will hold some minimal information about the in flight snapshot
	/// </summary>
	public class SnapshotLifecycleInProgress
	{
		/// <summary> The name of the snapshot currently being taken </summary>
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		/// <summary> The UUID of the snapshot currently being taken </summary>
		[DataMember(Name = "uuid")]
		public string UUID { get; internal set; }

		/// <summary> The state of the snapshot currently being taken </summary>
		[DataMember(Name = "state")]
		public string State { get; internal set; }

		/// <summary> The start time of the snapshot currently being taken </summary>
		[DataMember(Name = "start_time_millis")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset StartTime { get; internal set; }
	}
}

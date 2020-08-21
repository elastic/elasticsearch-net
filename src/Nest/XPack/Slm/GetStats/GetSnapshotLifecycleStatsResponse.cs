// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class GetSnapshotLifecycleStatsResponse : ResponseBase
	{
		[DataMember(Name = "retention_runs")]
		public long RetentionRuns { get; internal set; }

		[DataMember(Name = "retention_failed")]
		public long RetentionFailed { get; internal set; }

		[DataMember(Name = "retention_timed_out")]
		public long RetentionTimedOut { get; internal set; }

		[DataMember(Name = "retention_deletion_time")]
		public string RetentionDeletionTime { get; internal set; }

		[DataMember(Name = "retention_deletion_time_millis")]
		public long RetentionDeletionTimeMilliseconds { get; internal set; }

		[DataMember(Name = "total_snapshots_taken")]
		public long TotalSnapshotsTaken { get; internal set; }

		[DataMember(Name = "total_snapshots_failed")]
		public long TotalSnapshotsFailed { get; internal set; }

		[DataMember(Name = "total_snapshots_deleted")]
		public long TotalSnapshotsDeleted { get; internal set; }

		[DataMember(Name = "total_snapshot_deletion_failures")]
		public long TotalSnapshotsDeletionFailures { get; internal set; }

		//[DataMember(Name = "policy_stats")]
		//public IDictionary<string, SnapshotPolicyStats> PolicyStats { get; internal set; }
	}

	public class SnapshotPolicyStats
	{
		[DataMember(Name = "policy")]
		public string PolicyId { get; internal set; }

		[DataMember(Name = "snapshots_taken")]
		public long SnapshotsTaken { get; internal set; }

		[DataMember(Name = "snapshots_failed")]
		public long SnapshotsFailed { get; internal set; }

		[DataMember(Name = "snapshots_deleted")]
		public long SnapshotsDeleted { get; internal set; }

		[DataMember(Name = "snapshot_deletion_failures")]
		public long SnapshotsDeletionFailures { get; internal set; }
	}
}

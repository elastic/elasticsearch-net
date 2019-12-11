using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetSnapshotLifecycleStatsResponse : ResponseBase
	{
		[DataMember(Name = "retention_runs")]
		public long RetentionRuns { get; private set; }

		[DataMember(Name = "retention_failed")]
		public long RetentionFailed { get; private set; }

		[DataMember(Name = "retention_timed_out")]
		public long RetentionTimedOut { get; private set; }

		[DataMember(Name = "retention_deletion_time")]
		public string RetentionDeletionTime { get; private set; }

		[DataMember(Name = "retention_deletion_time_millis")]
		public long RetentionDeletionTimeMilliseconds { get; private set; }

		[DataMember(Name = "total_snapshots_taken")]
		public long TotalSnapshotsTaken { get; private set; }

		[DataMember(Name = "total_snapshots_failed")]
		public long TotalSnapshotsFailed { get; private set; }

		[DataMember(Name = "total_snapshots_deleted")]
		public long TotalSnapshotsDeleted { get; private set; }

		[DataMember(Name = "total_snapshot_deletion_failures")]
		public long TotalSnapshotsDeletionFailures { get; private set; }

		//[DataMember(Name = "policy_stats")]
		//public IDictionary<string, SnapshotPolicyStats> PolicyStats { get; private set; }
	}

	public class SnapshotPolicyStats
	{
		[DataMember(Name = "policy")]
		public string PolicyId { get; private set; }

		[DataMember(Name = "snapshots_taken")]
		public long SnapshotsTaken { get; private set; }

		[DataMember(Name = "snapshots_failed")]
		public long SnapshotsFailed { get; private set; }

		[DataMember(Name = "snapshots_deleted")]
		public long SnapshotsDeleted { get; private set; }

		[DataMember(Name = "snapshot_deletion_failures")]
		public long SnapshotsDeletionFailures { get; private set; }
	}
}

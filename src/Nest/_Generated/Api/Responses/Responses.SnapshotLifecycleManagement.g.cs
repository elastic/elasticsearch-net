// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using OneOf;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest.Slm
{
	public partial class DeleteLifecycleResponse : AcknowledgedResponseBase
	{
	}

	public partial class ExecuteLifecycleResponse : ResponseBase
	{
		[JsonPropertyName("snapshot_name")]
		public Nest.Name SnapshotName
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ExecuteRetentionResponse : AcknowledgedResponseBase
	{
	}

	public partial class GetLifecycleResponse : DictionaryResponseBase<Nest.Id, Nest.Slm.SnapshotLifecycle>
	{
	}

	public partial class GetStatsResponse : ResponseBase
	{
		[JsonPropertyName("policy_stats")]
		public IReadOnlyCollection<string> PolicyStats
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("retention_deletion_time")]
		public string RetentionDeletionTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("retention_deletion_time_millis")]
		public Nest.EpochMillis RetentionDeletionTimeMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("retention_failed")]
		public long RetentionFailed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("retention_runs")]
		public long RetentionRuns
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("retention_timed_out")]
		public long RetentionTimedOut
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_snapshot_deletion_failures")]
		public long TotalSnapshotDeletionFailures
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_snapshots_deleted")]
		public long TotalSnapshotsDeleted
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_snapshots_failed")]
		public long TotalSnapshotsFailed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_snapshots_taken")]
		public long TotalSnapshotsTaken
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetStatusResponse : ResponseBase
	{
		[JsonPropertyName("operation_mode")]
		public Nest.LifecycleOperationMode OperationMode
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class PutLifecycleResponse : AcknowledgedResponseBase
	{
	}

	public partial class StartResponse : AcknowledgedResponseBase
	{
	}

	public partial class StopResponse : AcknowledgedResponseBase
	{
	}
}
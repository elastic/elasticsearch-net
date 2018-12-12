using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;


namespace Nest
{
	[InterfaceDataContract]
	public interface IWatcherStatsResponse : IResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="manually_stopped")]
		bool ManuallyStopped { get; }

		[DataMember(Name ="stats")]
		IReadOnlyCollection<WatcherNodeStats> Stats { get; }
	}

	public class WatcherStatsResponse : ResponseBase, IWatcherStatsResponse
	{
		public string ClusterName { get; internal set; }

		public bool ManuallyStopped { get; internal set; }
		public IReadOnlyCollection<WatcherNodeStats> Stats { get; internal set; } = EmptyReadOnly<WatcherNodeStats>.Collection;
	}

	public class WatcherNodeStats
	{
		[DataMember(Name ="current_watches")]
		public IReadOnlyCollection<WatchRecordStats> CurrentWatches { get; internal set; } = EmptyReadOnly<WatchRecordStats>.Collection;

		[DataMember(Name ="execution_thread_pool")]
		public ExecutionThreadPool ExecutionThreadPool { get; internal set; }

		[DataMember(Name ="queued_watches")]
		public IReadOnlyCollection<WatchRecordQueuedStats> QueuedWatches { get; internal set; } = EmptyReadOnly<WatchRecordQueuedStats>.Collection;

		[DataMember(Name ="watch_count")]
		public long WatchCount { get; internal set; }

		[DataMember(Name ="watcher_state")]
		public WatcherState WatcherState { get; internal set; }
	}


	public enum WatcherState
	{
		[EnumMember(Value = "stopped")]
		Stopped,

		[EnumMember(Value = "starting")]
		Starting,

		[EnumMember(Value = "started")]
		Started,

		[EnumMember(Value = "stopping")]
		Stopping,
	}

	public class WatchRecordQueuedStats
	{
		[DataMember(Name ="execution_time")]
		public DateTimeOffset? ExecutionTime { get; internal set; }

		[DataMember(Name ="triggered_time")]
		public DateTimeOffset? TriggeredTime { get; internal set; }

		[DataMember(Name ="watch_id")]
		public string WatchId { get; internal set; }

		[DataMember(Name ="watch_record_id")]
		public string WatchRecordId { get; internal set; }
	}

	public class WatchRecordStats : WatchRecordQueuedStats
	{
		[DataMember(Name ="execution_phase")]
		public ExecutionPhase? ExecutionPhase { get; internal set; }
	}

	[DataContract]
	public class ExecutionThreadPool
	{
		[DataMember(Name ="max_size")]
		public long MaxSize { get; internal set; }

		[DataMember(Name ="queue_size")]
		public long QueueSize { get; internal set; }
	}


	public enum ExecutionPhase
	{
		[EnumMember(Value = "awaits_execution")]
		AwaitsExecution,

		[EnumMember(Value = "started")]
		Started,

		[EnumMember(Value = "input")]
		Input,

		[EnumMember(Value = "condition")]
		Condition,

		[EnumMember(Value = "actions")]
		Actions,

		[EnumMember(Value = "watch_transform")]
		WatchTransform,

		[EnumMember(Value = "aborted")]
		Aborted,

		[EnumMember(Value = "finished")]
		Finished,
	}
}

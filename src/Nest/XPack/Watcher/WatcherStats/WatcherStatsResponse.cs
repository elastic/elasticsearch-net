using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	public interface IWatcherStatsResponse : IResponse
	{
		[JsonProperty("current_watches")]
		IReadOnlyCollection<WatchRecordStats> CurrentWatches { get; }

		[JsonProperty("execution_thread_pool")]
		ExecutionThreadPool ExecutionThreadPool { get; }

		[JsonProperty("manually_stopped")]
		bool ManuallyStopped { get; }

		[JsonProperty("queued_watches")]
		IReadOnlyCollection<WatchRecordQueuedStats> QueuedWatches { get; }

		[JsonProperty("watch_count")]
		long WatchCount { get; }

		[JsonProperty("watcher_state")]
		WatcherState WatcherState { get; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
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

	public class WatcherStatsResponse : ResponseBase, IWatcherStatsResponse
	{
		public IReadOnlyCollection<WatchRecordStats> CurrentWatches { get; internal set; }

		public ExecutionThreadPool ExecutionThreadPool { get; internal set; }

		public bool ManuallyStopped { get; internal set; }

		public IReadOnlyCollection<WatchRecordQueuedStats> QueuedWatches { get; internal set; }

		public long WatchCount { get; internal set; }
		public WatcherState WatcherState { get; internal set; }
	}

	public class WatchRecordQueuedStats
	{
		[JsonProperty("execution_time")]
		public DateTimeOffset? ExecutionTime { get; internal set; }

		[JsonProperty("triggered_time")]
		public DateTimeOffset? TriggeredTime { get; internal set; }

		[JsonProperty("watch_id")]
		public string WatchId { get; internal set; }

		[JsonProperty("watch_record_id")]
		public string WatchRecordId { get; internal set; }
	}

	public class WatchRecordStats : WatchRecordQueuedStats
	{
		[JsonProperty("execution_phase")]
		public ExecutionPhase? ExecutionPhase { get; internal set; }
	}

	[JsonObject]
	public class ExecutionThreadPool
	{
		[JsonProperty("max_size")]
		public long MaxSize { get; internal set; }

		[JsonProperty("queue_size")]
		public long QueueSize { get; internal set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
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

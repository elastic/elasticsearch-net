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
		[JsonProperty("watcher_state")]
		WatcherState WatcherState { get; }

		[JsonProperty("watch_count")]
		long WatchCount { get; }

		[JsonProperty("execution_thread_pool")]
		ExecutionThreadPool ExecutionThreadPool { get; }

		[JsonProperty("current_watches")]
		IEnumerable<WatchRecordStats> CurrentWatches { get; }

		[JsonProperty("queued_watches")]
		IEnumerable<WatchRecordQueuedStats> QueuedWatches { get; }

		[JsonProperty("manually_stopped")]
		bool ManuallyStopped { get; }
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
		public WatcherState WatcherState { get; internal set; }

		public long WatchCount { get; internal set; }

		public ExecutionThreadPool ExecutionThreadPool { get; internal set; }

		public IEnumerable<WatchRecordStats> CurrentWatches { get; internal set; }

		public IEnumerable<WatchRecordQueuedStats> QueuedWatches { get; internal set; }

		public bool ManuallyStopped { get; internal set; }
	}

	public class WatchRecordQueuedStats
	{
		[JsonProperty("watch_id")]
		public Id WatchId { get; internal set; }

		[JsonProperty("watch_record_id")]
		public Id WatchRecordId { get; internal set; }

		[JsonProperty("triggered_time")]
		public DateTimeOffset? TriggeredTime { get; internal set; }

		[JsonProperty("execution_time")]
		public DateTimeOffset? ExecutionTime { get; internal set; }

	}
	public class WatchRecordStats : WatchRecordQueuedStats
	{
		[JsonProperty("execution_phase")]
		public ExecutionPhase? ExecutionPhase { get; internal set; }
	}

	[JsonObject]
	public class ExecutionThreadPool
	{
		[JsonProperty("queue_size")]
		public long QueueSize { get; internal set; }

		[JsonProperty("max_size")]
		public long MaxSize { get; internal set; }
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

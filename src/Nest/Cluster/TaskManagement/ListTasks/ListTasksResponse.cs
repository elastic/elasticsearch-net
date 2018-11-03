using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IListTasksResponse : IResponse
	{
		[JsonProperty("node_failures")]
		IReadOnlyCollection<ErrorCause> NodeFailures { get; }

		[JsonProperty("nodes")]
		IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; }
	}

	public class ListTasksResponse : ResponseBase, IListTasksResponse
	{
		public override bool IsValid => base.IsValid && !NodeFailures.HasAny();
		public IReadOnlyCollection<ErrorCause> NodeFailures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
	}

	public class TaskExecutingNode
	{
		[JsonProperty("host")]
		public string Host { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("tasks")]
		public IReadOnlyDictionary<TaskId, TaskState> Tasks { get; internal set; } = EmptyReadOnly<TaskId, TaskState>.Dictionary;

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }
	}

	public class TaskState
	{
		[JsonProperty("action")]
		public string Action { get; internal set; }

		[JsonProperty("cancellable")]
		public bool Cancellable { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("headers")]
		public IReadOnlyDictionary<string, string> Headers { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("parent_task_id")]
		public TaskId ParentTaskId { get; internal set; }

		[JsonProperty("running_time_in_nanos")]
		public long RunningTimeInNanoSeconds { get; internal set; }

		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[JsonProperty("status")]
		public TaskStatus Status { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}

	public class TaskStatus
	{
		[JsonProperty("batches")]
		public long Batches { get; internal set; }

		[JsonProperty("created")]
		public long Created { get; internal set; }

		[JsonProperty("deleted")]
		public long Deleted { get; internal set; }

		[JsonProperty("noops")]
		public long Noops { get; internal set; }

		[JsonProperty("requests_per_second")]
		public long RequestsPerSecond { get; internal set; }

		[JsonProperty("retries")]
		public TaskRetries Retries { get; internal set; }

		[JsonProperty("throttled_millis")]
		public long ThrottledMilliseconds { get; internal set; }

		[JsonProperty("throttled_until_millis")]
		public long ThrottledUntilMilliseconds { get; internal set; }

		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("updated")]
		public long Updated { get; internal set; }

		[JsonProperty("version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}

	public class TaskRetries
	{
		[JsonProperty("bulk")]
		public int Bulk { get; internal set; }

		[JsonProperty("search")]
		public int Search { get; internal set; }
	}
}

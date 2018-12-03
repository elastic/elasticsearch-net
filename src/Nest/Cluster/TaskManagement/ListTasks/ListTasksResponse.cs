using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IListTasksResponse : IResponse
	{
		[DataMember(Name = "node_failures")]
		IReadOnlyCollection<ErrorCause> NodeFailures { get; }

		[DataMember(Name = "nodes")]
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
		[DataMember(Name = "host")]
		public string Host { get; internal set; }

		[DataMember(Name = "ip")]
		public string Ip { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "tasks")]
		public IReadOnlyDictionary<TaskId, TaskState> Tasks { get; internal set; } = EmptyReadOnly<TaskId, TaskState>.Dictionary;

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}

	public class TaskState
	{
		[DataMember(Name = "action")]
		public string Action { get; internal set; }

		[DataMember(Name = "cancellable")]
		public bool Cancellable { get; internal set; }

		[DataMember(Name = "description")]
		public string Description { get; internal set; }

		[DataMember(Name = "headers")]
		public IReadOnlyDictionary<string, string> Headers { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name = "id")]
		public long Id { get; internal set; }

		[DataMember(Name = "node")]
		public string Node { get; internal set; }

		[DataMember(Name = "parent_task_id")]
		public TaskId ParentTaskId { get; internal set; }

		[DataMember(Name = "running_time_in_nanos")]
		public long RunningTimeInNanoSeconds { get; internal set; }

		[DataMember(Name = "start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "status")]
		public TaskStatus Status { get; internal set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}

	public class TaskStatus
	{
		[DataMember(Name = "batches")]
		public long Batches { get; internal set; }

		[DataMember(Name = "created")]
		public long Created { get; internal set; }

		[DataMember(Name = "deleted")]
		public long Deleted { get; internal set; }

		[DataMember(Name = "noops")]
		public long Noops { get; internal set; }

		[DataMember(Name = "requests_per_second")]
		public long RequestsPerSecond { get; internal set; }

		[DataMember(Name = "retries")]
		public TaskRetries Retries { get; internal set; }

		[DataMember(Name = "throttled_millis")]
		public long ThrottledMilliseconds { get; internal set; }

		[DataMember(Name = "throttled_until_millis")]
		public long ThrottledUntilMilliseconds { get; internal set; }

		[DataMember(Name = "total")]
		public long Total { get; internal set; }

		[DataMember(Name = "updated")]
		public long Updated { get; internal set; }

		[DataMember(Name = "version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}

	public class TaskRetries
	{
		[DataMember(Name = "bulk")]
		public int Bulk { get; internal set; }

		[DataMember(Name = "search")]
		public int Search { get; internal set; }
	}
}

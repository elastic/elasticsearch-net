using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITasksListResponse: IResponse
	{
		[JsonProperty("nodes")]
		IDictionary<string, TaskExecutingNode> Nodes { get; }

		[JsonProperty("node_failures")]
		IEnumerable<Throwable> NodeFailures { get; }

	}

	public class TasksListResponse : ResponseBase, ITasksListResponse
	{
		public override bool IsValid => base.IsValid && !this.NodeFailures.HasAny();

		public IDictionary<string, TaskExecutingNode> Nodes { get; internal set; }
		public IEnumerable<Throwable> NodeFailures { get; internal set; }
	}

	public class TaskExecutingNode
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("host")]
		public string Host { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("tasks")]
		public IDictionary<TaskId, TaskState> Tasks { get; internal set; }
	}

	public class TaskState
	{
		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("action")]
		public string Action { get; internal set; }

		[JsonProperty("status")]
		public TaskStatus Status { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[JsonProperty("running_time_in_nanos")]
		public long RunningTimeInNanoSeconds { get; internal set; }

		[JsonProperty("parent_task_id")]
		public TaskId ParentTaskId { get; internal set; }
	}

	public class TaskStatus
	{
		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("updated")]
		public long Updated { get; internal set; }

		[JsonProperty("created")]
		public long Created { get; internal set; }

		[JsonProperty("deleted")]
		public long Deleted { get; internal set; }

		[JsonProperty("batches")]
		public long Batches { get; internal set; }

		[JsonProperty("version_conflicts")]
		public long VersionConflicts { get; internal set; }

		[JsonProperty("noops")]
		public long Noops { get; internal set; }
	}
}

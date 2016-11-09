using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterPendingTasksResponse : IResponse
	{
		IReadOnlyCollection<PendingTask> Tasks { get; }
	}

	[JsonObject]
	public class ClusterPendingTasksResponse : ResponseBase, IClusterPendingTasksResponse
	{
		[JsonProperty("tasks")]
		public IReadOnlyCollection<PendingTask> Tasks { get; internal set; } = EmptyReadOnly<PendingTask>.Collection;
	}

	[JsonObject]
	public class PendingTask
	{
		[JsonProperty("insert_order")]
		public int InsertOrder { get; internal set; }

		[JsonProperty("priority")]
		public string Priority { get; internal set; }

		[JsonProperty("source")]
		public string Source { get; internal set; }

		[JsonProperty("time_in_queue_millis")]
		public int TimeInQueueMilliseconds { get; internal set; }

		[JsonProperty("time_in_queue")]
		public string TimeInQueue { get; internal set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IClusterPendingTasksResponse : IResponse
	{
		IEnumerable<PendingTask> Tasks { get; set; }
	}

	[JsonObject]
	public class ClusterPendingTasksResponse : BaseResponse, IClusterPendingTasksResponse
	{
		[JsonProperty("tasks")]
		public IEnumerable<PendingTask> Tasks { get; set; }
	}

	[JsonObject]
	public class PendingTask
	{
		[JsonProperty("insert_order")]
		public int InsertOrder { get; set; }

		[JsonProperty("priority")]
		public string Priority { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("time_in_queue_millis")]
		public int TimeInQueueMilliseconds { get; set; }

		[JsonProperty("time_in_queue")]
		public string TimeInQueue { get; set; }
	}
}

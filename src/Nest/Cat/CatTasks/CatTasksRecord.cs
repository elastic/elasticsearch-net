using Newtonsoft.Json;

namespace Nest
{
	public class CatTasksRecord : ICatRecord
	{
		[JsonProperty("action")]
		public string Action { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("parent_task_id")]
		public string ParentTaskId { get; internal set; }

		[JsonProperty("running_time")]
		public string RunningTime { get; internal set; }

		[JsonProperty("start_time")]
		public string StartTime { get; internal set; }

		[JsonProperty("task_id")]
		public string TaskId { get; internal set; }

		[JsonProperty("timestamp")]
		public string Timestamp { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}
}

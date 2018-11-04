using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class TaskInfo
	{
		[JsonProperty("action")]
		public string Action { get; internal set; }

		[JsonProperty("cancellable")]
		public bool Cancellable { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("running_time_in_nanos")]
		public long RunningTimeInNanoseconds { get; internal set; }

		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[JsonProperty("status")]
		public TaskStatus Status { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}
}

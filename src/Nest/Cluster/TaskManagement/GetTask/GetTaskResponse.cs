using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGetTaskResponse : IResponse
	{
		[JsonProperty("completed")]
		bool Completed { get; }

		[JsonProperty("task")]
		TaskInfo Task { get; }
	}

	public class GetTaskResponse : ResponseBase, IGetTaskResponse
	{
		public bool Completed { get; internal set; }

		public TaskInfo Task { get; internal set; }
	}
}

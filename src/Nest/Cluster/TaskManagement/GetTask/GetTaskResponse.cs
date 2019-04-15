using System.Runtime.Serialization;

namespace Nest
{
	public class GetTaskResponse : ResponseBase
	{
		[DataMember(Name = "completed")]
		public bool Completed { get; internal set; }

		[DataMember(Name = "task")]
		public TaskInfo Task { get; internal set; }
	}
}

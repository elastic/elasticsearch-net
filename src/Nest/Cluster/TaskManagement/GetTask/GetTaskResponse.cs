using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IGetTaskResponse : IResponse
	{
		[DataMember(Name ="completed")]
		bool Completed { get; }

		[DataMember(Name ="task")]
		TaskInfo Task { get; }
	}

	public class GetTaskResponse : ResponseBase, IGetTaskResponse
	{
		public bool Completed { get; internal set; }

		public TaskInfo Task { get; internal set; }
	}
}

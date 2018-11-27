using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IStopRollupJobResponse : IResponse
	{
		[DataMember(Name ="stopped")]
		bool Stopped { get; set; }
	}

	public class StopRollupJobResponse : ResponseBase, IStopRollupJobResponse
	{
		public bool Stopped { get; set; }
	}
}

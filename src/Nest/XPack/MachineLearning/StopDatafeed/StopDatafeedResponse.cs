using System.Runtime.Serialization;

namespace Nest
{
	public interface IStopDatafeedResponse : IResponse
	{
		bool Stopped { get; }
	}

	public class StopDatafeedResponse : ResponseBase, IStopDatafeedResponse
	{
		[DataMember(Name ="stopped")]
		public bool Stopped { get; internal set; }
	}
}

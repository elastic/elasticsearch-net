using System.Runtime.Serialization;

namespace Nest
{
	public class StopDatafeedResponse : ResponseBase
	{
		[DataMember(Name ="stopped")]
		public bool Stopped { get; internal set; }
	}
}

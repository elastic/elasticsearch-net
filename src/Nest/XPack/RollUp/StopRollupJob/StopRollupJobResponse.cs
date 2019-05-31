using System.Runtime.Serialization;

namespace Nest
{
	public class StopRollupJobResponse : ResponseBase
	{
		[DataMember(Name ="stopped")]
		public bool Stopped { get; set; }
	}
}

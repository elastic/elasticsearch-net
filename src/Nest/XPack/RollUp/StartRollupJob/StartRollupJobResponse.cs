using System.Runtime.Serialization;

namespace Nest
{
	public class StartRollupJobResponse : ResponseBase
	{
		[DataMember(Name ="started")]
		public bool Started { get; set; }
	}
}

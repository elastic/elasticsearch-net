using System.Runtime.Serialization;

namespace Nest
{
	public class StartDatafeedResponse : ResponseBase
	{
		[DataMember(Name ="started")]
		public bool Started { get; internal set; }
	}
}

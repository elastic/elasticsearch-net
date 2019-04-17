using System.Runtime.Serialization;

namespace Nest
{
	public class CloseJobResponse : ResponseBase
	{
		[DataMember(Name ="closed")]
		public bool Closed { get; internal set; }
	}
}

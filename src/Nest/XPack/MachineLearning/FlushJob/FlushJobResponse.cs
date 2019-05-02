using System.Runtime.Serialization;

namespace Nest
{
	public class FlushJobResponse : ResponseBase
	{
		[DataMember(Name ="flushed")]
		public bool Flushed { get; internal set; }
	}
}

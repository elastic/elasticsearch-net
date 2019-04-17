using System.Runtime.Serialization;

namespace Nest
{
	public class OpenJobResponse : ResponseBase
	{
		[DataMember(Name ="opened")]
		public bool Opened { get; internal set; }
	}
}

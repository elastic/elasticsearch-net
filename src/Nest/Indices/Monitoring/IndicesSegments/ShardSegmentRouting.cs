using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardSegmentRouting
	{
		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }
	}
}

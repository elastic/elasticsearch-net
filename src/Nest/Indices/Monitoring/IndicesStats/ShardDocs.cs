using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardDocs
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="deleted")]
		public long Deleted { get; internal set; }
	}
}

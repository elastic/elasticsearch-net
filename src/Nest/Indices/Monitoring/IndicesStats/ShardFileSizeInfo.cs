using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardFileSizeInfo
	{
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="size_in_bytes")]
		public long SizeInBytes { get; internal set; }
	}
}

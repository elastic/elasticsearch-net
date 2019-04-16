using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatAllocationRecord : ICatRecord
	{
		[DataMember(Name ="diskAvail")]
		public string DiskAvailable { get; set; }

		[DataMember(Name ="diskRatio")]
		public string DiskRatio { get; set; }

		[DataMember(Name ="diskUsed")]
		public string DiskUsed { get; set; }

		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		[DataMember(Name ="node")]
		public string Node { get; set; }

		[DataMember(Name ="shards")]
		public string Shards { get; set; }
	}
}

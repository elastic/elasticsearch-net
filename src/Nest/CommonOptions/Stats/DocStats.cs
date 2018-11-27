using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class DocStats
	{
		[DataMember(Name ="count")]
		public long Count { get; set; }

		[DataMember(Name ="deleted")]
		public long Deleted { get; set; }
	}
}

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class StoreStats
	{
		[DataMember(Name ="size")]
		public string Size { get; set; }

		[DataMember(Name ="size_in_bytes")]
		public double SizeInBytes { get; set; }
	}
}

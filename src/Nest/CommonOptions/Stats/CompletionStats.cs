using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CompletionStats
	{
		[DataMember(Name ="size")]
		public string Size { get; set; }

		[DataMember(Name ="size_in_bytes")]
		public long SizeInBytes { get; set; }
	}
}

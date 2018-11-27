using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryFileDetails
	{
		[DataMember(Name ="length")]
		public long Length { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="recovered")]
		public long Recovered { get; internal set; }
	}
}

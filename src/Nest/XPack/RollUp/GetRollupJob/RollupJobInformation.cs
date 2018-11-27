using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobInformation
	{
		[DataMember(Name ="config")]
		public RollupJobConfiguration Config { get; internal set; }

		[DataMember(Name ="stats")]
		public RollupJobStats Stats { get; internal set; }

		[DataMember(Name ="status")]
		public RollupJobStatus Status { get; internal set; }
	}
}

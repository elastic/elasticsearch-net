using System.Runtime.Serialization;

namespace Nest
{
	public class UpgradeStatus
	{
		[DataMember(Name ="size")]
		public string Size { get; set; }

		[DataMember(Name ="size_in_bytes")]
		public long SizeInBytes { get; set; }

		[DataMember(Name ="size_to_upgrade")]
		public string SizeToUpgrade { get; set; }

		[DataMember(Name ="size_to_upgrade_ancient_in_bytes")]
		public string SizeToUpgradeAncientInBytes { get; set; }

		[DataMember(Name ="size_to_upgrade_in_bytes")]
		public string SizeToUpgradeInBytes { get; set; }
	}
}

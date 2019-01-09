using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	// TODO: look to see if needed
	//[JsonFormatter(typeof(UpgradeStatusResponseJsonConverter))]
	public interface IUpgradeStatusResponse : IResponse
	{
		[DataMember(Name = "size")]
		string Size { get; }

		[DataMember(Name = "size_in_bytes")]
		long SizeInBytes { get; }

		[DataMember(Name = "size_to_upgrade_ancient_in_bytes")]
		string SizeToUpgradeAncientInBytes { get; }

		[DataMember(Name = "size_to_upgrade_in_bytes")]
		string SizeToUpgradeInBytes { get; }

		[DataMember(Name = "indices")]
		IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; }
	}

	public class UpgradeStatusResponse : ResponseBase, IUpgradeStatusResponse
	{
		public string Size { get; internal set; }
		public long SizeInBytes { get; internal set; }
		public string SizeToUpgradeAncientInBytes { get; internal set; }
		public string SizeToUpgradeInBytes { get; internal set; }

		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, UpgradeStatus>))]
		public IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; internal set; } = EmptyReadOnly<string, UpgradeStatus>.Dictionary;
	}
}

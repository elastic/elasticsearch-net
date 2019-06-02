using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class UpgradeStatusResponse : ResponseBase
	{
	// TODO: look to see if needed
	//[JsonFormatter(typeof(UpgradeStatusResponseJsonConverter))]
		[DataMember(Name = "size")]
		public string Size { get; internal set; }
		[DataMember(Name = "size_in_bytes")]
		public long SizeInBytes { get; internal set; }
		[DataMember(Name = "size_to_upgrade_ancient_in_bytes")]
		public string SizeToUpgradeAncientInBytes { get; internal set; }
		[DataMember(Name = "size_to_upgrade_in_bytes")]
		public string SizeToUpgradeInBytes { get; internal set; }

		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, UpgradeStatus>))]
		[DataMember(Name = "indices")]
		public IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; internal set; } = EmptyReadOnly<string, UpgradeStatus>.Dictionary;
	}
}

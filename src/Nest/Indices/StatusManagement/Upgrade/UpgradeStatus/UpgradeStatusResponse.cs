using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(UpgradeStatusResponseJsonConverter))]
	public interface IUpgradeStatusResponse : IResponse
	{
		long SizeInBytes { get; }
		string SizeToUpgradeInBytes { get; }
		string SizeToUpgradeAncientInBytes { get; }
		IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; }
	}

	public class UpgradeStatusResponse : ResponseBase, IUpgradeStatusResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, UpgradeStatus>))]
		public IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; internal set; } = EmptyReadOnly<string, UpgradeStatus>.Dictionary;
		public long SizeInBytes { get; internal set; }
		public string SizeToUpgradeInBytes { get; internal set; }
		public string SizeToUpgradeAncientInBytes { get; internal set; }
	}
}

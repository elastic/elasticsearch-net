using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(UpgradeStatusResponseJsonConverter))]
	public interface IUpgradeStatusResponse : IResponse
	{
		long SizeInBytes { get; }
		string SizeToUpgradeAncientInBytes { get; }
		string SizeToUpgradeInBytes { get; }
		IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; }
	}

	public class UpgradeStatusResponse : ResponseBase, IUpgradeStatusResponse
	{
		public long SizeInBytes { get; internal set; }
		public string SizeToUpgradeAncientInBytes { get; internal set; }
		public string SizeToUpgradeInBytes { get; internal set; }

		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, UpgradeStatus>))]
		public IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; internal set; } = EmptyReadOnly<string, UpgradeStatus>.Dictionary;
	}
}

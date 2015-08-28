using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(UpgradeStatusResponseJsonConverter))]
	public interface IUpgradeStatusResponse : IResponse
	{
		long SizeInBytes { get; set; }
		string SizeToUpgradeInBytes { get; set; }
		string SizeToUpgradeAncientInBytes { get; set; }
		Dictionary<string, UpgradeStatus> Upgrades { get; set; }

	}

	public class UpgradeStatusResponse : BaseResponse, IUpgradeStatusResponse
	{
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, UpgradeStatus> Upgrades { get; set; }
		public long SizeInBytes { get; set; }
		public string SizeToUpgradeInBytes { get; set; }
		public string SizeToUpgradeAncientInBytes { get; set; }
	}
}

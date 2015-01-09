using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IUpgradeStatusResponse : IResponse
	{
		Dictionary<string, UpgradeStatus> Upgrades { get; set; }
	}

	public class UpgradeStatusResponse : BaseResponse, IUpgradeStatusResponse
	{
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, UpgradeStatus> Upgrades { get; set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

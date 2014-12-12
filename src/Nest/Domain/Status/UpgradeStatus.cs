using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class UpgradeStatus
	{
		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; set; }

		[JsonProperty("size_to_upgrade")]
		public string SizeToUpgrade { get; set; }

		[JsonProperty("size_to_upgrade_in_bytes")]
		public string SizeToUpgradeInBytes { get; set; }
	}
}

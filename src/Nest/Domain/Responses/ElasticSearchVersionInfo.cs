using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class ElasticSearchVersionInfo
	{
		public string Number { get; set; }
		public DateTime Date { get; set; }
		[JsonProperty(PropertyName = "snapshot_build")]
		public bool IsSnapShotBuild { get; set; }
	}
}

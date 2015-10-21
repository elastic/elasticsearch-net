using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class DocStats
	{
		[JsonProperty(PropertyName = "count")]
		public long Count { get; set; }

		[JsonProperty(PropertyName = "deleted")]
		public long Deleted { get; set; }
	}
}

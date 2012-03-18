using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class DocStats
	{
		[JsonProperty(PropertyName = "count")]
		public int Count { get; set; }
		[JsonProperty(PropertyName = "deleted")]
		public int Deleted { get; set; }
	}
}

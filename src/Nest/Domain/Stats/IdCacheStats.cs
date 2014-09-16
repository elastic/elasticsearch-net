using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class IdCacheStats
	{
		[JsonProperty("memory_size")]
		public string MemorySize { get; set; }

		[JsonProperty("memory_size_in_bytes")]
		public string MemorySizeInBytes { get; set; }
	}
}

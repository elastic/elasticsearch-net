using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class SegmentsStats
	{
		[JsonProperty("count")]
		public long Count { get; set; }

		[JsonProperty("memory_in_bytes")]
		public long MemoryInBytes { get; set; }

		[JsonProperty("index_writer_memory_in_bytes")]
		public long IndexWriterMemoryInBytes { get; set; }

		[JsonProperty("version_map_memory_in_bytes")]
		public long VersionMapMemoryInBytes { get; set; }
	}
}
